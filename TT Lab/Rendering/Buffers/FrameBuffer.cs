using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Rendering.Buffers
{
    public class FrameBuffer : IGLObject
    {
        private uint frameBuffer = 0;

        public FrameBuffer()
        {
            frameBuffer = (uint)GL.GenFramebuffer();
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBuffer);
        }

        public void Delete()
        {
            if (frameBuffer != 0)
            {
                GL.DeleteFramebuffer(frameBuffer);
                frameBuffer = 0;
            }
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public uint Buffer
        {
            get => frameBuffer;
        }
    }
}
