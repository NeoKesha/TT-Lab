using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Rendering.Objects
{
    public class Skin : IRenderable
    {
        public Scene? Parent { get; set; }
        public float Opacity { get; set; } = 1.0f;

        List<IndexedBufferArray> modelBuffers = new List<IndexedBufferArray>();
        List<TextureBuffer> textureBuffers = new List<TextureBuffer>();

        public Skin(SkinData skin, List<LabURI> materials)
        {
            var materialIndex = 0;
            foreach (var subSkin in skin.SubSkins)
            {
                var matData = AssetManager.Get().GetAssetData<MaterialData>(materials[materialIndex++]);
                if (matData.Shaders.Any(s => s.TxtMapping == TwinShader.TextureMapping.ON))
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(subSkin.Vertexes.Select(v => v.Position).ToList(), subSkin.Faces,
                        subSkin.Vertexes.Select((v) =>
                        {
                            var col = v.Color.GetColor();
                            return System.Drawing.Color.FromArgb((int)col.ToARGB());
                        }).ToList(),
                        subSkin.Vertexes.Select(v => new Vector3(v.UV.X, 1 - v.UV.Y, v.UV.Z)).ToList(),
                        subSkin.Vertexes.Select(v => v.Normal).ToList()));
                    var tex = AssetManager.Get().GetAssetData<TextureData>(matData.Shaders[0].TextureId);
                    textureBuffers.Add(new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap));
                }
                else
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(subSkin.Vertexes.Select(v => v.Position).ToList(), subSkin.Faces,
                        subSkin.Vertexes.Select((v) =>
                        {
                            var col = v.Color.GetColor();
                            return System.Drawing.Color.FromArgb((int)col.ToARGB());
                        }).ToList()));
                }
            }
        }

        public void Bind()
        {
            Parent?.Renderer.RenderProgram.SetUniform1("Alpha", Opacity);
        }

        public void Delete()
        {
            foreach (var tex in textureBuffers)
            {
                tex.Delete();
            }
            foreach (var model in modelBuffers)
            {
                model.Delete();
            }
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
            for (var i = 0; i < modelBuffers.Count; ++i)
            {
                if (textureBuffers.Count != 0)
                {
                    Parent?.Renderer.RenderProgram.SetTextureUniform("tex", TextureTarget.Texture2D, textureBuffers[i].Buffer, 0);
                }
                modelBuffers[i].Bind();
                GL.DrawElements(PrimitiveType.Triangles, modelBuffers[i].Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                modelBuffers[i].Unbind();
            }
            Unbind();
        }

        public void Unbind()
        {
        }
    }
}
