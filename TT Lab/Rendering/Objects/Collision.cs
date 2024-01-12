using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets.Instance;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class Collision : BaseRenderable
    {
        IndexedBufferArray collisionBuffer;

        public Collision(Scene root, CollisionData colData) : base(root)
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

        public override void Render()
        {
            Bind();
            Root?.Renderer.RenderProgram.SetUniform1("Alpha", Opacity);
            // Fragment program uniforms
            Root?.Renderer.RenderProgram.SetUniform3("AmbientMaterial", 0.55f, 0.45f, 0.45f);
            Root?.Renderer.RenderProgram.SetUniform3("SpecularMaterial", 0.5f, 0.5f, 0.5f);
            Root?.Renderer.RenderProgram.SetUniform3("LightPosition", Root.CameraPosition.x, Root.CameraPosition.y, Root.CameraPosition.z);
            Root?.Renderer.RenderProgram.SetUniform3("LightDirection", -Root.CameraDirection.x, Root.CameraDirection.y, Root.CameraDirection.z);
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
