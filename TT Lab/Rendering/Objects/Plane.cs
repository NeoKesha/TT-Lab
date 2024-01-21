using GlmSharp;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class Plane : BaseRenderable
    {
        IndexedBufferArray planeBuffer;
        TextureBuffer? texture;

        public Plane(Scene root) : this(root, new vec3())
        {
        }

        public Plane(Scene root, vec3 position) : base(root)
        {
            planeBuffer = BufferGeneration.GetModelBuffer(
                new List<vec3>
                {
                    new vec3(1 + position.x, 1 + position.y, -1 + position.z),
                    new vec3(-1 + position.x, 1 + position.y, -1 + position.z),
                    new vec3(-1 + position.x, -1 + position.y, -1 + position.z),
                    new vec3(1 + position.x, -1 + position.y, -1 + position.z)
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

        public Plane(Scene root, MaterialData material) : this(root)
        {
            if (material.Shaders[0].TxtMapping == Twinsanity.TwinsanityInterchange.Common.TwinShader.TextureMapping.ON)
            {
                var tex = AssetManager.Get().GetAssetData<TextureData>(material.Shaders[0].TextureId);
                texture = new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap);
            }
        }

        public Plane(Scene root, TextureData tex) : this(root, tex.Bitmap)
        {
        }

        public Plane(Scene root, Bitmap texImage) : this(root)
        {
            texture = new TextureBuffer(texImage.Width, texImage.Height, texImage);
        }

        public Plane(Scene root, TextureBuffer texture) : this(root)
        {
            this.texture = texture;
        }

        public void Bind()
        {
            if (texture != null)
            {
                Root?.Renderer.RenderProgram.SetTextureUniform("tex", TextureTarget.Texture2D, texture.Buffer, 0);
            }
            Root?.Renderer.RenderProgram.SetUniform1("Opacity", Opacity);
            planeBuffer.Bind();
        }

        public void Delete()
        {
            texture?.Delete();
            planeBuffer.Delete();
        }

        protected override void RenderSelf()
        {
            Bind();
            GL.DrawElements(PrimitiveType.Triangles, planeBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            texture?.Unbind();
            planeBuffer.Unbind();
        }
    }
}
