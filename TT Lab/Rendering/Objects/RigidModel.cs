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

namespace TT_Lab.Rendering.Objects
{
    public class RigidModel : IRenderable
    {
        List<IndexedBufferArray> modelBuffers = new List<IndexedBufferArray>();
        List<TextureBuffer> textureBuffers = new List<TextureBuffer>();

        public RigidModel(RigidModelData rigid)
        {
            var model = (ModelData)ProjectManagerSingleton.PM.OpenedProject.GetAsset(rigid.Model).GetData();
            var materials = rigid.Materials;
            for (var i = 0; i < model.Vertexes.Count; ++i)
            {
                var matData = (MaterialData)ProjectManagerSingleton.PM.OpenedProject.GetAsset(materials[i]).GetData();
                if (matData.Shaders.Any(s => s.TxtMapping == Twinsanity.TwinsanityInterchange.Common.TwinShader.TextureMapping.ON))
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(model.Vertexes[i].Select(v => v.Position).ToList(), model.Faces[i],
                        model.Vertexes[i].Select((v) =>
                        {
                            var col = v.Color.GetColor();
                            return System.Drawing.Color.FromArgb((int)col.ToARGB());
                        }).ToList(),
                        model.Vertexes[i].Select(v => v.UV).ToList()));
                    var tex = (TextureData)ProjectManagerSingleton.PM.OpenedProject.GetAsset(matData.Shaders[0].TextureId).GetData();
                    var bitmapBits = tex.Bitmap.LockBits(new System.Drawing.Rectangle(0, 0, tex.Bitmap.Width, tex.Bitmap.Height),
                        System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    textureBuffers.Add(new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, bitmapBits.Scan0));
                    tex.Bitmap.UnlockBits(bitmapBits);
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
            for (var i = 0; i < modelBuffers.Count; ++i)
            {
                if (textureBuffers.Count != 0)
                {
                    textureBuffers[i].Bind();
                }
                modelBuffers[i].Bind();
                GL.DrawElements(PrimitiveType.Triangles, modelBuffers[i].Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                if (textureBuffers.Count != 0)
                {
                    textureBuffers[i].Unbind();
                }
                modelBuffers[i].Unbind();
            }
        }

        public void Unbind()
        {
        }
    }
}
