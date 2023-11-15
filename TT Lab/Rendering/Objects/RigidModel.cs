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
    public class RigidModel : IRenderable
    {
        public Scene? Parent { get; set; }
        public float Opacity { get; set; } = 1.0f;

        List<IndexedBufferArray> modelBuffers = new();
        Dictionary<Int32, TextureBuffer> textureBuffers = new();

        public RigidModel(RigidModelData rigid)
        {
            var model = AssetManager.Get().GetAssetData<ModelData>(rigid.Model);
            var materials = rigid.Materials;
            for (var i = 0; i < model.Vertexes.Count; ++i)
            {
                var matData = AssetManager.Get().GetAssetData<MaterialData>(materials[i]);
                var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
                if (texturedShaderIndex != -1)
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(model.Vertexes[i].Select(v => v.Position).ToList(), model.Faces[i],
                        model.Vertexes[i].Select((v) =>
                        {
                            var col = v.Color.GetColor();
                            return System.Drawing.Color.FromArgb((int)col.ToARGB());
                        }).ToList(),
                        model.Vertexes[i].Select(v => v.UV).ToList(),
                        model.Vertexes[i].Select(v => v.Normal).ToList()));
                    var tex = AssetManager.Get().GetAssetData<TextureData>(matData.Shaders[texturedShaderIndex].TextureId);
                    textureBuffers.Add(modelBuffers.Count - 1, new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap));
                }
                else
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(model.Vertexes[i].Select(v => v.Position).ToList(), model.Faces[i],
                        model.Vertexes[i].Select((v) =>
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
