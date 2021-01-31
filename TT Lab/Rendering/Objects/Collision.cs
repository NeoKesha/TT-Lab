using GlmNet;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets.Instance;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class Collision : IRenderable
    {
        public Scene? Parent { get; set; }
        public float Opacity { get; set; } = 1.0f;

        IndexedBufferArray collisionBuffer;

        public Collision(CollisionData colData)
        {
            collisionBuffer = BufferGeneration.GetModelBuffer(
                colData.Vectors.Select(v => new Twinsanity.TwinsanityInterchange.Common.Vector3(v.X, v.Y, v.Z)).ToList(),
                colData.Triangles.Select(t => t.Face).ToList(),
                CollisionSurface.DefaultColors.ToList().Select(c => System.Drawing.Color.FromArgb((int)c.ToARGB())).ToList(),
                null,
                (colors, i) =>
                {
                    return colors[colData.Triangles[i].SurfaceIndex].ToArray();
                });
        }

        public void Bind()
        {
            collisionBuffer.Bind();
        }

        public void Delete()
        {
            collisionBuffer.Delete();
        }

        public void Render()
        {
            Bind();
            Parent?.Renderer.RenderProgram.SetUniform1("Alpha", Opacity);
            // Fragment program uniforms
            Parent?.Renderer.RenderProgram.SetUniform3("AmbientMaterial", 0.55f, 0.45f, 0.45f);
            Parent?.Renderer.RenderProgram.SetUniform3("SpecularMaterial", 0.5f, 0.5f, 0.5f);
            Parent?.Renderer.RenderProgram.SetUniform3("LightPosition", Parent.CameraPosition.x, Parent.CameraPosition.y, Parent.CameraPosition.z);
            Parent?.Renderer.RenderProgram.SetUniform3("LightDirection", -Parent.CameraDirection.x, Parent.CameraDirection.y, Parent.CameraDirection.z);
            // Draw collision
            GL.DrawElements(PrimitiveType.Triangles, collisionBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            collisionBuffer.Unbind();
        }
    }
}
