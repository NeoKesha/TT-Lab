using SharpGL;

namespace TT_Lab.Rendering.Buffers
{
    /// <summary>
    /// Stores buffered indices for Element rendering. Works just like VertexBufferArray
    /// </summary>
    /// <seealso cref="VertexBufferArray"/>
    public class IndexedBufferArray : IGLObject
    {
        public OpenGL GL { get; private set; }

        public VertexBufferArray Buffer;
        public uint[] Indices;

        public IndexedBufferArray(OpenGL gl)
        {
            GL = gl;

            Buffer = new VertexBufferArray(gl);
            Indices = System.Array.Empty<System.UInt32>();
        }

        public void Bind()
        {
            Buffer.Bind();
        }

        public void Delete()
        {
            Buffer.Delete();
        }

        public void Unbind()
        {
            Buffer.Unbind();
        }
    }
}
