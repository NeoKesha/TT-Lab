﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;


namespace Twinsanity.PS2Hardware
{
    using SwizzledVectorData = List<List<List<Vector4>>>;

    /// <summary>
    /// Provides interface for compiling vector data into VIF instructions specifically for Twinsanity's 3d model formats
    /// </summary>
    public class TwinVIFCompiler
    {

        private readonly ModelFormat format;
        private readonly List<List<Vector4>> vectorData;
        private readonly List<bool> conns;
        private SwizzledVectorData swizzledVectorBatches;
        private readonly Dictionary<ModelFormat, List<UInt16>> outputAddressMap = new()
        {
            {
                ModelFormat.Model, new List<UInt16> { 0, 1, 3, 4, 5, 6 }
            },
            {
                ModelFormat.Skin, new List<UInt16> { 0, 1, 2, 3, 4, 6, 5 }
            },
            {
                ModelFormat.BlendSkin, new List<UInt16> { 0, 1, 2, 3, 4, 6, 5 }
            }
        };

        private enum VectorBatchIndex
        {
            Vertex,
            Color,
            Uv,
            Normal,
            EmitColor,
            JointInfo
        }

        public enum ModelFormat
        {
            Model,
            Skin,
            BlendSkin,
            BlendFace
        }

        public TwinVIFCompiler(ModelFormat format, List<List<Vector4>> data, List<bool> conns)
        {
            this.format = format;
            this.vectorData = data;
            this.conns = conns;
        }

        public Byte[] Compile()
        {
            var interpreter = new VIFInterpreter();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);

            // Blend faces are special in that they are pure packed vector data
            if (format == ModelFormat.BlendFace)
            {
                var packedFaceData = new List<UInt32>();
                interpreter.Pack(vectorData[0], packedFaceData, PackFormat.V4_8);
                foreach (var packedFace in packedFaceData)
                {
                    writer.Write(packedFace);
                }
                var spaceNeeded = packedFaceData.Sum(_ => 4);
                while (spaceNeeded % 0x10 != 0)
                {
                    writer.Write(0U);
                    spaceNeeded += 4;
                }

                writer.Flush();
                ms.Flush();
                return ms.ToArray();
            }
            
            var dmaTag = new DMATag
            {
                ID = DMATag.IdType.RET // Indicates that all transfered data is right next to the DMA tag
            };
            dmaTag.Extra |= 0x00000000070000fa; // The entry is a VIF command called MARK with a value of 0xFA/250
            dmaTag.QWC = 0;
            var dmaTagPosition = writer.BaseStream.Position; // Return at the end to rewrite the amount of QWC in DMA tag
            dmaTag.Write(writer);

            // NOP for later operations for alignment
            VIFCode nop = new()
            {
                OP = VIFCodeEnum.NOP
            };

