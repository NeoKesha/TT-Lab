using SharpGL;

namespace TT_Lab.Rendering.Buffers
{
    public class FrameBuffer : IGLObject
    {
        public OpenGL GL { get; private set; }

        private uint frameBuffer = 0;

        public FrameBuffer(OpenGL gl)
        {
            GL = gl;

            var framebuffers = new uint[1];
            GL.GenFramebuffersEXT(1, framebuffers);
            frameBuffer = framebuffers[0];
        }

        public void Bind()
        {
            GL.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, frameBuffer);
        }

        public void Delete()
        {
            if (frameBuffer != 0)
            {
                GL.DeleteFramebuffersEXT(1, new uint[] { frameBuffer });
                frameBuffer = 0;
            }
        }

        public void Unbind()
        {
            GL.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, 0);
        }

        public uint Buffer
        {
            get => frameBuffer;
        }
    }
}
