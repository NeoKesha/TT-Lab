using GlmSharp;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;
using TT_Lab.ViewModels.Editors.Graphics;

namespace TT_Lab.Rendering.Objects
{
    public class TwinMaterialPlane : BaseRenderable
    {
        private IndexedBufferArray planeBuffer;
        private TwinMaterial[] materialBuffers = new TwinMaterial[5];
        private int texAmt;

        private TwinMaterialPlane(OpenGL gl, GLWindow window, Scene root) : base(gl, window, root)
        {
            planeBuffer = BufferGeneration.GetModelBuffer(GL,
                new List<vec3>
                {
                    new vec3(1, 1, -1),
                    new vec3(-1, 1, -1),
                    new vec3(-1, -1, -1),
                    new vec3(1, -1, -1)
                },
                new List<AssetData.Graphics.SubModels.IndexedFace>
                {
                    new AssetData.Graphics.SubModels.IndexedFace(new int[] { 0, 1, 2 }),
                    new AssetData.Graphics.SubModels.IndexedFace(new int[] { 2, 3, 0 })
                },
                new List<System.Drawing.Color> { System.Drawing.Color.White },
                new List<vec3>
                {
                    new vec3(0, 0, 0),
                    new vec3(1, 0, 0),
                    new vec3(1, 1, 0),
                    new vec3(0, 1, 0)
                });
        }

        public TwinMaterialPlane(OpenGL gl, GLWindow window, Scene root, ShaderProgram shader, Bitmap[] textures, ShaderViewModel[] viewModels, int texAmt = 1) : this(gl, window, root)
        {
            if (texAmt > 5) texAmt = 5;
            this.texAmt = texAmt;
            for (var i = 0; i < texAmt; ++i)
            {
                materialBuffers[i] = new TwinMaterial(GL, shader, "Texture", textures[i], viewModels[i], 3 + i, i);
            }
        }

        public void Bind()
        {
            for (var i = 0; i < texAmt; ++i)
            {
                materialBuffers[i].Bind();
            }
            Window.Renderer?.RenderProgram.SetUniform1("Opacity", Opacity);
            Window.Renderer?.RenderProgram.SetUniform1("TexturesAmount", texAmt);
            planeBuffer.Bind();
        }

        public void Delete()
        {
            planeBuffer.Delete();
            for (var i = 0; i < texAmt; ++i)
            {
                materialBuffers[i].Delete();
            }
        }

        protected override void RenderSelf(ShaderProgram shader)
        {
            Bind();
            GL.DrawElements(OpenGL.GL_TRIANGLES, planeBuffer.Indices.Length, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            for (var i = 0; i < texAmt; ++i)
            {
                materialBuffers[i].Unbind();
            }
            planeBuffer.Unbind();
        }
    }
}
