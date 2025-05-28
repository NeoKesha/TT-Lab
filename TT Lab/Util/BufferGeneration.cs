using GlmSharp;
using org.ogre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using TT_Lab.AssetData.Graphics.SubModels;
using Twinsanity.TwinsanityInterchange.Common;
using Color = System.Drawing.Color;
using Vector3 = org.ogre.Vector3;

namespace TT_Lab.Util
{
    public static class BufferGeneration
    {
        public static MeshPtr GetModelBuffer(string name, List<Vertex> vertexes, List<IndexedFace> faces, RenderOperation.OperationType renderStyle = RenderOperation.OperationType.OT_TRIANGLE_LIST, bool generateUvs = true, bool useSkinning = false)
        {
            var positions = vertexes.Select(v => new vec3(v.Position.X, v.Position.Y, v.Position.Z)).ToList();
            var colors = vertexes.Select((v) =>
            {
                var col = v.Color.GetColor();
                if (!v.HasEmitColor)
                {
                    if (useSkinning)
                    {
                        col.ScaleAlphaUp();
                    }

                    return Color.FromArgb((int)col.ToARGB());
                }
                
                var emitCol = v.EmitColor.GetColor();
                col.R = (Byte)System.Math.Min(col.R + emitCol.R, 255);
                col.G = (Byte)System.Math.Min(col.G + emitCol.G, 255);
                col.B = (Byte)System.Math.Min(col.B + emitCol.B, 255);
                col.A = (Byte)System.Math.Min(col.A + emitCol.A, 255);
                return Color.FromArgb((int)col.ToARGB());
            }).ToList();

            if (useSkinning)
            {
                var joints = vertexes.Select(v => v.JointInfo).ToList();
                return GetModelBuffer(name, positions, faces,
                    colors,
                    renderStyle,
                    vertexes.Select(v => new vec2(v.UV.X, v.UV.Y)).ToList(),
                    null, null,
                    joints);
            }

            if (generateUvs)
            {
                if (vertexes.Any(v => v.HasNormals))
                {
                    return GetModelBuffer(name, positions, faces,
                        colors,
                        renderStyle,
                        vertexes.Select(v => new vec2(v.UV.X, v.UV.Y)).ToList(),
                        vertexes.Select(v => new vec4(v.Normal.X, v.Normal.Y, v.Normal.Z, v.Normal.W)).ToList());
                }

                return GetModelBuffer(name, positions, faces,
                    colors, renderStyle,
                    vertexes.Select(v => new vec2(v.UV.X, v.UV.Y)).ToList());
            }

            if (vertexes.Any(v => v.HasNormals))
            {
                return GetModelBuffer(name, positions, faces,
                    colors, renderStyle, null,
                    vertexes.Select(v => new vec4(v.Normal.X, v.Normal.Y, v.Normal.Z, v.Normal.W)).ToList());
            }

            return GetModelBuffer(name, positions, faces, colors);
        }

        public static HardwareVertexBufferPtr GetBonesBuffer()
        {
            var bufferManager = HardwareBufferManager.getSingleton();
            return bufferManager.createVertexBuffer(4 * 4 * sizeof(float), 128 * 16, (byte)HardwareBufferUsage.HBU_CPU_TO_GPU);
        }

