using OpenTK.Graphics.OpenGL;

namespace TT_Lab.Rendering.Buffers
{
    public class RenderBuffer : IGLObject
    {
        private uint renderBuffer;

        public RenderBuffer()
        {
            renderBuffer = (uint)GL.GenRenderbuffer();
        }

        public void Bind()
        {
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBuffer);
        }

        public void Delete()
        {
            GL.DeleteRenderbuffer(renderBuffer);
        }

        public void Unbind()
        {
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
        }

        public uint Buffer
        {
            get => renderBuffer;
        }
    }
}
