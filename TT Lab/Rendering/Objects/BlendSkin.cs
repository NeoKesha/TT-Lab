using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Rendering.Objects
{
    public class BlendSkin : IRenderable
    {
        public Scene? Parent { get; set; }
        public Single Opacity { get; set; } = 1.0f;
        public Int32 BlendShapesAmount { get; set; }
        public Single[] BlendShapesValues { get; set; }

        List<IndexedBufferArray> modelBuffers = new();
        Dictionary<Int32, TextureBuffer> textureBuffers = new();

        public BlendSkin(BlendSkinData blendSkin)
        {
            if (blendSkin.Blends.Count > 0)
            {
                BlendShapesAmount = blendSkin.Blends[0].Models[0].BlendFaces.Count;
            }
            BlendShapesValues = new Single[15];

            foreach (var blend in blendSkin.Blends)
            {
                foreach (var model in blend.Models)
                {
                    var matData = AssetManager.Get().GetAssetData<MaterialData>(blend.Material);
                    var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
                    if (texturedShaderIndex != -1)
                    {
                        var buffer = BufferGeneration.GetModelBuffer(model.Vertexes.Select(v => v.Position).ToList(), model.Faces,
                            model.Vertexes.Select((v) =>
                            {
                                var col = v.Color.GetColor();
                                return System.Drawing.Color.FromArgb((int)col.ToARGB());
                            }).ToList(),
                            model.Vertexes.Select(v => v.UV).ToList(),
                            model.BlendFaces);
                        modelBuffers.Add(buffer);
                        var tex = AssetManager.Get().GetAssetData<TextureData>(matData.Shaders[texturedShaderIndex].TextureId);
                        textureBuffers.Add(modelBuffers.Count - 1, new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap));
                    }
                    else
                    {
                        modelBuffers.Add(BufferGeneration.GetModelBuffer(model.Vertexes.Select(v => v.Position).ToList(), model.Faces,
                            model.Vertexes.Select((v) =>
                            {
                                var col = v.Color.GetColor();
                                return System.Drawing.Color.FromArgb((int)col.ToARGB());
                            }).ToList()));
                    }
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
                tex.Value.Delete();
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
                if (textureBuffers.ContainsKey(i))
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
