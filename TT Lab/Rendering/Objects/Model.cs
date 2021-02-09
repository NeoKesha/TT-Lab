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
        public float Opacity { get; set; } = 1.0f;

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
                    }).ToList(),
                    model.Vertexes[i].Select(v => v.Normal).ToList()));
            }
        }

        public void Bind()
        {
            Parent?.Renderer.RenderProgram.SetUniform1("Alpha", Opacity);
        }

        public void Delete()
        {
            foreach (var modelBuffer in modelBuffers)
            {
                modelBuffer.Delete();
            }
        }

        public void Render()
        {
            Bind();
            foreach (var modelBuffer in modelBuffers)
            {
                modelBuffer.Bind();
                GL.DrawElements(PrimitiveType.Triangles, modelBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                modelBuffer.Unbind();
            }
            Unbind();
        }

        public void Unbind()
        {
        }
    }
}
