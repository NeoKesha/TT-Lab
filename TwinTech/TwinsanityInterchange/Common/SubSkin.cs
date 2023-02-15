using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class SubSkin : ITwinSerializable
    {
        public UInt32 Material;
        private Int32 BlobSize;
        private Int32 VertexAmount;
        private Byte[] VifCode;
        public List<Vector4> Vertexes { get; set; }
        public List<Vector4> UVW { get; set; }
        public List<Vector4> EmitColor { get; set; }
        public List<Vector4> VertexAdjustments { get; set; }
        public List<bool> Connection { get; set; }
        public List<Vector4> DecompressedData { get; set; }

        public int GetLength()
        {
            return 12 + VifCode.Length;
        }

        public void Read(BinaryReader reader, int length)
        {
            Material = reader.ReadUInt32();
            BlobSize = reader.ReadInt32();
            VertexAmount = reader.ReadInt32();
            VifCode = reader.ReadBytes(BlobSize);
        }
        public void CalculateData()
        {
            var interpreter = VIFInterpreter.InterpretCode(VifCode);
            var data = interpreter.GetMem();

            DecompressedData = new();

            foreach (var d in data)
            {
                foreach (var v in d)
                {
                    if (v == null)
                        break;
                    DecompressedData.Add(v);
                }
            }

            // Skin vector processing step 1
            /*var dataIndex = 3;
            var offsetIndex = pureData[0x1].GetBinaryX();
            var scaleVecX = pureData[0x2].X;
            var scaleVecY = pureData[0x2].Y;
            offsetIndex += (uint)dataIndex;
            const Int32 mask = 0x3;
            const Int32 mask2 = 0x1FF;
            var batch1_index = dataIndex + 0x4;
            var batch2_index = dataIndex + 0x8;
            var batch3_index = dataIndex + 0xC;
            offsetIndex += 0xC;
            while (true)
            {
                List<Vector4> resVec;
                ReadAndConvert(dataIndex, pureData, out resVec);
                ScaleVectors(scaleVecX, scaleVecY, ref resVec);
                dataIndex += 0x10;
                pureData[dataIndex - 0x10] = resVec[0];
                pureData[dataIndex - 0xF] = resVec[1];
                if (offsetIndex == dataIndex)
                {
                    break;
                }
                batch1_index += 0x10;
                pureData[batch1_index - 0x10] = resVec[2];
                pureData[batch1_index - 0xF] = resVec[3];
                if (offsetIndex == batch1_index)
                {
                    break;
                }
                batch2_index+= 0x10;
                pureData[batch2_index - 0x10] = resVec[4];
                pureData[batch2_index - 0xF] = resVec[5];
                if (offsetIndex == batch2_index)
                {
                    break;
                }
                batch3_index+= 0x10;
                pureData[batch3_index - 0x10] = resVec[6];
                pureData[batch3_index - 0xF] = resVec[7];
                if (offsetIndex == batch3_index)
                {
                    break;
                }
            }


            // Step 2
            dataIndex = 3;
            offsetIndex = pureData[1].GetBinaryX();
            offsetIndex += (uint)dataIndex;
            var vectors = new Vector4[14];
            vectors[0] = new Vector4(pureData[dataIndex + 0x2]);
            vectors[1] = new Vector4(pureData[dataIndex]);
            vectors[2] = new Vector4(pureData[dataIndex + 0x1]);

            var unkValue = vectors[0].GetBinaryW() & 0xFFFF;
            var unkValue2 = unkValue;
            unkValue &= mask;
            var unkMatrixAddress = vectors[0].GetBinaryX() & 0xFFFF;
            unkMatrixAddress &= mask2;
            Console.WriteLine($"Unknown matrix address at the VU memory {unkMatrixAddress}");

            vectors[3] = vectors[1].Multiply(vectors[0].X);
            vectors[4] = vectors[1].Multiply(vectors[0].Y);
            vectors[5] = vectors[1].Multiply(vectors[0].Z);
            vectors[6] = new Vector4(vectors[0]);

            while (true)
            {
                // Use identity matrix because I am clueless what the exact matrix unkMatrixAddress points to
                var matrix = new Matrix4();
                matrix[0].X = 1.0f;
                matrix[1].Y = 1.0f;
                matrix[2].Z = 1.0f;
                matrix[3].W = 1.0f;
                matrix[3].X = 1.0f;
                matrix[3].Y = 1.0f;
                matrix[3].Z = 1.0f;

                vectors[7] = new Vector4(vectors[2]);
                vectors[8] = new Vector4(vectors[3]);
                var counter = 1;
                var unkMatrixAddress2 = vectors[0].GetBinaryY() & 0xFFFF;
                unkMatrixAddress2 &= mask2;
                Console.WriteLine($"Unknown matrix 2 address at the VU memory {unkMatrixAddress2}");
                var acc = matrix[0].Multiply(vectors[1].W);
                acc += matrix[1].Multiply(vectors[7].Z);
                vectors[9] = acc + matrix[2].Multiply(vectors[7].W);
                acc = matrix[3].Multiply(vectors[6].X);
                acc += matrix[0].Multiply(vectors[3].X);
                acc += matrix[1].Multiply(vectors[3].Y);
                
                if (counter == unkValue)
                {
                    acc += matrix[0].Multiply(vectors[8].X);
                    acc += matrix[1].Multiply(vectors[8].Y);
                    vectors[12] = acc + matrix[2].Multiply(vectors[8].Z);

                    vectors[0] = new Vector4(pureData[dataIndex + 0x6]);
                    vectors[1] = new Vector4(pureData[dataIndex + 0x4]);
                    vectors[2] = new Vector4(pureData[dataIndex + 0x5]);

                    unkValue = vectors[0].GetBinaryW() & 0xFFFF;
                    dataIndex += 0x4;

                    vectors[7].Z = 1.0f;
                    vectors[13] = new Vector4(vectors[9]);
                    vectors[13].SetBinaryW(unkValue2);

                    pureData[dataIndex - 0x4] = new Vector4(vectors[12]);
                    pureData[dataIndex - 0x3] = new Vector4(vectors[7]);
                    pureData[dataIndex - 0x2] = new Vector4(vectors[13]);
                    vectors[3] = vectors[1].Multiply(vectors[0].X);
                    vectors[4] = vectors[1].Multiply(vectors[0].Y);
                    vectors[5] = vectors[1].Multiply(vectors[0].Z);
                    vectors[6] = new Vector4(vectors[0]);

                    unkValue2 = unkValue;
                    unkValue &= mask;
                    unkMatrixAddress = vectors[0].GetBinaryX() & 0xFFFF;
                    unkMatrixAddress &= mask2;
                    Console.WriteLine($"Unknown matrix address at the VU memory {unkMatrixAddress}");
                    if (dataIndex == offsetIndex)
                    {
                        break;
                    }
                    continue;
                }
                counter++;
                var matrix2 = matrix;
                vectors[10] = acc + matrix[2].Multiply(vectors[3].Z);
                vectors[11] = new Vector4(vectors[4]);
                acc = matrix2[3].Multiply(vectors[6].Y);
                acc += matrix2[0].Multiply(vectors[4].X);
                acc += vectors[9];
                counter++;
                var unkMatrixAddress3 = vectors[0].GetBinaryZ() & 0xFFFF;
                if (counter != unkValue)
                {
                    unkMatrixAddress3 &= mask2;
                    Console.WriteLine($"Unknown matrix 2 address at the VU memory {unkMatrixAddress3}");
                    break;
                }
                acc += matrix2[1].Multiply(vectors[4].Y);
                vectors[12] = acc + matrix2[2].Multiply(vectors[4].Z);
                vectors[0] = new Vector4(pureData[dataIndex + 0x6]);
                vectors[1] = new Vector4(pureData[dataIndex + 0x4]);
                vectors[2] = new Vector4(pureData[dataIndex + 0x5]);

                unkValue = vectors[0].GetBinaryW() & 0xFFFF;
                dataIndex += 0x4;

                vectors[7].Z = 1.0f;
                vectors[13] = new Vector4(vectors[9]);
                vectors[13].SetBinaryW(unkValue2);

                pureData[dataIndex - 0x4] = new Vector4(vectors[12]);
                pureData[dataIndex - 0x3] = new Vector4(vectors[7]);
                pureData[dataIndex - 0x2] = new Vector4(vectors[13]);
                vectors[3] = vectors[1].Multiply(vectors[0].X);
                vectors[4] = vectors[1].Multiply(vectors[0].Y);
                vectors[5] = vectors[1].Multiply(vectors[0].Z);
                vectors[6] = new Vector4(vectors[0]);

                unkValue2 = unkValue;
                unkValue &= mask;
                unkMatrixAddress = vectors[0].GetBinaryX() & 0xFFFF;
                unkMatrixAddress &= mask2;
                Console.WriteLine($"Unknown matrix address at the VU memory {unkMatrixAddress}");
                if (dataIndex == offsetIndex)
                {
                    break;
                }
            }*/

            Vertexes = new List<Vector4>();
            UVW = new List<Vector4>();
            EmitColor = new List<Vector4>();
            VertexAdjustments = new List<Vector4>();
            Connection = new List<bool>();

            const int VERT_DATA_INDEX = 3;
            for (int i = 0; i < data.Count;)
            {
                var verts = (data[i][0].GetBinaryX() & 0xFF);
                var fields = (data[i + 1][0].GetBinaryX() & 0xFF) / verts;
                var scaleVec = data[i + 2][0];
                Console.WriteLine($"Verts in this subskin block {verts}");
                Console.WriteLine($"Fields in this subskin block {fields}");
                Console.WriteLine($"Scale vector for this subskin block ({scaleVec.X};{scaleVec.Y})");
                var vertex_batch_1 = data[i + VERT_DATA_INDEX];
                var vertex_batch_2 = data[i + VERT_DATA_INDEX + 1];
                var vertex_batch_3 = data[i + VERT_DATA_INDEX + 3];
                var vertex_batch_4 = data[i + VERT_DATA_INDEX + 2];
                // Vertex conversion
                for (int j = 0; j < verts; ++j)
                {
                    var v1 = new Vector4(vertex_batch_1[j]);
                    var v2 = new Vector4(vertex_batch_2[j]);
                    v1.X = (int)v1.GetBinaryX();
                    v1.Y = (int)v1.GetBinaryY();
                    v1.Z = (int)v1.GetBinaryZ();
                    v1.W = (int)v1.GetBinaryW();
                    v2.X = (int)v2.GetBinaryX();
                    v2.Y = (int)v2.GetBinaryY();
                    v2.Z = (int)v2.GetBinaryZ();
                    v2.W = (int)v2.GetBinaryW();
                    v1 = v1.Multiply(scaleVec.X);
                    v2 = v2.Multiply(scaleVec.Y);
                    vertex_batch_1[j] = v1;
                    vertex_batch_2[j] = v2;
                }

                // Second conversion step
                // Use identity matrix because I am clueless what the exact matrix unkMatrixAddress points to
                var matrix = new Matrix4();
                matrix[0].X = 1.0f;
                matrix[1].Y = 1.0f;
                matrix[2].Z = 1.0f;
                matrix[3].W = 1.0f;
                matrix[3].X = 1.0f;
                matrix[3].Y = 1.0f;
                matrix[3].Z = 1.0f;

                const Int32 mask = 0x3;
                const Int32 mask2 = 0x1FF;

                for (int j = 0; j < verts; ++j)
                {
                    var v1 = vertex_batch_3[j];

                    var unkValue = v1.GetBinaryW() & 0xFFFF;
                    Connection.Add((unkValue >> 8) != 128);
                    /*var unkValue2 = unkValue;
                    unkValue &= mask;
                    var unkMatrixAddress = v1.GetBinaryX() & 0xFFFF;
                    unkMatrixAddress &= mask2;
                    Console.WriteLine($"Unknown matrix address at the VU memory 0x{unkMatrixAddress:x}");

                    var acc = matrix[0].Multiply(v2.W);
                    acc += matrix[1].Multiply(vectors[7].Z);
                    vectors[9] = acc + matrix[2].Multiply(vectors[7].W);
                    acc = matrix[3].Multiply(vectors[6].X);
                    acc += matrix[0].Multiply(vectors[3].X);
                    acc += matrix[1].Multiply(vectors[3].Y);
                    var counter = 1;
                    if (counter == unkValue)
                    {
                        acc += matrix[0].Multiply(vectors[8].X);
                        acc += matrix[1].Multiply(vectors[8].Y);
                        vectors[12] = acc + matrix[2].Multiply(vectors[8].Z);

                        vectors[7].Z = 1.0f;
                        vectors[13] = new Vector4(vectors[9]);
                        vectors[13].SetBinaryW(unkValue2);

                        vertex_batch_1[j] = vectors[12];
                        vertex_batch_2[j] = vectors[7];
                        vertex_batch_3[j] = vectors[13];
                        continue;
                    }
                    var matrix2 = matrix;
                    vectors[10] = acc + matrix[2].Multiply(vectors[3].Z);
                    vectors[11] = new Vector4(vectors[4]);
                    acc = matrix2[3].Multiply(vectors[6].Y);
                    acc += matrix2[0].Multiply(vectors[4].X);
                    acc += vectors[9];
                    acc += matrix2[1].Multiply(vectors[4].Y);
                    vectors[12] = acc + matrix2[2].Multiply(vectors[4].Z);
                    vectors[7].Z = 1.0f;
                    vectors[13] = new Vector4(vectors[9]);
                    vectors[13].SetBinaryW(unkValue2);

                    vertex_batch_1[j] = vectors[12];
                    vertex_batch_2[j] = vectors[7];
                    vertex_batch_3[j] = vectors[13];*/
                }

                // Third conversion step
                /*const float VALUE = 255.0f;
                Vector4 cVec0 = new Vector4(0, 0, 0, 1);
                Vector4 cVec1 = new Vector4(7418.4697f, -0.0039f, -0.2166f, 0);
                Vector4 cVec2 = new Vector4(-8232.9033f, -17806.9199f, 280.75f, -0.2512f);
                Vector4 cVec3 = new Vector4(31716.8906f, 29231.7207f, -1081.8789f, 0.9679f);
                Vector4 cVec4 = new Vector4(266657.875f, 291554.4375f, 1668766.125f, 8.1378f);
                Vector4 cVec5 = new Vector4(0.5725f, -0.9969f, 0.5925f, 0);
                Vector4 cVec6 = new Vector4(0.8192f, -0.0000f, 0.8192f, 0);
                Vector4 cVec7 = new Vector4(0.0347f, -0.0784f, 0.0347f, 0);
                Vector4 cVec8 = new Vector4(1.5f, 1.1867f, 0, 0);
                Vector4 cVec9 = new Vector4(0.6087f, 0.6648f, 1.25f, 0);
                Vector4 cVec10 = new Vector4(0, 0, 0, 1);
                Vector4 cVec11 = new Vector4(0.75f, 0.75f, 0.75f, 1);
                var unkVec = new Vector4(vertex_batch_4[0].GetBinaryX(), vertex_batch_4[0].GetBinaryY(), vertex_batch_4[0].GetBinaryZ(), vertex_batch_4[0].GetBinaryW());
                var unkUvCalc = new Vector4(vertex_batch_1[0]);
                for (int j = 0; j < verts; j++)
                {
                    var acc = new Vector4(cVec4);
                    var iUvCoords = new Vector4(vertex_batch_1[j]);
                    var moreUvCoords = new Vector4(vertex_batch_2[j]);
                    var adjustVector = new Vector4(vertex_batch_3[j]);
                    var vertCoords = new Vector4(vertex_batch_4[j]);
                    unkVec.X *= unkUvCalc.X;
                    unkVec.Y *= unkUvCalc.Y;
                    unkVec.Z *= unkUvCalc.Z;
                    acc += cVec2.Multiply(iUvCoords.Y);
                    acc += cVec3.Multiply(iUvCoords.Z);
                    var uvCoords = acc + cVec1.Multiply(iUvCoords.X);
                    acc = cVec5.Multiply(adjustVector.X);
                    acc += cVec6.Multiply(adjustVector.Y);
                    adjustVector = acc + cVec7.Multiply(adjustVector.Z);
                    var swap = new Vector4(vertCoords);
                    vertCoords = new Vector4(unkVec);
                    unkVec = new Vector4(swap.GetBinaryX(), swap.GetBinaryY(), swap.GetBinaryZ(), swap.GetBinaryW());
                    var resColorVec = new Vector4(Math.Min(vertCoords.X, VALUE), Math.Min(vertCoords.Y, VALUE), Math.Min(vertCoords.Z, VALUE), Math.Min(vertCoords.W, VALUE));
                    var colorVec = new Vector4(resColorVec);
                    resColorVec = new Vector4(Math.Max(adjustVector.X, 0), Math.Max(adjustVector.Y, 0), Math.Max(adjustVector.Z, 0), Math.Max(adjustVector.W, 0));
                    acc = cVec11.Multiply(cVec0.W);
                    acc += cVec8.Multiply(resColorVec.X);
                    acc += cVec9.Multiply(resColorVec.Y);
                    unkUvCalc = acc + cVec10.Multiply(resColorVec.Z);
                    vertex_batch_4[j] = colorVec;
                    var Q = cVec0.W / uvCoords.W;
                    vertCoords = uvCoords.Multiply(Q);
                    uvCoords = moreUvCoords.Multiply(Q);
                    vertex_batch_1[j] = uvCoords;
                    vertCoords.W = adjustVector.W;
                    vertCoords.X = FixedToFloat((int)vertCoords.GetBinaryX());
                    vertCoords.Y = FixedToFloat((int)vertCoords.GetBinaryY());
                    vertCoords = new Vector4(vertCoords.X, vertCoords.Y, vertCoords.Z, vertCoords.W);
                    vertex_batch_2[j] = vertCoords;
                }*/

                // Save the batches into their respective vertex parts
                for (int j = 0; j < verts; j++)
                {
                    Vertexes.Add(vertex_batch_1[j]);
                    UVW.Add(vertex_batch_2[j]);
                    EmitColor.Add(vertex_batch_4[j]);
                    VertexAdjustments.Add(vertex_batch_4[j]);
                }
                i += (int)fields + 3;
            }
            //Vertexes = new List<Vector4>();
            //UVW = new List<Vector4>();
            //EmitColor = new List<Vector4>();
            //VertexAdjustments = new List<Vector4>();
            //Connection = new List<bool>();
            //for (var i = 0; i < data.Count;)
            //{
            //    var verts = (data[i][0].GetBinaryX() & 0xFF);
            //    var fields = 0;
            //    while (data[i + 3 + fields].Count == verts)
            //    {
            //        ++fields;
            //        if (i + fields + 3 >= data.Count)
            //        {
            //            break;
            //        }
            //    }
            //    var vertexes = data[i + 3];
            //    var divNum = 65536f / 2;
            //    // Verts
            //    foreach (var v in vertexes)
            //    {
            //        var comp_x = (short)(v.GetBinaryX() & 0xFFFF);
            //        var comp_y = (short)(v.GetBinaryY() & 0xFFFF);
            //        var comp_z = (short)(v.GetBinaryZ() & 0xFFFF);
            //        var comp_w = (short)(v.GetBinaryW() & 0xFFFF);
            //        Vertexes.Add(new Vector4(comp_x / divNum,
            //            comp_y / divNum,
            //            comp_z / divNum,
            //            comp_w / divNum));
            //    }
            //    // UVs
            //    if (fields > 1)
            //    {
            //        UVW.AddRange(data[i + 4].Select(v => new Vector4((v.GetBinaryX() & 0xFFFF) / divNum,
            //            (v.GetBinaryY() & 0xFFFF) / divNum,
            //            (v.GetBinaryZ() & 0xFFFF) / divNum,
            //            (v.GetBinaryW() & 0xFFFF) / divNum)).ToList());
            //    }
            //    // Emit color
            //    if (fields > 2)
            //    {
            //        foreach (var e in data[i + 5])
            //        {
            //            Vector4 emit = new Vector4(e);
            //            emit.X = (emit.X + 126.0f) / 255.0f;
            //            emit.Y = (emit.Y + 126.0f) / 255.0f;
            //            emit.Z = (emit.Z + 126.0f) / 255.0f;
            //            emit.W = (emit.W + 126.0f) / 255.0f;
            //            EmitColor.Add(emit);
            //        }
            //    }
            //    // Vertex adjustments
            //    if (fields > 3)
            //    {
            //        var adjustments_conn = data[i + 6];
            //        foreach (var e in adjustments_conn)
            //        {
            //            var conn = (e.GetBinaryW() & 0xFF00) >> 8;
            //            Connection.Add(conn != 128);
            //            var comp_c = e.GetBinaryW() & 0x00FF;
            //            var x = e.X;
            //            var y = e.Y;
            //            var z = e.Z;
            //            var v = new Vector4(1f, 1f, 1f, 1f);
            //            switch (comp_c)
            //            {
            //                case 1:
            //                    v.X = x;
            //                    break;
            //                case 2:
            //                    v.X = x;
            //                    v.Y = y;
            //                    break;
            //                case 3:
            //                    v.X = x;
            //                    v.Y = y;
            //                    v.Z = z;
            //                    break;
            //                default:
            //                    Console.WriteLine("[WARNING] Somehow there are more than 3 components?");
            //                    break;
            //            }
            //            VertexAdjustments.Add(v);
            //        }
            //    }
            //    i += fields + 3;
            //    TrimList(UVW, Vertexes.Count);
            //    TrimList(EmitColor, Vertexes.Count);
            //    TrimList(VertexAdjustments, Vertexes.Count);
            //}
        }
        public void Write(BinaryWriter writer)
        {
            writer.Write(Material);
            writer.Write(BlobSize);
            writer.Write(VertexAmount);
            writer.Write(VifCode);
        }
        private float FixedToFloat(int value)
        {
            var shortVal = (short)(value >> 16);
            var c = Math.Abs(shortVal);
            var sign = 1;
            if (shortVal < 0)
            {
                c = (short)(shortVal - 1);
                c = (short)~c;
                sign = -1;
            }
            var f = (1.0f * c) / 16f;
            f *= sign;
            return f;
        }
        private void ReadAndConvert(int dataOffset, List<Vector4> memory, out List<Vector4> vectors)
        {
            var resVec = new List<Vector4>
            {
                new Vector4(memory[dataOffset]), new Vector4(memory[dataOffset + 1]),
                new Vector4(memory[dataOffset + 4]), new Vector4(memory[dataOffset + 5]),
                new Vector4(memory[dataOffset + 8]), new Vector4(memory[dataOffset + 9]),
                new Vector4(memory[dataOffset + 0xC]), new Vector4(memory[dataOffset + 0xD]),
            };
            foreach (var v in resVec)
            {
                v.X = v.GetBinaryX();
                v.Y = v.GetBinaryY();
                v.Z = v.GetBinaryZ();
                v.W = v.GetBinaryW();
            }
            vectors = resVec;
        }
        private void ScaleVectors(float scaleX, float scaleY, ref List<Vector4> vectors)
        {
            vectors[0].X *= scaleX;
            vectors[0].Y *= scaleX;
            vectors[0].Z *= scaleX;
            vectors[0].W *= scaleX;
            vectors[1].X *= scaleY;
            vectors[1].Y *= scaleY;
            vectors[1].Z *= scaleY;
            vectors[1].W *= scaleY;
            vectors[2].X *= scaleX;
            vectors[2].Y *= scaleX;
            vectors[2].Z *= scaleX;
            vectors[2].W *= scaleX;
            vectors[3].X *= scaleY;
            vectors[3].Y *= scaleY;
            vectors[3].Z *= scaleY;
            vectors[3].W *= scaleY;
            vectors[4].X *= scaleX;
            vectors[4].Y *= scaleX;
            vectors[4].Z *= scaleX;
            vectors[4].W *= scaleX;
            vectors[5].X *= scaleY;
            vectors[5].Y *= scaleY;
            vectors[5].Z *= scaleY;
            vectors[5].W *= scaleY;
            vectors[6].X *= scaleX;
            vectors[6].Y *= scaleX;
            vectors[6].Z *= scaleX;
            vectors[6].W *= scaleX;
            vectors[7].X *= scaleY;
            vectors[7].Y *= scaleY;
            vectors[7].Z *= scaleY;
            vectors[7].W *= scaleY;
        }
        private void TrimList(List<Vector4> list, Int32 desiredLength, Vector4 defaultValue = null)
        {
            if (list != null)
            {
                if (list.Count > desiredLength)
                {
                    list.RemoveRange(desiredLength, list.Count - desiredLength);
                }
                while (list.Count < desiredLength)
                {
                    if (defaultValue != null)
                    {
                        list.Add(new Vector4(defaultValue));
                    }
                    else
                    {
                        list.Add(new Vector4());
                    }
                }
            }
        }
    }
}