        public static MeshPtr GetModelBuffer(string name, List<vec3> vectors, List<IndexedFace> faces, List<Color> colors,
            RenderOperation.OperationType renderStyle = RenderOperation.OperationType.OT_TRIANGLE_LIST, List<vec2>? uvs = null,
            List<vec4>? preCalcNormals = null, Func<List<Color>, int, float[]>? colorSelector = null, List<VertexJointInfo>? joints = null)
        {
            var vertices = new List<float>();
            var vert3s = new List<vec3>();
            var vertColors = new List<vec4>();
            List<ivec3>? vertBlendIndices = null;
            List<vec3>? vertBlendWeights = null;
            List<vec2>? vertUvs = null;
            if (uvs != null)
            {
                vertUvs = new List<vec2>();
            }

            if (joints != null)
            {
                vertBlendIndices = new List<ivec3>();
                vertBlendWeights = new List<vec3>();
            }
            var indices = new List<uint>();
            var normals = new List<vec3>(faces.Count * 3);
            var index = 0;
            foreach (var face in faces)
            {
                var i1 = face.Indexes![0];
                var i2 = face.Indexes[1];
                var i3 = face.Indexes[2];
                var v1 = vectors[i1];
                var v2 = vectors[i2];
                var v3 = vectors[i3];
                v1.x = -v1.x;
                v2.x = -v2.x;
                v3.x = -v3.x;
                if (preCalcNormals != null)
                {
                    var n1 = preCalcNormals[i1];
                    var n2 = preCalcNormals[i2];
                    var n3 = preCalcNormals[i3];
                    normals.Add(n1.xyz);
                    normals.Add(n2.xyz);
                    normals.Add(n3.xyz);
                }
                if (uvs != null)
                {
                    var uv1 = uvs[i1];
                    var uv2 = uvs[i2];
                    var uv3 = uvs[i3];
                    vertUvs!.Add(uv1);
                    vertUvs!.Add(uv2);
                    vertUvs!.Add(uv3);
                }

                if (joints != null)
                {
                    vertBlendIndices!.Add(new ivec3(joints[i1].JointIndex1, joints[i1].JointIndex2, joints[i1].JointIndex3));
                    vertBlendIndices!.Add(new ivec3(joints[i2].JointIndex1, joints[i2].JointIndex2, joints[i2].JointIndex3));
                    vertBlendIndices!.Add(new ivec3(joints[i3].JointIndex1, joints[i3].JointIndex2, joints[i3].JointIndex3));
                    vertBlendWeights!.Add(new vec3(joints[i1].Weight1, joints[i1].Weight2, joints[i1].Weight3));
                    vertBlendWeights!.Add(new vec3(joints[i2].Weight1, joints[i2].Weight2, joints[i2].Weight3));
                    vertBlendWeights!.Add(new vec3(joints[i3].Weight1, joints[i3].Weight2, joints[i3].Weight3));
                }
                var vec1 = v1;
                var vec2 = v2;
                var vec3 = v3;
                vert3s.Add(vec1);
                vert3s.Add(vec2);
                vert3s.Add(vec3);
                indices.Add((uint)(vert3s.Count - 1));
                indices.Add((uint)(vert3s.Count - 2));
                indices.Add((uint)(vert3s.Count - 3));
                vertices.AddRange(vec1.ToArray());
                vertices.AddRange(vec2.ToArray());
                vertices.AddRange(vec3.ToArray());
                vertColors.Add(new vec4(colorSelector == null ? colors[i1 % colors.Count].ToArray() : colorSelector.Invoke(colors, index)));
                vertColors.Add(new vec4(colorSelector == null ? colors[i2 % colors.Count].ToArray() : colorSelector.Invoke(colors, index)));
                vertColors.Add(new vec4(colorSelector == null ? colors[i3 % colors.Count].ToArray() : colorSelector.Invoke(colors, index)));
                index++;
            }


            if (preCalcNormals == null)
            {
                for (var i = 0; i < indices.Count; ++i)
                {
                    normals.Add(new vec3());
                }
                for (var i = 0; i < indices.Count; i += 3)
                {
                    var vec1 = vert3s[(int)indices[i]];
                    var vec2 = vert3s[(int)indices[i + 1]];
                    var vec3 = vert3s[(int)indices[i + 2]];

                    normals[(int)indices[i]] += vec3.Cross(vec2 - vec1, vec3 - vec1);
                    normals[(int)indices[i + 1]] += vec3.Cross(vec2 - vec1, vec3 - vec1);
                    normals[(int)indices[i + 2]] += vec3.Cross(vec2 - vec1, vec3 - vec1);
                }

                for (var i = 0; i < normals.Count; ++i)
                {
                    normals[i] = normals[i].Normalized;
                }
            }

            var resultBuffer = GenerateInterleavedVertexData(vert3s, normals, vertColors, vertUvs, vertBlendIndices, vertBlendWeights);
            return GenerateMesh(name, (uint)vert3s.Count, resultBuffer, indices, renderStyle, true, uvs != null, vertBlendIndices, vertBlendWeights);
        }

