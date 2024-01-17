using GlmSharp;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;
using TT_Lab.ViewModels.Graphics;

namespace TT_Lab.Rendering.Objects
{
    public class TwinMaterialPlane : BaseRenderable
    {
        private IndexedBufferArray planeBuffer;
        private TwinMaterial[] materialBuffers = new TwinMaterial[5];
        private int texAmt;

        private TwinMaterialPlane(Scene root) : base(root)
        {
            planeBuffer = BufferGeneration.GetModelBuffer(
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

        public TwinMaterialPlane(Scene root, ShaderProgram shader, Bitmap[] textures, LabShaderViewModel[] viewModels, int texAmt = 1) : this(root)
        {
            if (texAmt > 5) texAmt = 5;
            this.texAmt = texAmt;
            for (var i = 0; i < texAmt; ++i)
            {
                materialBuffers[i] = new TwinMaterial(shader, "tex", textures[i], viewModels[i], 3 + i, i);
            }
        }

        public void Bind()
        {
            for (var i = 0; i < texAmt; ++i)
            {
                materialBuffers[i].Bind();
            }
            Root?.Renderer.RenderProgram.SetUniform1("Alpha", Opacity);
            Root?.Renderer.RenderProgram.SetUniform1("TexturesAmount", texAmt);
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

        public override void Render()
        {
            Bind();
            GL.DrawElements(PrimitiveType.Triangles, planeBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
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
