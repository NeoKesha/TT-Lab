using GlmSharp;
using SharpGL;
using System;
using System.Linq;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets.Instance;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class Collision : BaseRenderable
    {
        IndexedBufferArray collisionBuffer;

        public Collision(OpenGL gl, GLWindow window, Scene root, CollisionData colData) : base(gl, window, root)
        {
            collisionBuffer = BufferGeneration.GetModelBuffer(GL,
                colData.Vectors.Select(v => new vec3(v.X, v.Y, v.Z)).ToList(),
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

        public override void SetUniforms(ShaderProgram shader)
        {
            base.SetUniforms(shader);

            shader.SetUniform1("Opacity", Opacity);
            shader.SetUniform3("AmbientMaterial", 0.55f, 0.45f, 0.45f);
            shader.SetUniform3("SpecularMaterial", 0.5f, 0.5f, 0.5f);
        }

        protected override void RenderSelf(ShaderProgram shader)
        {
            Bind();
            // Draw collision
            GL.DrawElements(OpenGL.GL_TRIANGLES, collisionBuffer.Indices.Length, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            collisionBuffer.Unbind();
        }
    }
}
