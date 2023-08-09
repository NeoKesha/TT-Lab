using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Project;
using TT_Lab.Util;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Rendering.Objects
{
    public class Skin : IRenderable
    {
        public Scene? Parent { get; set; }
        public float Opacity { get; set; } = 1.0f;

        List<IndexedBufferArray> modelBuffers = new List<IndexedBufferArray>();
        List<TextureBuffer> textureBuffers = new List<TextureBuffer>();

        public Skin(SkinData skin, List<Guid> materials)
        {
            for (var i = 0; i < skin.Vertexes.Count; ++i)
            {
                var matData = (MaterialData)ProjectManagerSingleton.PM.OpenedProject.GetAsset(materials[i]).GetData();
                if (matData.Shaders.Any(s => s.TxtMapping == Twinsanity.TwinsanityInterchange.Common.TwinShader.TextureMapping.ON))
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(skin.Vertexes[i].Select(v => v.Position).ToList(), skin.Faces[i],
                        skin.Vertexes[i].Select((v) =>
                        {
                            var col = v.Color.GetColor();
                            return System.Drawing.Color.FromArgb((int)col.ToARGB());
                        }).ToList(),
                        skin.Vertexes[i].Select(v => new Vector3(v.UV.X, 1 - v.UV.Y, v.UV.Z)).ToList(),
                        skin.Vertexes[i].Select(v => v.Normal).ToList()));
                    var tex = (TextureData)ProjectManagerSingleton.PM.OpenedProject.GetAsset(matData.Shaders[0].TextureId).GetData();
                    textureBuffers.Add(new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap));
                }
                else
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(skin.Vertexes[i].Select(v => v.Position).ToList(), skin.Faces[i],
                        skin.Vertexes[i].Select((v) =>
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
