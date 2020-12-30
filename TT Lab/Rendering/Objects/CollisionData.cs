using GlmNet;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class CollisionData : IRenderable
    {

        VertexBufferArray collisionBuffer;
        uint[] indices;

        public CollisionData(AssetData.Instance.CollisionData colData)
        {
            // Init collision buffer
            var vertices = new List<float>();
            var vert3s = new List<Vector3>();
            var colors = new List<float>();
            var indices = new List<uint>();

            var surfaceColors = new System.Drawing.Color[]
            {
                System.Drawing.Color.White,
                System.Drawing.Color.Red,
                System.Drawing.Color.Brown,
                System.Drawing.Color.Cyan,
                System.Drawing.Color.Blue,
                System.Drawing.Color.Pink,
                System.Drawing.Color.Yellow,
                System.Drawing.Color.Gray,
                System.Drawing.Color.DarkGray,
                System.Drawing.Color.Green,
                System.Drawing.Color.Gold,
                System.Drawing.Color.Aqua,
                System.Drawing.Color.SkyBlue,
                System.Drawing.Color.AntiqueWhite,
                System.Drawing.Color.Bisque,
                System.Drawing.Color.Chocolate,
                System.Drawing.Color.DarkSeaGreen,
                System.Drawing.Color.Azure,
                System.Drawing.Color.HotPink,
                System.Drawing.Color.Honeydew,
                System.Drawing.Color.Lime,
                System.Drawing.Color.Magenta,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
                System.Drawing.Color.White,
            };
            foreach (var tri in colData.Triangles)
            {
                var v1 = colData.Vectors[tri.Vector1Index];
                var v2 = colData.Vectors[tri.Vector2Index];
                var v3 = colData.Vectors[tri.Vector3Index];
                var vec1 = new Vector3(-v1.X, v1.Y, v1.Z);
                var vec2 = new Vector3(-v2.X, v2.Y, v2.Z);
                var vec3 = new Vector3(-v3.X, v3.Y, v3.Z);
                vert3s.Add(vec1);
                vert3s.Add(vec2);
                vert3s.Add(vec3);
                indices.Add((uint)(vert3s.Count - 1));
                indices.Add((uint)(vert3s.Count - 2));
                indices.Add((uint)(vert3s.Count - 3));
                vertices.AddRange(vec1.ToArray());
                vertices.AddRange(vec2.ToArray());
                vertices.AddRange(vec3.ToArray());

                var vecCol = surfaceColors[tri.SurfaceIndex];
                var col = new vec4(vecCol.R / 255f, vecCol.G / 255f, vecCol.B / 255f, 1.0f);
                colors.AddRange(col.to_array());
                colors.AddRange(col.to_array());
                colors.AddRange(col.to_array());
            }

            var normals = new Vector3[colData.Triangles.Count * 3];
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

            collisionBuffer = new VertexBufferArray();
            collisionBuffer.Bind();

            var indexBuffer = new IndexBuffer();
            indexBuffer.Bind();
            indexBuffer.SetData(indices.ToArray());
            this.indices = indices.ToArray();

            var vertexBuffer = new VertexBuffer();
            vertexBuffer.Bind();
            vertexBuffer.SetData(0, vertices.ToArray(), false, 3);

            var colorBuffer = new VertexBuffer();
            colorBuffer.Bind();
            colorBuffer.SetData(1, colors.ToArray(), false, 4);

            var normalBuffer = new VertexBuffer();
            normalBuffer.Bind();
            normalBuffer.SetData(2, normals.SelectMany(v => v.ToArray()).ToArray(), false, 3);

            collisionBuffer.Unbind();
        }

        public void Bind()
        {
            collisionBuffer.Bind();
        }

        public void Delete()
        {
            collisionBuffer.Delete();
        }

        public void PostRender()
        {
        }

        public void PreRender()
        {
        }

        public void Render()
        {
            Bind();
            // Draw collision
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            collisionBuffer.Unbind();
        }
    }
}