        private static List<float> GenerateInterleavedVertexData(List<vec3> positions, List<vec3> normals, List<vec4>? colors = null, List<vec2>? uvs = null,
            List<ivec3>? blendIndices = null, List<vec3>? blendWeights = null)
        {
            var resultBuffer = new List<float>();
            var positionToIndexMap = new Dictionary<vec3, int>();
            var vertexId = 0;
            for (var i = 0; i < positions.Count; ++i)
            {
                var pos = positions[i];
                if (!positionToIndexMap.ContainsKey(pos))
                {
                    positionToIndexMap.Add(pos, vertexId++);
                }
                
                var normal = normals[i];

                resultBuffer.AddRange(new vec4(pos.xyz, positionToIndexMap[pos]).Values);
                resultBuffer.AddRange(normal.Values);

                if (colors != null)
                {
                    var color = colors[i];
                    resultBuffer.AddRange(color.Values);
                }
                if (uvs != null)
                {
                    var uv = uvs[i];
                    resultBuffer.AddRange(uv.Values);
                }
                if (blendIndices != null)
                {
                    var blend = blendIndices[i];
                    // var resultPack = (uint)((blend.x) | (blend.y << 8) | (blend.z << 16));
                    // resultBuffer.Add(resultPack);
                    resultBuffer.Add(blend.x);
                    resultBuffer.Add(blend.y);
                    resultBuffer.Add(blend.z);
                    resultBuffer.Add(0);
                }
                if (blendWeights != null)
                {
                    var weights = blendWeights[i];
                    resultBuffer.AddRange(weights.Values);
                    resultBuffer.Add(0.0f);
                }
            }

            return resultBuffer;
        }

        private static bool TryRetrieveMesh(string name, bool createOnFail, out MeshPtr? mesh)
        {
            var meshManager = MeshManager.getSingleton();
            mesh = null;

            if (meshManager.resourceExists(name, GlobalConsts.OgreGroup))
            {
                mesh = meshManager.getByName(name);
                return true;
            }

            if (createOnFail)
            {
                mesh = meshManager.createManual(name, GlobalConsts.OgreGroup);
            }
            return false;
        }

