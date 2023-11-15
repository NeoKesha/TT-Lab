namespace TT_Lab.Rendering.Buffers
{
    /// <summary>
    /// Stores buffered indices for Element rendering. Works just like VertexBufferArray
    /// </summary>
    /// <seealso cref="VertexBufferArray"/>
    public class IndexedBufferArray : IGLObject
    {
        public VertexBufferArray Buffer;
        public uint[] Indices;

        public IndexedBufferArray()
        {
            Buffer = new VertexBufferArray();
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
