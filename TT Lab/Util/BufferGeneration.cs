using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics.SubModels;
using TT_Lab.Rendering.Buffers;

namespace TT_Lab.Util
{
    public static class BufferGeneration
    {
        public static IndexedBufferArray GetModelBuffer(List<Twinsanity.TwinsanityInterchange.Common.Vector3> vectors, List<IndexedFace> faces)
        {
            var vertices = new List<float>();
            var vert3s = new List<Vector3>();
            var indices = new List<uint>();

            foreach (var face in faces)
            {
                var v1 = vectors[face.Indexes[0]];
                var v2 = vectors[face.Indexes[1]];
                var v3 = vectors[face.Indexes[2]];
                var vec1 = v1.ToGL();
                var vec2 = v2.ToGL();
                var vec3 = v3.ToGL();
                vert3s.Add(vec1);
                vert3s.Add(vec2);
                vert3s.Add(vec3);
                indices.Add((uint)(vert3s.Count - 1));
                indices.Add((uint)(vert3s.Count - 2));
                indices.Add((uint)(vert3s.Count - 3));
                vertices.AddRange(vec1.ToArray());
                vertices.AddRange(vec2.ToArray());
                vertices.AddRange(vec3.ToArray());
            }

            var normals = new Vector3[faces.Count * 3];
            for (var i = 0; i < indices.Count; i += 3)
            {
                var vec1 = vert3s[(int)indices[i]];
                var vec2 = vert3s[(int)indices[i + 1]];
                var vec3 = vert3s[(int)indices[i + 2]];

                normals[indices[i]] += Vector3.Cross(vec2 - vec1, vec3 - vec1);
                normals[indices[i + 1]] += Vector3.Cross(vec2 - vec1, vec3 - vec1);
                normals[indices[i + 2]] += Vector3.Cross(vec2 - vec1, vec3 - vec1);
            }

            for (var i = 0; i < normals.Length; ++i)
            {
                var n = normals[i];
                n.Normalize();
                normals[i] = n;
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

            var normalBuffer = new VertexBuffer();
            normalBuffer.Bind();
            normalBuffer.SetData(1, normals.SelectMany(v => v.ToArray()).ToArray(), false, 3);

            buffer.Unbind();

            return buffer;
        }
    }
}