        private static MeshPtr GenerateMesh(string name, uint vertexAmount, List<float> vertexData, List<uint> indexData, RenderOperation.OperationType renderStyle = RenderOperation.OperationType.OT_TRIANGLE_LIST, bool hasColors = false, bool hasUvs = false, List<ivec3>? blendIndices = null, List<vec3>? blendWeights = null)
        {
            var formattedName = name.Replace(" ", "_");
            MeshPtr mesh;
            if (TryRetrieveMesh(formattedName, true, out mesh!))
            {
                return mesh;
            }

            mesh._setBounds(new AxisAlignedBox(new Vector3(-500, -500, -500), new Vector3(500, 500, 500)));
            mesh.createVertexData();
            mesh.sharedVertexData.vertexCount = vertexAmount;
            var vertDecl = mesh.sharedVertexData.vertexDeclaration;
            var vertBind = mesh.sharedVertexData.vertexBufferBinding;

            var offset = 0U;
            offset += vertDecl.addElement(0, offset, VertexElementType.VET_FLOAT4, VertexElementSemantic.VES_POSITION, 0).getSize();
            offset += vertDecl.addElement(0, offset, VertexElementType.VET_FLOAT3, VertexElementSemantic.VES_NORMAL, 0).getSize();
            if (hasColors)
            {
                offset += vertDecl.addElement(0, offset, VertexElementType.VET_FLOAT4, VertexElementSemantic.VES_COLOUR, 0).getSize();
            }
            if (hasUvs)
            {
                offset += vertDecl.addElement(0, offset, VertexElementType.VET_FLOAT2, VertexElementSemantic.VES_TEXTURE_COORDINATES, 0).getSize();
            }

            if (blendIndices != null && blendWeights != null)
            {
                offset += vertDecl.addElement(0, offset, VertexElementType.VET_FLOAT4, VertexElementSemantic.VES_POSITION, 1).getSize();
                offset += vertDecl.addElement(0, offset, VertexElementType.VET_FLOAT4, VertexElementSemantic.VES_POSITION, 2).getSize();
            }
            //offset += vertDecl.addElement(0, offset, VertexElementType.VET_UINT1, VertexElementSemantic.VES_VERTEX_ID).getSize();

            var bufferManager = HardwareBufferManager.getSingleton();
            var vbuf = bufferManager.createVertexBuffer(offset, vertexAmount, (byte)HardwareBufferUsage.HBU_GPU_ONLY);
            var resultBufferArray = vertexData.ToArray();
            unsafe
            {
                fixed (float* buffer = resultBufferArray)
                {
                    vbuf.writeData(0, vbuf.getSizeInBytes(), new nint(buffer), true);
                    vertBind.setBinding(0, vbuf);
                }
            }

            var ibuf = bufferManager.createIndexBuffer(HardwareIndexBuffer.IndexType.IT_32BIT, (uint)indexData.Count, (byte)HardwareBufferUsage.HBU_GPU_ONLY);
            var indicesArray = indexData.ToArray();
            unsafe
            {
                fixed (uint* buffer = indicesArray)
                {
                    ibuf.writeData(0, ibuf.getSizeInBytes(), new nint(buffer), true);
                }
            }
            //
            // if (blendIndices != null)
            // {
            //     foreach (var index in indexData)
            //     {
            //         AddBoneAssignment((int)index);
            //         continue;
            //
            //         void AddBoneAssignment(int i)
            //         {
            //             var boneAssignment1 = new VertexBoneAssignment();
            //             boneAssignment1.boneIndex = (ushort)blendIndices[i].x;
            //             boneAssignment1.weight = blendWeights[i].x;
            //             boneAssignment1.vertexIndex = (uint)i;
            //             var boneAssignment2 = new VertexBoneAssignment();
            //             boneAssignment2.boneIndex = (ushort)blendIndices[i].y;
            //             boneAssignment2.weight = blendWeights[i].y;
            //             boneAssignment2.vertexIndex = (uint)i;
            //             var boneAssignment3 = new VertexBoneAssignment();
            //             boneAssignment3.boneIndex = (ushort)blendIndices[i].z;
            //             boneAssignment3.weight = blendWeights[i].z;
            //             boneAssignment3.vertexIndex = (uint)i;
            //             mesh.addBoneAssignment(boneAssignment1);
            //             mesh.addBoneAssignment(boneAssignment2);
            //             mesh.addBoneAssignment(boneAssignment3);
            //         }
            //     }
            // }
            var subMesh = mesh.createSubMesh();
            subMesh.useSharedVertices = true;
            subMesh.operationType = renderStyle;
            subMesh.indexData.indexBuffer = ibuf;
            subMesh.indexData.indexCount = (uint)indexData.Count;
            subMesh.indexData.indexStart = 0;

            // mesh.sharedVertexData.vertexDeclaration = vertDecl;
            // mesh._compileBoneAssignments();

            mesh.load();

            return mesh;
        }

