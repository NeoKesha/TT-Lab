using GlmSharp;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.Extensions;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Util
{
    public static class BufferGeneration
    {
        public static IndexedBufferArray GetModelBuffer(List<Vertex> vertexes, List<IndexedFace> faces, bool generateUvs = true)
        {
            if (generateUvs)
            {
                return GetModelBuffer(vertexes.Select(v => v.Position).ToList(), faces,
                    vertexes.Select((v) =>
                    {
                        var col = v.Color.GetColor();
                        return Color.FromArgb((int)col.ToARGB());
                    }).ToList(),
                    vertexes.Select(v => v.UV).ToList(),
                    vertexes.Select(v => v.Normal).ToList());
            }

            if (vertexes.Select(v => v.HasNormals).Any())
            {
                return GetModelBuffer(vertexes.Select(v => v.Position).ToList(), faces,
                    vertexes.Select((v) =>
                    {
                        var col = v.Color.GetColor();
                        return Color.FromArgb((int)col.ToARGB());
                    }).ToList(),
                    vertexes.Select(v => v.Normal).ToList());
            }

            return GetModelBuffer(vertexes.Select(v => v.Position).ToList(), faces,
                vertexes.Select((v) =>
                {
                    var col = v.Color.GetColor();
                    return Color.FromArgb((int)col.ToARGB());
                }).ToList());
        }

        public static IndexedBufferArray GetModelBuffer(List<Twinsanity.TwinsanityInterchange.Common.Vector3> vectors, List<IndexedFace> faces, List<Color> colors,
            List<Twinsanity.TwinsanityInterchange.Common.Vector4>? preCalcNormals = null, Func<List<Color>, int, float[]>? colorSelector = null)
        {
            var vertices = new List<float>();
            var vert3s = new List<Vector3>();
            var vertColors = new List<float>();
            var indices = new List<uint>();
            var normals = new List<Vector3>(faces.Count * 3);
            var index = 0;
            foreach (var face in faces)
            {
                var i1 = face.Indexes![0];
                var i2 = face.Indexes[1];
                var i3 = face.Indexes[2];
                var v1 = vectors[i1];
                var v2 = vectors[i2];
                var v3 = vectors[i3];
                if (preCalcNormals != null)
                {
                    var n1 = preCalcNormals[i1].ToGL();
                    var n2 = preCalcNormals[i2].ToGL();
                    var n3 = preCalcNormals[i3].ToGL();
                    n1.X = -n1.X;
                    n2.X = -n2.X;
                    n3.X = -n3.X;
                    normals.Add(n1.Xyz);
                    normals.Add(n2.Xyz);
                    normals.Add(n3.Xyz);
                }
                var vec1 = v1.ToGL();
                var vec2 = v2.ToGL();
                var vec3 = v3.ToGL();
                vec1.X = -vec1.X;
                vec2.X = -vec2.X;
                vec3.X = -vec3.X;
                vert3s.Add(vec1);
                vert3s.Add(vec2);
                vert3s.Add(vec3);
                indices.Add((uint)(vert3s.Count - 1));
                indices.Add((uint)(vert3s.Count - 2));
                indices.Add((uint)(vert3s.Count - 3));
                vertices.AddRange(vec1.ToArray());
                vertices.AddRange(vec2.ToArray());
                vertices.AddRange(vec3.ToArray());
                vertColors.AddRange(colorSelector == null ? colors[i1 % colors.Count].ToArray() : colorSelector.Invoke(colors, index));
                vertColors.AddRange(colorSelector == null ? colors[i2 % colors.Count].ToArray() : colorSelector.Invoke(colors, index));
                vertColors.AddRange(colorSelector == null ? colors[i3 % colors.Count].ToArray() : colorSelector.Invoke(colors, index));
                index++;
            }


            if (preCalcNormals == null)
            {
                for (var i = 0; i < indices.Count; ++i)
                {
                    normals.Add(new Vector3());
                }
                for (var i = 0; i < indices.Count; i += 3)
                {
                    var vec1 = vert3s[(int)indices[i]];
                    var vec2 = vert3s[(int)indices[i + 1]];
                    var vec3 = vert3s[(int)indices[i + 2]];

                    normals[(int)indices[i]] += Vector3.Cross(vec2 - vec1, vec3 - vec1);
                    normals[(int)indices[i + 1]] += Vector3.Cross(vec2 - vec1, vec3 - vec1);
                    normals[(int)indices[i + 2]] += Vector3.Cross(vec2 - vec1, vec3 - vec1);
                }

                for (var i = 0; i < normals.Count; ++i)
                {
                    var n = normals[i];
                    n.Normalize();
                    normals[i] = n;
                }
            }

            var buffer = new IndexedBufferArray();
            buffer.Bind();

            var indexBuffer = new IndexBuffer();
            indexBuffer.Bind();
            indexBuffer.SetData(indices.ToArray());
            buffer.Indices = indices.ToArray();

            var vertexBuffer = new VertexBuffer();
            vertexBuffer.Bind();
            vertexBuffer.SetData(0, vertices.ToArray(), false, 3);

            var colorBuffer = new VertexBuffer();
            colorBuffer.Bind();
            colorBuffer.SetData(1, vertColors.ToArray(), false, 4);

            var normalBuffer = new VertexBuffer();
            normalBuffer.Bind();
            normalBuffer.SetData(2, normals.SelectMany(v => v.ToArray()).ToArray(), false, 3);

            buffer.Unbind();

            return buffer;
        }

        public static IndexedBufferArray GetModelBuffer(List<Twinsanity.TwinsanityInterchange.Common.Vector3> vectors, List<IndexedFace> faces, List<Color> colors, List<Twinsanity.TwinsanityInterchange.Common.Vector3> uvs,
            List<Twinsanity.TwinsanityInterchange.Common.Vector4>? normals = null)
        {
            var uvVecs = new List<float>();

            foreach (var face in faces)
            {
                var i1 = face.Indexes![0];
                var i2 = face.Indexes[1];
                var i3 = face.Indexes[2];
                var v1 = uvs[i1];
                var v2 = uvs[i2];
                var v3 = uvs[i3];
                var vec1 = v1.ToGL();
                var vec2 = v2.ToGL();
                var vec3 = v3.ToGL();
                uvVecs.AddRange(vec1.ToArray());
                uvVecs.AddRange(vec2.ToArray());
                uvVecs.AddRange(vec3.ToArray());
            }

            var bufferArray = GetModelBuffer(vectors, faces, colors, normals);
            bufferArray.Bind();

            var uvBuffer = new VertexBuffer();
            uvBuffer.Bind();
            uvBuffer.SetData(3, uvVecs.ToArray(), true, 3);

            bufferArray.Unbind();

            return bufferArray;
        }

        public static IndexedBufferArray GetCubeBuffer(Vector3 position = default, Vector3 scale = default, Quaternion rotation = default, List<Color>? colors = null)
        {
            colors ??= new List<Color> { Color.LightGray };
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
            // Scale
            var scaleMat = mat4.Scale(new vec3(scale.X, scale.Y, scale.Z));
            for (var i = 0; i < cubeVertecies.Length; i += 3)
            {
                var v = new vec4(cubeVertecies[i], cubeVertecies[i + 1], cubeVertecies[i + 2], 1.0f);
                v = scaleMat * v;
                cubeVertecies[i] = v.x;
                cubeVertecies[i + 1] = v.y;
                cubeVertecies[i + 2] = v.z;
            }
            // Rotation
            if (rotation != default)
            {
                var rotMat = Matrix4.CreateFromQuaternion(rotation);
                for (var i = 0; i < cubeVertecies.Length; i += 3)
                {
                    var v = new Vector4(cubeVertecies[i], cubeVertecies[i + 1], cubeVertecies[i + 2], 1.0f);
                    v = rotMat * v;
                    v = rotMat * v;
                    v = rotMat * v;
                    cubeVertecies[i] = v.X;
                    cubeVertecies[i + 1] = v.Y;
                    cubeVertecies[i + 2] = v.Z;
                }
            }
            // Translation
            for (var i = 0; i < cubeVertecies.Length; i += 3)
            {
                cubeVertecies[i] += position.X;
                cubeVertecies[i + 1] += position.Y;
                cubeVertecies[i + 2] += position.Z;
            }
            var vectors = new List<Twinsanity.TwinsanityInterchange.Common.Vector3>();
            var faces = new List<IndexedFace>();
            for (var i = 0; i < cubeVertecies.Length; i += 3)
            {
                vectors.Add(new Twinsanity.TwinsanityInterchange.Common.Vector3(cubeVertecies[i], cubeVertecies[i + 1], cubeVertecies[i + 2]));
            }
            for (var i = 0; i < vectors.Count; i += 3)
            {
                faces.Add(new IndexedFace { Indexes = new int[] { i + 2, i + 1, i } });
            }
            return GetModelBuffer(vectors, faces, colors);
        }

        public static IndexedBufferArray GetCubeBuffer(Vector3 position = default, float scale = 1.0f, List<Color>? colors = null)
        {
            return GetCubeBuffer(position, new Vector3(scale, scale, scale), default, colors);
        }

    }
}
