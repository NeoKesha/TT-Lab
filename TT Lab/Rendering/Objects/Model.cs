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
        IndexedBufferArray modelBuffer;

        public Model(ModelData model)
        {
            modelBuffer = BufferGeneration.GetModelBuffer(model.Vertexes.Select(v => v.Position).ToList(), model.Faces);
        }

        public void Bind()
        {
            modelBuffer.Bind();
        }

        public void Delete()
        {
            modelBuffer.Delete();
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
            GL.DrawElements(PrimitiveType.Triangles, modelBuffer.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            modelBuffer.Unbind();
        }
    }
}