            // Swizzle the vector data and go over every batch compiling it into its needed format
            var totalSpaceNeeded = 0U;
            var connIndex = 0;
            SwizzleVectorData();
            for (Int32 i = 0; i < swizzledVectorBatches.Count; i++)
            {
                var vectorBatch = swizzledVectorBatches[i];
                var outAddressIndex = 0;
                // Next up is the model descriptor which tells what the format of the model is
                VIFCode modelDescriptorCode = new()
                {
                    OP = VIFCodeEnum.UNPACK,
                    Immediate = outputAddressMap[format][outAddressIndex++],
                    Amount = 1
                };
                modelDescriptorCode.SetUnpackFormat(PackFormat.V4_32);
                modelDescriptorCode.Write(writer);
                var packedMetaVector = new List<UInt32>();
                var modelDescriptor = new Vector4();
                // A GIF tag describing what GS registers to set when sending the model data to GS
                modelDescriptor.SetBinaryX(0x8000 | (UInt32)vectorBatch[GetBatchIndex(VectorBatchIndex.Vertex)].Count); // Set End of packet flag and the amount of loops
                modelDescriptor.SetBinaryY(0x30024000); // Set primitive as triangle strip in PACKED mode with 3 descriptors
                modelDescriptor.SetBinaryZ(0x512); // Use REGS ST, RGBAQ and XYZ2 (in that exact order) as GS outputs
                modelDescriptor.SetBinaryW(0x0);
                var metaVectorList = new List<Vector4>
                {
                    modelDescriptor
                };
                interpreter.Pack(metaVectorList, packedMetaVector, PackFormat.V4_32);
                foreach (var packedV in packedMetaVector)
                {
                    writer.Write(packedV);
                }
                totalSpaceNeeded += modelDescriptorCode.GetLength();

                // After comes STCYCL operation
                VIFCode setCycle = new()
                {
                    OP = VIFCodeEnum.STCYCL,
                    Immediate = 0x0101
                };
                setCycle.Write(writer);
                totalSpaceNeeded += setCycle.GetLength();

                // Next up is the meta data vector packed as V2_32. Purpose unknown
                VIFCode metaVectorCode = new()
                {
                    OP = VIFCodeEnum.UNPACK,
                    Immediate = outputAddressMap[format][outAddressIndex++],
                    Amount = 1
                };
                metaVectorCode.SetUnpackFormat(PackFormat.V2_32);
                metaVectorCode.Write(writer);
                packedMetaVector.Clear();
                var metaVector = new Vector4();
                metaVector.SetBinaryX((UInt32)vectorBatch[GetBatchIndex(VectorBatchIndex.Vertex)].Count * 4);
                metaVector.SetBinaryY(0x8000 | (UInt32)vectorBatch[GetBatchIndex(VectorBatchIndex.Vertex)].Count);
                metaVectorList.Clear();
                metaVectorList.Add(metaVector);
                interpreter.Pack(metaVectorList, packedMetaVector, PackFormat.V2_32);
                foreach (var packedV in packedMetaVector)
                {
                    writer.Write(packedV);
                }
                totalSpaceNeeded += metaVectorCode.GetLength();

                // Scale vector for Twinsanity's skins and blend skins
                // This is mostly correct and what most skins use in Twinsanity but maybe there is some magic to figure this out?
                var scaleVector = new Vector4();
                scaleVector.SetBinaryX(0x38C03F4D);
                scaleVector.SetBinaryY(0x3A000000);

                // For skins and blend skins a special scaling vector comes in
                if (format == ModelFormat.Skin || format == ModelFormat.BlendSkin)
                {
                    VIFCode scaleVectorCode = new()
                    {
                        OP = VIFCodeEnum.UNPACK,
                        Immediate = outputAddressMap[format][outAddressIndex++],
                        Amount = 1
                    };
                    scaleVectorCode.SetUnpackFormat(PackFormat.V2_32);
                    scaleVectorCode.Write(writer);
                    var packedScaleVector = new List<UInt32>();
                    var scaleVectorList = new List<Vector4>
                    {
                        scaleVector
                    };
                    interpreter.Pack(scaleVectorList, packedScaleVector, PackFormat.V2_32);
                    foreach (var packedV in packedScaleVector)
                    {
                        writer.Write(packedV);
                    }
                    totalSpaceNeeded += scaleVectorCode.GetLength();

                    // Additionally add in STMOD and STROW to sets additional decompression write
                    VIFCode turnOnOffsetCode = new()
                    {
                        OP = VIFCodeEnum.STMOD,
                        Immediate = 0x0001
                    };
                    turnOnOffsetCode.Write(writer);
                    totalSpaceNeeded += turnOnOffsetCode.GetLength();

                    // Still not entirely sure how Twinsanity needs this or how to even calculate the values but stays here for now
                    VIFCode setRow = new()
                    {
                        OP = VIFCodeEnum.STROW,
                        Immediate = 0
                    };
                    setRow.Write(writer);
                    writer.Write(0U);
                    writer.Write(0U);
                    writer.Write(0U);
                    writer.Write(0U);
                    totalSpaceNeeded += setRow.GetLength();
                }

                // Next up is STCYCL operation again
                setCycle = new()
                {
                    OP = VIFCodeEnum.STCYCL,
                    Immediate = 0x0104
                };
                setCycle.Write(writer);

                // After comes the actual writing of vectors
                switch (format)
                {
                    // For models there are Vertexes, UVs + Colors + Conns, Normals(optional) and EmitColors(optional)
                    case ModelFormat.Model:
                        {
                            var hasNormals = vectorData.Count == 5;
                            var hasEmitColors = vectorData.Count >= 4;

                            // Write vertexes
                            VIFCode vertexCode = new()
                            {
                                OP = VIFCodeEnum.UNPACK,
                                Immediate = outputAddressMap[format][outAddressIndex++],
                                Amount = (Byte)vectorBatch[GetBatchIndex(VectorBatchIndex.Vertex)].Count
                            };
                            vertexCode.SetUnpackFormat(PackFormat.V3_32);
                            vertexCode.Write(writer);
                            var packedVertexData = new List<UInt32>();
                            interpreter.Pack(vectorBatch[GetBatchIndex(VectorBatchIndex.Vertex)], packedVertexData, PackFormat.V3_32);
                            foreach (var packedVertex in packedVertexData)
                            {
                                writer.Write(packedVertex);
                            }
                            totalSpaceNeeded += vertexCode.GetLength();

                            // Compile the resulting UVs + Colors + Conns
                            var colors = vectorBatch[GetBatchIndex(VectorBatchIndex.Color)].Select(c => c.GetColor()).ToList();
                            var uvs = vectorBatch[GetBatchIndex(VectorBatchIndex.Uv)];
                            var resultBatch = new List<Vector4>(colors.Count);
                            for (Int32 j = 0; j < colors.Count; j++)
                            {
                                var color = colors[j];
                                color.ScaleAlphaDown();
                                var uv = uvs[j];
                                // Undo the reversing of UV
                                uv.Y = 1 - uv.Y;
                                var compiledVector = new Vector4();
                                compiledVector.SetBinaryX(uv.GetBinaryX() | color.R);
                                compiledVector.SetBinaryY(uv.GetBinaryY() | color.G);
                                // Redo it back to make sure we are not modifying the input vector
                                uv.Y = 1 - uv.Y;
                                compiledVector.SetBinaryZ(uv.GetBinaryZ() | color.B);
                                compiledVector.SetBinaryW(color.A | (UInt32)(conns[connIndex++] ? 0x0 : 0x8000));
                                resultBatch.Add(compiledVector);
                            }

                            // Write UVs + Colors + Conns
                            VIFCode uvColorCode = new()
                            {
                                OP = VIFCodeEnum.UNPACK,
                                Immediate = outputAddressMap[format][outAddressIndex++],
                                Amount = (Byte)vectorBatch[GetBatchIndex(VectorBatchIndex.Uv)].Count
                            };
                            uvColorCode.SetUnpackFormat(PackFormat.V4_32);
                            uvColorCode.Write(writer);
                            var packedUvColorData = new List<UInt32>();
                            interpreter.Pack(resultBatch, packedUvColorData, PackFormat.V4_32);
                            foreach (var packedUvColor in packedUvColorData)
                            {
                                writer.Write(packedUvColor);
                            }
                            totalSpaceNeeded += uvColorCode.GetLength();

                            // Write normals if any
                            if (hasNormals)
                            {
                                VIFCode normalsCode = new()
                                {
                                    OP = VIFCodeEnum.UNPACK,
                                    Immediate = outputAddressMap[format][outAddressIndex],
                                    Amount = (Byte)vectorBatch[GetBatchIndex(VectorBatchIndex.Normal)].Count
                                };
                                normalsCode.SetUnpackFormat(PackFormat.V3_32);
                                normalsCode.Write(writer);
                                var packedNormals = new List<UInt32>();
                                interpreter.Pack(vectorBatch[GetBatchIndex(VectorBatchIndex.Normal)], packedNormals, PackFormat.V3_32);
                                foreach (var packedNormal in packedNormals)
                                {
                                    writer.Write(packedNormal);
                                }
                                totalSpaceNeeded += normalsCode.GetLength();
                            }
                            // Increment output address index regardless if normals were present
                            outAddressIndex++;

                            // Write emit colors if any
                            if (hasEmitColors)
                            {
                                VIFCode emitColorsCode = new()
                                {
                                    OP = VIFCodeEnum.UNPACK,
                                    Immediate = outputAddressMap[format][outAddressIndex],
                                    Amount = (Byte)vectorBatch[GetBatchIndex(VectorBatchIndex.EmitColor)].Count
                                };
                                emitColorsCode.SetUnpackFormat(PackFormat.V4_8);
                                emitColorsCode.Write(writer);
                                var packedEmits = new List<UInt32>();
                                interpreter.Pack(vectorBatch[GetBatchIndex(VectorBatchIndex.EmitColor)], packedEmits, PackFormat.V4_8);
                                foreach (var emits in packedEmits)
                                {
                                    writer.Write(emits);
                                }
                                totalSpaceNeeded += emitColorsCode.GetLength();
                            }
                            // Increment output address index regardless if emit colors were present
                            outAddressIndex++;
                        }
                        break;
                    // For skins and blend skins there are Vertexes, UVs, Colors, Joint information + Conns
                    case ModelFormat.Skin:
                    case ModelFormat.BlendSkin:
                        {
                            // Compile vertexes and UVs
                            var compiledPositions = new List<Vector4>(vectorBatch[GetBatchIndex(VectorBatchIndex.Vertex)].Count);
                            var compiledUvs = new List<Vector4>(vectorBatch[GetBatchIndex(VectorBatchIndex.Uv)].Count);
                            for (Int32 j = 0; j < vectorBatch[GetBatchIndex(VectorBatchIndex.Vertex)].Count; j++)
                            {
                                var position = vectorBatch[GetBatchIndex(VectorBatchIndex.Vertex)][j];
                                var uv = vectorBatch[GetBatchIndex(VectorBatchIndex.Uv)][j];
                                var compiledPosition = position.Divide(scaleVector.X);
                                var compiledUv = uv.Divide(scaleVector.Y);
                                compiledPosition.SetBinaryX((UInt32)((Int32)compiledPosition.X));
                                compiledPosition.SetBinaryY((UInt32)((Int32)compiledPosition.Y));
                                compiledPosition.SetBinaryZ((UInt32)((Int32)compiledPosition.Z));
                                compiledPosition.SetBinaryW((UInt32)((Int32)compiledPosition.W));
                                compiledUv.SetBinaryX((UInt32)((Int32)compiledUv.X));
                                compiledUv.SetBinaryY((UInt32)((Int32)compiledUv.Y));
                                compiledUv.SetBinaryZ((UInt32)((Int32)compiledUv.Z));
                                compiledUv.SetBinaryW((UInt32)((Int32)compiledUv.W));
                                compiledPositions.Add(compiledPosition);
                                compiledUvs.Add(compiledUv);
                            }

                            // Write vertexes
                            VIFCode vertexCode = new()
                            {
                                OP = VIFCodeEnum.UNPACK,
                                Immediate = outputAddressMap[format][outAddressIndex++],
                                Amount = (Byte)vectorBatch[GetBatchIndex(VectorBatchIndex.Vertex)].Count
                            };
                            vertexCode.SetUnpackFormat(PackFormat.V4_16);
                            vertexCode.Write(writer);
                            var packedVertexData = new List<UInt32>();
                            interpreter.Pack(compiledPositions, packedVertexData, PackFormat.V4_16);
                            foreach (var packedVertex in packedVertexData)
                            {
                                writer.Write(packedVertex);
                            }
                            totalSpaceNeeded += vertexCode.GetLength();

                            // Set row to all 0s
                            VIFCode setRow = new()
                            {
                                OP = VIFCodeEnum.STROW,
                                Immediate = 0
                            };
                            setRow.Write(writer);
                            writer.Write(0U);
                            writer.Write(0U);
                            writer.Write(0U);
                            writer.Write(0U);
                            totalSpaceNeeded += setRow.GetLength();

                            // Write UVs
                            VIFCode uvCode = new()
                            {
                                OP = VIFCodeEnum.UNPACK,
                                Immediate = outputAddressMap[format][outAddressIndex++],
                                Amount = (Byte)vectorBatch[GetBatchIndex(VectorBatchIndex.Uv)].Count
                            };
                            uvCode.SetUnpackFormat(PackFormat.V4_16);
                            uvCode.Write(writer);
                            var packedUvData = new List<UInt32>();
                            interpreter.Pack(compiledUvs, packedUvData, PackFormat.V4_16);
                            foreach (var packedUv in packedUvData)
                            {
                                writer.Write(packedUv);
                            }
                            totalSpaceNeeded += uvCode.GetLength();

                            // Turn off the offset mode
                            VIFCode turnOffOffsetCode = new()
                            {
                                OP = VIFCodeEnum.STMOD,
                                Immediate = 0x0000
                            };
                            turnOffOffsetCode.Write(writer);
                            totalSpaceNeeded += turnOffOffsetCode.GetLength();

                            // Compile colors
                            var colors = vectorBatch[GetBatchIndex(VectorBatchIndex.Color)].Select(c => c.GetColor()).Select(c =>
                            {
                                c.ScaleAlphaDown();
                                return c;
                            }).ToList();
                            var compiledColors = new List<Vector4>(colors.Count);
                            foreach (var c in colors)
                            {
                                var compiledColor = new Vector4();
                                compiledColor.SetBinaryX(c.R);
                                compiledColor.SetBinaryY(c.G);
                                compiledColor.SetBinaryZ(c.B);
                                compiledColor.SetBinaryW(c.A);
                                compiledColors.Add(compiledColor);
                            }

                            // Write colors
                            VIFCode colorCode = new()
                            {
                                OP = VIFCodeEnum.UNPACK,
                                Immediate = outputAddressMap[format][outAddressIndex++],
                                Amount = (Byte)vectorBatch[GetBatchIndex(VectorBatchIndex.Color)].Count
                            };
                            colorCode.SetUnpackFormat(PackFormat.V4_8);
                            colorCode.Write(writer);
                            var packedColorsData = new List<UInt32>();
                            interpreter.Pack(compiledColors, packedColorsData, PackFormat.V4_8);
                            foreach (var packedColor in packedColorsData)
                            {
                                writer.Write(packedColor);
                            }
                            totalSpaceNeeded += colorCode.GetLength();

                            // Write joint information
                            VIFCode jointCode = new()
                            {
                                OP = VIFCodeEnum.UNPACK,
                                Immediate = outputAddressMap[format][outAddressIndex++],
                                Amount = (Byte)vectorBatch[GetBatchIndex(VectorBatchIndex.JointInfo)].Count
                            };
                            jointCode.SetUnpackFormat(PackFormat.V4_32);
                            jointCode.Write(writer);
                            var packedJointData = new List<UInt32>();
                            interpreter.Pack(vectorBatch[GetBatchIndex(VectorBatchIndex.JointInfo)], packedJointData, PackFormat.V4_32);
                            foreach (var packedJoint in packedJointData)
                            {
                                writer.Write(packedJoint);
                            }
                            totalSpaceNeeded += jointCode.GetLength();
                        }
                        break;
                    default:
                        throw new Exception("Unexisting model type provided");
                }

                VIFCode mscal = new()
                {
                    OP = VIFCodeEnum.MSCAL,
                    Immediate = 0x0
                };
                mscal.Write(writer);
                totalSpaceNeeded += mscal.GetLength();

                setCycle = new()
                {
                    OP = VIFCodeEnum.STCYCL,
                    Immediate = 0x0101
                };
                setCycle.Write(writer);
                totalSpaceNeeded += setCycle.GetLength();
            }

