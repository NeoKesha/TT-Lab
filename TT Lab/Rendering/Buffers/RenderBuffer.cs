using SharpGL;

namespace TT_Lab.Rendering.Buffers
{
    public class RenderBuffer : IGLObject
    {
        public OpenGL GL { get; private set; }

        private uint renderBuffer;

        public RenderBuffer(OpenGL gl)
        {
            GL = gl;

            var buffer = new uint[1];
            GL.GenRenderbuffersEXT(1, buffer);
            renderBuffer = buffer[0];
        }

        public void Bind()
        {
            GL.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, renderBuffer);
        }

        public void Delete()
        {
            GL.DeleteRenderbuffersEXT(1, new uint[] { renderBuffer });
        }

        public void Unbind()
        {
            GL.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER, 0);
        }

        public uint Buffer
        {
            get => renderBuffer;
        }
    }
}
