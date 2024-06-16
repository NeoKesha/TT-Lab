using SharpGL;

// Credits to https://github.com/dwmkerr/sharpgl
namespace TT_Lab.Rendering.Buffers
{
    /// <summary>
    /// A VertexBufferArray is a logical grouping of VertexBuffers. Vertex Buffer Arrays
    /// allow us to use a set of vertex buffers for vertices, indicies, normals and so on,
    /// without having to use more complicated interleaved arrays.
    /// </summary>
    public class VertexBufferArray : IGLObject
    {
        public OpenGL GL { get; private set; }

        public VertexBufferArray(OpenGL gl)
        {
            GL = gl;

            //  Generate the vertex array.
            uint[] ids = new uint[1];
            GL.GenVertexArrays(1, ids);
            vertexArrayObject = ids[0];
        }

        public void Delete()
        {
            GL.DeleteVertexArrays(1, new uint[] { vertexArrayObject });
        }

        public void Bind()
        {
            GL.BindVertexArray(vertexArrayObject);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Gets the vertex buffer array object.
        /// </summary>
        public uint VertexBufferArrayObject
        {
            get { return vertexArrayObject; }
        }

        private uint vertexArrayObject;
    }
}