        public static MeshPtr GetLineBuffer(string name, List<vec3> vectors, List<IndexedFace> faces, List<Color> colors)
        {
            var vertices = new List<float>();
            var vert3s = new List<vec3>();
            var vertColors = new List<vec4>();
            var indices = new List<uint>();
            var normals = new List<vec3>(faces.Count * 3);
            var index = 0;
            foreach (var face in faces)
            {
                var i1 = face.Indexes![0];
                var i2 = face.Indexes[1];
                var v1 = vectors[i1];
                var v2 = vectors[i2];
                var vec1 = v1;
                var vec2 = v2;
                vec1.x = -vec1.x;
                vec2.x = -vec2.x;
                vert3s.Add(vec1);
                vert3s.Add(vec2);
                indices.Add((uint)(vert3s.Count - 1));
                indices.Add((uint)(vert3s.Count - 2));
                vertices.AddRange(vec1.ToArray());
                vertices.AddRange(vec2.ToArray());
                vertColors.Add(new vec4(colors[i1 % colors.Count].ToArray()));
                vertColors.Add(new vec4(colors[i2 % colors.Count].ToArray()));
                index++;
            }


            for (var i = 0; i < indices.Count; ++i)
            {
                normals.Add(new vec3());
            }
            for (var i = 0; i < indices.Count; i += 2)
            {
                var vec1 = vert3s[(int)indices[i]];
                var vec2 = vert3s[(int)indices[i + 1]];

                normals[(int)indices[i]] += vec3.Cross(vec2 - vec1, vec2 - vec1);
                normals[(int)indices[i + 1]] += vec3.Cross(vec2 - vec1, vec2 - vec1);
            }

            for (var i = 0; i < normals.Count; ++i)
            {
                normals[i] = normals[i].Normalized;
            }

            var resultBuffer = GenerateInterleavedVertexData(vert3s, normals, vertColors);
            return GenerateMesh(name, (uint)vert3s.Count, resultBuffer, indices, RenderOperation.OperationType.OT_LINE_LIST, true);
        }

        public static MeshPtr GetPlaneBuffer()
        {
            float[] vertices = new float[18] {
                -100, -100, 0,  // pos
                100, -100, 0,
                -100,  100, 0,
                -100,  100, 0 ,
                100,  -100, 0 ,
                100,  100, 0 ,
            };
            
            var vectors = new List<vec3>();
            var faces = new List<IndexedFace>();
            var uvs = new List<vec2>()
            {
                new vec2(0, 0),
                new vec2(1, 0),
                new vec2(0, 1),
                new vec2(0, 1),
                new vec2(1, 0),
                new vec2(1, 1),
            };
            for (var i = 0; i < vertices.Length; i += 3)
            {
                vectors.Add(new vec3(vertices[i], vertices[i + 1], vertices[i + 2]));
            }
            for (var i = 0; i < vectors.Count; i += 3)
            {
                faces.Add(new IndexedFace { Indexes = new int[] { i + 2, i + 1, i } });
            }

            return GetModelBuffer("LAB_DEFAULT_PLANE", vectors, faces, new List<Color> { Color.LightGray }, RenderOperation.OperationType.OT_TRIANGLE_LIST, uvs);
        }

        public static MeshPtr GetCubeBuffer(string name, Color? color = null)
        {
            color ??= Color.LightGray;
            List<Color> colors = new List<Color> { color.Value };
            float[] cubeVertecies = {
                -1.0f,-1.0f,-1.0f,
                -1.0f,-1.0f, 1.0f,
                -1.0f, 1.0f, 1.0f,
                1.0f, 1.0f,-1.0f,
                -1.0f,-1.0f,-1.0f,
                -1.0f, 1.0f,-1.0f,
                1.0f,-1.0f, 1.0f,
                -1.0f,-1.0f,-1.0f,
                1.0f,-1.0f,-1.0f,
                1.0f, 1.0f,-1.0f,
                1.0f,-1.0f,-1.0f,
                -1.0f,-1.0f,-1.0f,
                -1.0f,-1.0f,-1.0f,
                -1.0f, 1.0f, 1.0f,
                -1.0f, 1.0f,-1.0f,
                1.0f,-1.0f, 1.0f,
                -1.0f,-1.0f, 1.0f,
                -1.0f,-1.0f,-1.0f,
                -1.0f, 1.0f, 1.0f,
                -1.0f,-1.0f, 1.0f,
                1.0f,-1.0f, 1.0f,
                1.0f, 1.0f, 1.0f,
                1.0f,-1.0f,-1.0f,
                1.0f, 1.0f,-1.0f,
                1.0f,-1.0f,-1.0f,
                1.0f, 1.0f, 1.0f,
                1.0f,-1.0f, 1.0f,
                1.0f, 1.0f, 1.0f,
                1.0f, 1.0f,-1.0f,
                -1.0f, 1.0f,-1.0f,
                1.0f, 1.0f, 1.0f,
                -1.0f, 1.0f,-1.0f,
                -1.0f, 1.0f, 1.0f,
                1.0f, 1.0f, 1.0f,
                -1.0f, 1.0f, 1.0f,
                1.0f,-1.0f, 1.0f
            };
            
            var vectors = new List<vec3>();
            var faces = new List<IndexedFace>();
            for (var i = 0; i < cubeVertecies.Length; i += 3)
            {
                vectors.Add(new vec3(cubeVertecies[i], cubeVertecies[i + 1], cubeVertecies[i + 2]));
            }
            for (var i = 0; i < vectors.Count; i += 3)
            {
                faces.Add(new IndexedFace { Indexes = new int[] { i + 2, i + 1, i } });
            }
            return GetModelBuffer(name, vectors, faces, colors);
        }

