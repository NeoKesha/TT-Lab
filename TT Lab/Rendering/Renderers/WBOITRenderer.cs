using SharpGL;
using System;
using System.Collections.Generic;
using TT_Lab.Extensions;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;

namespace TT_Lab.Rendering.Renderers
{
    public class WBOITRenderer : IRenderer
    {
        public OpenGL GL { get; private set; }
        public ShaderProgram RenderProgram { get => wboitShader; }

        private readonly TextureBuffer screenTexture;
        private readonly TextureBuffer colorTexture;
        private readonly TextureBuffer alphaTexture;
        private readonly FrameBuffer   framebuffer;
        private readonly ShaderProgram resultImageShader;
        private readonly ShaderProgram wboitShader;
        private readonly ShaderProgram opaqueShader;
        private readonly TextureBuffer colorTextureNT;

        public WBOITRenderer(OpenGL gl, ShaderStorage storage, TextureBuffer colorTextureNT, RenderBuffer depthBuffer, float width, float height, ShaderStorage.LibraryFragmentShaders fragmentLib, ShaderStorage.LibraryVertexShaders vertexLib)
        {
            GL = gl;
            resultImageShader = storage.BuildShaderProgram(GL, ShaderStorage.StoredVertexShaders.WboitScreenResult, ShaderStorage.StoredFragmentShaders.WboitScreenResult, vertexLib, fragmentLib);
            wboitShader = storage.BuildShaderProgram(GL, ShaderStorage.StoredVertexShaders.WboitBlend, ShaderStorage.StoredFragmentShaders.WboitBlend, vertexLib, fragmentLib);
            opaqueShader = storage.BuildShaderProgram(GL, ShaderStorage.StoredVertexShaders.ModelRender, ShaderStorage.StoredFragmentShaders.ModelTextured, vertexLib, fragmentLib);
            this.colorTextureNT = colorTextureNT;

            screenTexture = new TextureBuffer(GL);
            colorTexture = new TextureBuffer(GL, TextureBuffer.TextureTarget.Texture2DMultisample);
            alphaTexture = new TextureBuffer(GL, TextureBuffer.TextureTarget.Texture2DMultisample);
            framebuffer = new FrameBuffer(GL);

            ReallocateFramebuffer((int)width, (int)height);
            framebuffer.Bind();
            GL.FramebufferTexture2DEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, (uint)TextureBuffer.TextureTarget.Texture2DMultisample, colorTexture.Buffer, 0);
            GL.FramebufferTexture2DEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT1_EXT, (uint)TextureBuffer.TextureTarget.Texture2DMultisample, alphaTexture.Buffer, 0);
            uint[] attachments = new uint[2] { OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_COLOR_ATTACHMENT1_EXT };
            GL.DrawBuffers(2, attachments);
            GL.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER, depthBuffer.Buffer);
        }

        public void Delete()
        {
            screenTexture.Delete();
            framebuffer.Delete();
            colorTexture.Delete();
            alphaTexture.Delete();
        }

        public void ReallocateFramebuffer(int width, int height)
        {
            screenTexture.Bind();
            GL.TexImage2D((uint)TextureBuffer.TextureTarget.Texture2D, 0, (uint)TextureBuffer.PixelInternalFormat.Rgb, width, height, 0, (uint)TextureBuffer.PixelFormat.Rgb, (uint)TextureBuffer.PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter((uint)TextureBuffer.TextureTarget.Texture2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            GL.TexParameter((uint)TextureBuffer.TextureTarget.Texture2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);

            uint numOfSamples = 4;
            colorTexture?.Bind();
            GL.TexStorage2DMultisample((uint)TextureBuffer.TextureTarget.Texture2DMultisample, numOfSamples, (uint)TextureBuffer.PixelInternalFormat.Rgba16f, (uint)width, (uint)height, true);
            alphaTexture?.Bind();
            GL.TexStorage2DMultisample((uint)TextureBuffer.TextureTarget.Texture2DMultisample, numOfSamples, (uint)TextureBuffer.PixelInternalFormat.Rgba16f, (uint)width, (uint)height, true);
        }

        public void Render(List<IRenderable> objects)
        {
            opaqueShader.Bind();
            foreach (var @object in objects)
            {
                @object.Render(opaqueShader, false);
            }
            opaqueShader.Unbind();

            framebuffer.Bind();
            float[] clearColor = { 0f, 0f, 0f, 0f };
            float[] clearAlpha = { 1f, 1f, 1f, 1f };
            GL.ClearBuffer(OpenGL.GL_COLOR, 0, clearColor);
            GL.ClearBuffer(OpenGL.GL_COLOR, 1, clearAlpha);
            GL.MemoryBarrier(OpenGLExtension.GL_FRAMEBUFFER_BARRIER_BIT);
            // Setup transparent rendering
            GL.Enable(OpenGL.GL_DEPTH_TEST);
            GL.DepthMask(0);
            GL.DepthFunc(OpenGL.GL_LEQUAL);
            GL.Disable(OpenGL.GL_CULL_FACE);
            GL.Enable(OpenGL.GL_MULTISAMPLE);
            GL.Enable(OpenGL.GL_BLEND);
            GL.BlendFunc(0, OpenGL.GL_ONE, OpenGL.GL_ONE);
            GL.BlendEquation(0, OpenGL.GL_FUNC_ADD_EXT);
            GL.BlendFunc(1, OpenGL.GL_DST_COLOR, OpenGL.GL_ZERO);
            GL.BlendEquation(1, OpenGL.GL_FUNC_ADD_EXT);
            wboitShader.Bind();
            foreach (var @object in objects)
            {
                @object.Render(wboitShader, true);
            }
            wboitShader.Unbind();
            GL.DepthMask(1);
            GL.Disable(OpenGL.GL_BLEND);
            // Render result image
            GL.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, 0);
            GL.BlendFunc(OpenGL.GL_ONE_MINUS_SRC_ALPHA, OpenGL.GL_SRC_ALPHA);
            GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
            GL.MemoryBarrier(OpenGLExtension.GL_TEXTURE_FETCH_BARRIER_BIT);
            resultImageShader.Bind();
            resultImageShader.SetTextureUniform("colorTextureNT", TextureBuffer.TextureTarget.Texture2DMultisample, colorTextureNT.Buffer, 0);
            resultImageShader.SetTextureUniform("colorTexture", TextureBuffer.TextureTarget.Texture2DMultisample, colorTexture.Buffer, 1);
            resultImageShader.SetTextureUniform("alphaTexture", TextureBuffer.TextureTarget.Texture2DMultisample, alphaTexture.Buffer, 2);
            GL.Enable(OpenGL.GL_MULTISAMPLE);
            GL.Disable(OpenGL.GL_DEPTH_TEST);
            GL.DrawArrays(OpenGL.GL_TRIANGLE_FAN, 0, 4);
        }

        public void PostProcess()
        {

        }
    }
}