            // Align to 16(0x10) bytes
            while (totalSpaceNeeded % 0x10 != 0)
            {
                nop.Write(writer);
                totalSpaceNeeded += nop.GetLength();
            }

            // As a cherry on top tell DMA tag how many QWCs we are gonna transfer
            /*dmaTag.QWC = (UInt16)(totalSpaceNeeded / 0x10);
            writer.BaseStream.Position = dmaTagPosition;
            dmaTag.Write(writer);*/

            // Finally flush all the operations and return the byte code compiled model
            writer.Flush();
            ms.Flush();
            var result = ms.ToArray();
            dmaTag.QWC = (UInt16)(result.Length / 0x10 - 1);
            writer.BaseStream.Position = dmaTagPosition;
            dmaTag.Write(writer);
            writer.Flush();
            ms.Flush();
            return ms.ToArray();
        }

        private Int32 GetBatchIndex(VectorBatchIndex index)
        {
            return format switch
            {
                ModelFormat.Model => index switch
                {
                    VectorBatchIndex.JointInfo => throw new NotImplementedException(),
                    _ => (Int32)index
                },
                ModelFormat.Skin or ModelFormat.BlendSkin => index switch
                {
                    VectorBatchIndex.Vertex => (Int32)index,
                    VectorBatchIndex.Color => 2,
                    VectorBatchIndex.Uv => 1,
                    VectorBatchIndex.JointInfo => 3,
                    _ => throw new NotImplementedException()
                },
                _ => throw new NotImplementedException()
            };
        }