        public static MeshPtr GetCircleBuffer(string name, Color color, float segmentPart = 1.0f, float thickness = 0.1f, int resolution = 16)
        {
            var segment = 2 * System.Math.PI * segmentPart;
            List<vec3> vectors = new List<vec3>();
            var step = (2 * System.Math.PI) / resolution;
            var k = 1.0f - thickness;
            for (var i = 0; i <= resolution; ++i)
            {
                var step1 = i * step;
                if (step1 > segment)
                {
                    break;
                }
                var step2 = System.Math.Min((i + 1) * step, segment);
                vectors.Add(new vec3((float)System.Math.Cos(step1), 0, (float)System.Math.Sin(step1)));
                vectors.Add(new vec3((float)System.Math.Cos(step1) * k, 0, (float)System.Math.Sin(step1) * k));
                vectors.Add(new vec3((float)System.Math.Cos(step2) * k, 0, (float)System.Math.Sin(step2)));
                vectors.Add(new vec3((float)System.Math.Cos(step1), 0, (float)System.Math.Sin(step1)));
                vectors.Add(new vec3((float)System.Math.Cos(step2) * k, 0, (float)System.Math.Sin(step2) * k));
                vectors.Add(new vec3((float)System.Math.Cos(step2), 0, (float)System.Math.Sin(step2)));
            }
            var faces = new List<IndexedFace>();
            for (var i = 0; i < vectors.Count; i += 3)
            {
                faces.Add(new IndexedFace { Indexes = new int[] { i + 2, i + 1, i } });
            }
            
            return GetModelBuffer(name, vectors, faces, new List<Color> { color });
        }
        public static MeshPtr GetLineBuffer(string name, Color color)
        {
            List<vec3> vectors = new List<vec3>
            {
                new vec3(0, 0, 0),
                new vec3(1, 0, 0),
                new vec3(1, 0, 0)
            };
            var faces = new List<IndexedFace>();
            for (var i = 0; i < vectors.Count; i += 3)
            {
                faces.Add(new IndexedFace { Indexes = new int[] { i + 2, i + 1, i } });
            }
            return GetModelBuffer(name, vectors, faces, new List<Color> { color }, RenderOperation.OperationType.OT_LINE_LIST);
        }

        public static MeshPtr GetSimpleAxisBuffer()
        {
            List<vec3> vectors = new List<vec3>
            {
                new vec3(0, 0, 0),
                new vec3(-1, 0, 0),
                new vec3(0, 0, 0),
                new vec3(0, 1, 0),
                new vec3(0, 0, 0),
                new vec3(0, 0, 1),
            };
            List<Color> colors = new List<Color>()
            {
                Color.FromArgb(255,255,0,0),
                Color.FromArgb(255,255,0,0),
                Color.FromArgb(255,0,255,0),
                Color.FromArgb(255,0,255,0),
                Color.FromArgb(255,0,0,255),
                Color.FromArgb(255,0,0,255),
            };
            var faces = new List<IndexedFace>();
            for (var i = 0; i < vectors.Count; i += 2)
            {
                faces.Add(new IndexedFace { Indexes = new int[] { i + 1, i } });
            }
            return GetLineBuffer("SimpleAxisMesh", vectors, faces, colors);
        }
    }
}
