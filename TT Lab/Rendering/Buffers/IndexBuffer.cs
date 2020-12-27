using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Credits to https://github.com/dwmkerr/sharpgl
namespace TT_Lab.Rendering.Buffers
{
    public class IndexBuffer : IGLObject
    {
        public IndexBuffer()
        {
            //  Generate the vertex array.
            uint[] ids = new uint[1];
            GL.GenBuffers(1, ids);
            bufferObject = ids[0];
        }

        public void SetData(uint[] rawData)
        {
            GL.BufferData(BufferTarget.ElementArrayBuffer, rawData.Length * sizeof(uint), rawData, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferObject);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Delete()
        {
            GL.DeleteBuffer(bufferObject);
        }

        public bool IsCreated() { return bufferObject != 0; }

        /// <summary>
        /// Gets the index buffer object.
        /// </summary>
        public uint IndexBufferObject
        {
            get { return bufferObject; }
        }

        private uint bufferObject;
    }
}
