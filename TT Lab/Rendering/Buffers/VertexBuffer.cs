using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
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
        public VertexBuffer()
        {
            //  Generate the vertex array.
            uint[] ids = new uint[1];
            GL.GenBuffers(1, ids);
            vertexBufferObject = ids[0];
        }

        public void SetData(uint attributeIndex, float[] rawData, bool isNormalised, int stride)
        {
            //  Set the data, specify its shape and assign it to a vertex attribute (so shaders can bind to it).
            GL.BufferData(BufferTarget.ArrayBuffer, rawData.Length * sizeof(float), rawData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeIndex, stride, VertexAttribPointerType.Float, isNormalised, 0, IntPtr.Zero);
            GL.EnableVertexAttribArray(attributeIndex);
        }

        public void SetDataArray(uint attributeIndex, float[] rawData, bool isNormalised, int stride)
        {
            //  Set the data, specify its shape and assign it to a vertex attribute (so shaders can bind to it).
            GL.BufferData(BufferTarget.ArrayBuffer, rawData.Length * sizeof(float), rawData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeIndex, stride, VertexAttribPointerType.Float, isNormalised, 0, IntPtr.Zero);
            GL.EnableVertexArrayAttrib(vertexBufferObject, attributeIndex);
        }

        public void SetData(uint attributeIndex, Vector3[] data, bool isNormalised, int stride)
        {
            //  Set the data, specify its shape and assign it to a vertex attribute (so shaders can bind to it).
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float) * 3, data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeIndex, stride, VertexAttribPointerType.Float, isNormalised, 0, IntPtr.Zero);
            GL.EnableVertexAttribArray(attributeIndex);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Delete()
        {
            GL.DeleteBuffer(vertexBufferObject);
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
