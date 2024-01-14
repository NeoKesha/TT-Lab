using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.Extensions;
using TT_Lab.MeshProcessor;
using Twinsanity.PS2Hardware;

namespace TT_Lab.Libraries
{
    public unsafe static class MeshOptimizer
    {
        public static List<Meshlet> BuildMeshlets(List<UInt32> indices, List<Vertex> positions, List<SubBlendFaceData>? blendFaces = null)
        {
            nuint maxVertecies = 128;
            nuint maxTriangles = (nuint)(TwinVIFCompiler.VertexStripCache / 3);

            var indicesArray = indices.ToArray();
            var meshletAmount = Meshoptimizer.Meshopt.BuildMeshletsBound((nuint)indices.Count, maxVertecies, maxTriangles);
            var vertexAccessor = new uint[meshletAmount.ToUInt32() * maxVertecies.ToUInt32()];
            var trianglesAccessor = new byte[vertexAccessor.Length * meshletAmount.ToUInt32() * 3];
            var meshlets = new Meshoptimizer.Meshopt.Meshlet[meshletAmount];
            var positionsGl = positions.Select(p => p.Position.ToGL());
            var positionsArray = new float[positionsGl.Count() * 3];
            var index = 0;
            foreach (var position in positionsGl)
            {
                positionsArray[index++] = position.X;
                positionsArray[index++] = position.Y;
                positionsArray[index++] = position.Z;
            }

            var meshletsBuilt = 0U;
            fixed (Meshoptimizer.Meshopt.Meshlet* meshletsNative = meshlets)
            {
                fixed (uint* maxVerteciesNative = vertexAccessor)
                {
                    fixed (byte* maxTrianglesNative = trianglesAccessor)
                    {
                        fixed (uint* indicesNative = indicesArray)
                        {
                            fixed (float* positionsNative = positionsArray)
                            {
                                meshletsBuilt = Meshoptimizer.Meshopt.BuildMeshlets(
                                    ref *meshletsNative,
                                    in *maxVerteciesNative,
                                    in *maxTrianglesNative,
                                    in *indicesNative,
                                    (nuint)indicesArray.Length,
                                    in *positionsNative,
                                    (nuint)positions.Count,
                                    12,
                                    maxVertecies,
                                    maxTriangles,
                                    0.0f).ToUInt32();
                            }
                        }
                    }
                }
            }
            
            var result = new List<Meshlet>();

            for (var i = 0; i < meshletsBuilt; ++i)
            {
                var meshlet = new Meshlet();
                var nativeMeshlet = meshlets[i];

                if (blendFaces != null)
                {
                    meshlet.BlendFaces = new List<SubBlendFaceData>();
                    foreach (var blendFace in blendFaces)
                    {
                        var newFace = new SubBlendFaceData();
                        for (var j = 0; j < nativeMeshlet.VertexCount; ++j)
                        {
                            var vertex = vertexAccessor[j + nativeMeshlet.VertexOffset];
                            var shape = blendFace.BlendShapes[(Int32)vertex];
                            newFace.BlendShapes.Add(shape);
                        }
                        newFace.VertexesAmount = (UInt32)newFace.BlendShapes.Count;
                        meshlet.BlendFaces.Add(newFace);
                    }
                }

                for (var j = 0; j < nativeMeshlet.TriangleCount * 3; j += 3)
                {
                    var idx1 = trianglesAccessor[j + nativeMeshlet.TriangleOffset];
                    var idx2 = trianglesAccessor[j + nativeMeshlet.TriangleOffset + 1];
                    var idx3 = trianglesAccessor[j + nativeMeshlet.TriangleOffset + 2];
                    meshlet.Indices.Add(idx1);
                    meshlet.Indices.Add(idx2);
                    meshlet.Indices.Add(idx3);
                }

                for (var j = 0; j < nativeMeshlet.VertexCount; ++j)
                {
                    var vertex = vertexAccessor[j + nativeMeshlet.VertexOffset];
                    meshlet.Vertexes.Add(positions[(Int32)vertex]);
                }

                result.Add(meshlet);
            }

            return result;
        }

        public static List<UInt32> Stripify(List<UInt32> indices, UInt32 vertexAmount)
        {
            var maxNeededLength = Meshoptimizer.Meshopt.StripifyBound((nuint)indices.Count);
            var destination = new uint[maxNeededLength];
            var indicesArray = indices.ToArray();

            var stripifiedIndices = 0U;
            fixed (uint* arrayStart = indicesArray)
            {
                fixed (uint* destinationStart = destination)
                {
                    nuint indicesCountNative = (nuint)indicesArray.Length;
                    stripifiedIndices = Meshoptimizer.Meshopt.Stripify(ref *destinationStart, in *arrayStart, indicesCountNative, vertexAmount, 0xFFFF).ToUInt32();
                }
            }

            var result = new List<UInt32>();
            for (var i = 0; i < stripifiedIndices; ++i)
            {
                result.Add(destination[i]);
            }

            return result;
        }
    }
}