        private void SwizzleVectorData()
        {
            var vertexAmount = vectorData[0].Count;
            Debug.Assert(vectorData.All(l => l.Count == vertexAmount), "Must swizzle equal amount of vertexes");
            // TODO: Better swizzling heuristic
            var swizzleAmount = 37;
            var vertexIndex = 0;
            var leftOver = vertexAmount % swizzleAmount;
            var swizzleCount = vertexAmount / swizzleAmount;
            Debug.Assert(swizzleCount * swizzleAmount + leftOver == vertexAmount, "Didn't properly calculate the proper amount of batches");

            // Create new batch
            swizzledVectorBatches = new();
            for (Int32 i = 0; i < swizzleCount - 1; i++)
            {
                // Create new list for the batch
                swizzledVectorBatches.Add(new());

                // Create new list for each type of vertex data
                for (Int32 j = 0; j < vectorData.Count; j++)
                {
                    swizzledVectorBatches[^1].Add(new());
                }

                // Fill the batch with each vertex type
                for (Int32 j = 0; j < swizzleAmount; j++)
                {
                    for (Int32 k = 0; k < vectorData.Count; k++)
                    {
                        swizzledVectorBatches[^1][k].Add(vectorData[k][vertexIndex + j]);
                    }
                    vertexIndex++;
                }
            }

            // Fill the leftover batch

            // Create new list for the batch
            swizzledVectorBatches.Add(new());

            // Create new list for each type of vertex data
            for (Int32 j = 0; j < vectorData.Count; j++)
            {
                swizzledVectorBatches[^1].Add(new());
            }

            // Fill the batch with each vertex type
            for (Int32 j = 0; j < leftOver; j++)
            {
                for (Int32 k = 0; k < vectorData.Count; k++)
                {
                    swizzledVectorBatches[^1][k].Add(vectorData[k][vertexIndex + j]);
                }
                vertexIndex++;
            }
        }
    }
}