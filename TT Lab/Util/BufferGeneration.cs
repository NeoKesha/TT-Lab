using GlmSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Util
{
    public static class BufferGeneration
    {
        public static IndexedBufferArray GetModelBuffer(List<Vertex> vertexes, List<IndexedFace> faces, bool generateUvs = true)
        {
            if (generateUvs)
            {
                return GetModelBuffer(vertexes.Select(v => new vec3(v.Position.X, v.Position.Y, v.Position.Z)).ToList(), faces,
                    vertexes.Select((v) =>
                    {
                        var col = v.Color.GetColor();
                        return Color.FromArgb((int)col.ToARGB());
                    }).ToList(),
                    vertexes.Select(v => new vec3(v.UV.X, v.UV.Y, v.UV.Z)).ToList(),
                    vertexes.Select(v => new vec4(v.Normal.X, v.Normal.Y, v.Normal.Z, v.Normal.W)).ToList());
            }

            if (vertexes.Select(v => v.HasNormals).Any())
            {
                return GetModelBuffer(vertexes.Select(v => new vec3(v.Position.X, v.Position.Y, v.Position.Z)).ToList(), faces,
                    vertexes.Select((v) =>
                    {
                        var col = v.Color.GetColor();
                        return Color.FromArgb((int)col.ToARGB());
                    }).ToList(),
                    vertexes.Select(v => new vec4(v.Normal.X, v.Normal.Y, v.Normal.Z, v.Normal.W)).ToList());
            }

            return GetModelBuffer(vertexes.Select(v => new vec3(v.Position.X, v.Position.Y, v.Position.Z)).ToList(), faces,
                vertexes.Select((v) =>
                {
                    var col = v.Color.GetColor();
                    return Color.FromArgb((int)col.ToARGB());
                }).ToList());
        }

        public static IndexedBufferArray GetModelBuffer(List<vec3> vectors, List<IndexedFace> faces, List<Color> colors,
            List<vec4>? preCalcNormals = null, Func<List<Color>, int, float[]>? colorSelector = null)
        {
            var vertices = new List<float>();
            var vert3s = new List<vec3>();
            var vertColors = new List<float>();
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
                if (preCalcNormals != null)
                {
                    var n1 = preCalcNormals[i1];
                    var n2 = preCalcNormals[i2];
                    var n3 = preCalcNormals[i3];
                    n1.x = -n1.x;
                    n2.x = -n2.x;
                    n3.x = -n3.x;
                    normals.Add(n1.xyz);
                    normals.Add(n2.xyz);
                    normals.Add(n3.xyz);
                }
                var vec1 = v1;
                var vec2 = v2;
                var vec3 = v3;
                vec1.x = -vec1.x;
                vec2.x = -vec2.x;
                vec3.x = -vec3.x;
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

        public static IndexedBufferArray GetLineBuffer(List<vec3> vectors, List<IndexedFace> faces, List<Color> colors)
        {
            var vertices = new List<float>();
            var vert3s = new List<vec3>();
            var vertColors = new List<float>();
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
                vertColors.AddRange(colors[i1 % colors.Count].ToArray());
                vertColors.AddRange(colors[i2 % colors.Count].ToArray());
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

        public static IndexedBufferArray GetModelBuffer(List<vec3> vectors, List<IndexedFace> faces, List<Color> colors, List<vec3> uvs,
            List<vec4>? normals = null)
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
                var vec1 = v1;
                var vec2 = v2;
                var vec3 = v3;
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

        public static IndexedBufferArray GetCubeBuffer(vec3 position = default, vec3 scale = default, quat rotation = default, List<Color>? colors = null)
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
            var scaleMat = mat4.Scale(new vec3(scale.x, scale.y, scale.z));
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
                var rotMat = rotation.ToMat4;
                for (var i = 0; i < cubeVertecies.Length; i += 3)
                {
                    var v = new vec4(cubeVertecies[i], cubeVertecies[i + 1], cubeVertecies[i + 2], 1.0f);
                    v = rotMat * v;
                    v = rotMat * v;
                    v = rotMat * v;
                    cubeVertecies[i] = v.x;
                    cubeVertecies[i + 1] = v.y;
                    cubeVertecies[i + 2] = v.z;
                }
            }
            // Translation
            for (var i = 0; i < cubeVertecies.Length; i += 3)
            {
                cubeVertecies[i] += position.x;
                cubeVertecies[i + 1] += position.y;
                cubeVertecies[i + 2] += position.z;
            }
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
            return GetModelBuffer(vectors, faces, colors);
        }

        public static IndexedBufferArray GetCubeBuffer(vec3 position = default, float scale = 1.0f, List<Color>? colors = null)
        {
            return GetCubeBuffer(position, new vec3(scale, scale, scale), default, colors);
        }

        public static IndexedBufferArray GetCircleBuffer(Color color, float segmentPart = 1.0f, float thickness = 0.1f, int resolution = 16)
        {
            var segment = 2 * Math.PI * segmentPart;
            List<vec3> vectors = new List<vec3>();
            var step = (2 * Math.PI) / resolution;
            var k = 1.0f - thickness;
            for (var i = 0; i <= resolution; ++i)
            {
                var step1 = i * step;
                if (step1 > segment)
                {
                    break;
                }
                var step2 = Math.Min((i + 1) * step, segment);
                vectors.Add(new vec3((float)Math.Cos(step1), 0, (float)Math.Sin(step1)));
                vectors.Add(new vec3((float)Math.Cos(step1) * k, 0, (float)Math.Sin(step1) * k));
                vectors.Add(new vec3((float)Math.Cos(step2) * k, 0, (float)Math.Sin(step2)));
                vectors.Add(new vec3((float)Math.Cos(step1), 0, (float)Math.Sin(step1)));
                vectors.Add(new vec3((float)Math.Cos(step2) * k, 0, (float)Math.Sin(step2) * k));
                vectors.Add(new vec3((float)Math.Cos(step2), 0, (float)Math.Sin(step2)));
            }
            var faces = new List<IndexedFace>();
            for (var i = 0; i < vectors.Count; i += 3)
            {
                faces.Add(new IndexedFace { Indexes = new int[] { i + 2, i + 1, i } });
            }
            return GetModelBuffer(vectors, faces, new List<Color> {color});
        }
        public static IndexedBufferArray GetLineBuffer(Color color) {
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
            return GetModelBuffer(vectors, faces, new List<Color> { color });
        }

        public static IndexedBufferArray GetSimpleAxisBuffer()
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
            return GetLineBuffer(vectors, faces, colors);
        }
    }
}
