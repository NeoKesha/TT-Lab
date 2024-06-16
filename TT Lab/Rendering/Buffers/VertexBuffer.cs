using GlmSharp;
using SharpGL;
using System;
using System.Linq;
// Credits to https://github.com/dwmkerr/sharpgl
namespace TT_Lab.Rendering.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Very useful reference for management of VBOs and VBAs:
    /// http://stackoverflow.com/questions/8704801/glvertexattribpointer-clarification
    /// </remarks>
    public class VertexBuffer : IGLObject
    {
        public OpenGL GL { get; private set; }

        public VertexBuffer(OpenGL gl)
        {
            GL = gl;

            //  Generate the vertex array.
            uint[] ids = new uint[1];
            GL.GenBuffers(1, ids);
            vertexBufferObject = ids[0];
        }

        public void SetData(uint attributeIndex, float[] rawData, bool isNormalised, int stride)
        {
            //  Set the data, specify its shape and assign it to a vertex attribute (so shaders can bind to it).
            GL.BufferData(OpenGL.GL_ARRAY_BUFFER, rawData, OpenGL.GL_STATIC_DRAW);
            GL.VertexAttribPointer(attributeIndex, stride, OpenGL.GL_FLOAT, isNormalised, 0, IntPtr.Zero);
            GL.EnableVertexAttribArray(attributeIndex);
        }

        public void SetDataArray(uint attributeIndex, float[] rawData, bool isNormalised, int stride)
        {
            //  Set the data, specify its shape and assign it to a vertex attribute (so shaders can bind to it).
            GL.BufferData(OpenGL.GL_ARRAY_BUFFER, rawData, OpenGL.GL_STATIC_DRAW);
            GL.VertexAttribPointer(attributeIndex, stride, OpenGL.GL_FLOAT, isNormalised, 0, IntPtr.Zero);
            Bind();
            GL.EnableVertexAttribArray(attributeIndex);
            Unbind();
        }

        public void SetData(uint attributeIndex, vec3[] data, bool isNormalised, int stride)
        {
            var floats = data.SelectMany(x => x.Values).ToArray();
            //  Set the data, specify its shape and assign it to a vertex attribute (so shaders can bind to it).
            GL.BufferData(OpenGL.GL_ARRAY_BUFFER, floats, OpenGL.GL_STATIC_DRAW);
            GL.VertexAttribPointer(attributeIndex, stride, OpenGL.GL_FLOAT, isNormalised, 0, IntPtr.Zero);
            GL.EnableVertexAttribArray(attributeIndex);
        }

        public void Bind()
        {
            GL.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vertexBufferObject);
        }

        public void Unbind()
        {
            GL.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        public void Delete()
        {
            GL.DeleteBuffers(1, new uint[] { vertexBufferObject });
        }

        public bool IsCreated() { return vertexBufferObject != 0; }

        /// <summary>
        /// Gets the vertex buffer object.
        /// </summary>
        public uint VertexBufferObject
        {
            get { return vertexBufferObject; }
        }

        private uint vertexBufferObject;
    }
}
