namespace TT_Lab.Rendering.Buffers
{
    /// <summary>
    /// Stores buffered indices for Element rendering. Works just like VertexBufferArray
    /// </summary>
    /// <seealso cref="VertexBufferArray"/>
    public class IndexedBufferArray : IGLObject
    {
        public VertexBufferArray buffer;
        public uint[] Indices;

        public IndexedBufferArray()
        {
            buffer = new VertexBufferArray();
            Indices = new uint[0];
        }

        public void Bind()
        {
            buffer.Bind();
        }

        public void Delete()
        {
            buffer.Delete();
        }

        public void Unbind()
        {
            buffer.Unbind();
        }
    }
}
