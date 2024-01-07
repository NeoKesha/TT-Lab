using GlmNet;
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
    public class Plane : IRenderable
    {
        public Scene? Parent { get; set; }
        public float Opacity { get; set; } = 1.0f;

        IndexedBufferArray planeBuffer;
        TextureBuffer? texture;

        public Plane() : this(new vec3())
        {
        }

        public Plane(vec3 position)
        {
            planeBuffer = BufferGeneration.GetModelBuffer(
                new List<Twinsanity.TwinsanityInterchange.Common.Vector3>
                {
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(1 + position.x, 1 + position.y, -1 + position.z),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(-1 + position.x, 1 + position.y, -1 + position.z),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(-1 + position.x, -1 + position.y, -1 + position.z),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(1 + position.x, -1 + position.y, -1 + position.z)
                },
                new List<AssetData.Graphics.SubModels.IndexedFace>
                {
                    new AssetData.Graphics.SubModels.IndexedFace(new int[] { 0, 1, 2 }),
                    new AssetData.Graphics.SubModels.IndexedFace(new int[] { 2, 3, 0 })
                },
                new List<System.Drawing.Color> { System.Drawing.Color.White },
                new List<Twinsanity.TwinsanityInterchange.Common.Vector3>
                {
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(0, 0, 0),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(1, 0, 0),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(1, 1, 0),
                    new Twinsanity.TwinsanityInterchange.Common.Vector3(0, 1, 0)
                });
        }

        public Plane(MaterialData material) : this()
        {
            if (material.Shaders[0].TxtMapping == Twinsanity.TwinsanityInterchange.Common.TwinShader.TextureMapping.ON)
            {
                var tex = AssetManager.Get().GetAssetData<TextureData>(material.Shaders[0].TextureId);
                texture = new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap);
            }
        }

        public Plane(TextureData tex) : this(tex.Bitmap)
        {
        }

        public Plane(Bitmap texImage) : this()
        {
            texture = new TextureBuffer(texImage.Width, texImage.Height, texImage);
        }

        public Plane(TextureBuffer texture) : this()
        {
            this.texture = texture;
        }

        public void Bind()
        {
            if (texture != null)
            {
                Parent?.Renderer.RenderProgram.SetTextureUniform("tex", TextureTarget.Texture2D, texture.Buffer, 0);
            }
            Parent?.Renderer.RenderProgram.SetUniform1("Alpha", Opacity);
            planeBuffer.Bind();
        }

        public void Delete()
        {
            texture?.Delete();
            planeBuffer.Delete();
        }

        public void Render()
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
