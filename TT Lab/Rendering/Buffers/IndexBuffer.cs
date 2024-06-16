// Credits to https://github.com/dwmkerr/sharpgl
using SharpGL;
using System;

namespace TT_Lab.Rendering.Buffers
{
    public class IndexBuffer : IGLObject
    {
        public OpenGL GL { get; private set; }

        public IndexBuffer(OpenGL gl)
        {
            GL = gl;

            //  Generate the vertex array.
            uint[] ids = new uint[1];
            GL.GenBuffers(1, ids);
            bufferObject = ids[0];
        }

        public void SetData(uint[] rawData)
        {
            unsafe
            {
                fixed (uint* rawDataPtr = rawData)
                {
                    GL.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, rawData.Length, new IntPtr(rawDataPtr), OpenGL.GL_STATIC_DRAW);
                }
            }
        }

        public void Bind()
        {
            GL.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, bufferObject);
        }

        public void Unbind()
        {
            GL.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, 0);
        }

        public void Delete()
        {
            GL.DeleteBuffers(1, new uint[] { bufferObject });
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
