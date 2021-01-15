using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class Model : IRenderable
    {
        public Scene? Parent { get; set; }

        List<IndexedBufferArray> modelBuffers;

        public Model(ModelData model)
        {
            modelBuffers = new List<IndexedBufferArray>();
            for (var i = 0; i < model.Vertexes.Count; ++i)
            {
                modelBuffers.Add(BufferGeneration.GetModelBuffer(model.Vertexes[i].Select(v => v.Position).ToList(), model.Faces[i],
                    model.Vertexes[i].Select((v) =>
                    {
                        var col = v.Color.GetColor();
                        return System.Drawing.Color.FromArgb((int)col.ToARGB());
                    }).ToList()));
            }
        }

        public void Bind()
        {
            foreach (var modelBuffer in modelBuffers)
            {
                modelBuffer.Bind();
            }
            
        }

        public void Delete()
        {
            foreach (var modelBuffer in modelBuffers)
            {
                modelBuffer.Delete();
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
            foreach (var modelBuffer in modelBuffers)
            {
                GL.DrawElements(PrimitiveType.Triangles, modelBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            }
            Unbind();
        }

        public void Unbind()
        {
            foreach (var modelBuffer in modelBuffers)
            {
                modelBuffer.Unbind();
            }
        }
    }
}
