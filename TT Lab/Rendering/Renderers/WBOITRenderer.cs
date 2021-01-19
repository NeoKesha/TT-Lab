using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Renderers
{
    public class WBOITRenderer : IRenderer
    {
        public Scene Scene { get; set; }
        public ShaderProgram RenderProgram { get => wboitShader; }

        private readonly TextureBuffer colorTexture = new TextureBuffer(TextureTarget.Texture2DMultisample);
        private readonly TextureBuffer alphaTexture = new TextureBuffer(TextureTarget.Texture2DMultisample);
        private readonly FrameBuffer framebuffer = new FrameBuffer();
        private readonly ShaderProgram resultImageShader =
            new ShaderProgram(ManifestResourceLoader.LoadTextFile("Shaders\\ScreenResult.vert"), ManifestResourceLoader.LoadTextFile("Shaders\\ScreenResult.frag"));
        private readonly ShaderProgram wboitShader =
            new ShaderProgram(ManifestResourceLoader.LoadTextFile("Shaders\\WBOIT_blend.vert"), ManifestResourceLoader.LoadTextFile("Shaders\\WBOIT_blend.frag"));

        public WBOITRenderer(RenderBuffer depthBuffer, float width, float height)
        {
            ReallocateFramebuffer((int)width, (int)height);
            framebuffer.Bind();
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2DMultisample, colorTexture.Buffer, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment1, TextureTarget.Texture2DMultisample, alphaTexture.Buffer, 0);
            DrawBuffersEnum[] attachments = new DrawBuffersEnum[2] { DrawBuffersEnum.ColorAttachment0, DrawBuffersEnum.ColorAttachment1 };
            GL.DrawBuffers(2, attachments);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, depthBuffer.Buffer);
        }

        public void Delete()
        {
            resultImageShader.Delete();
            wboitShader.Delete();
            framebuffer.Delete();
            colorTexture.Delete();
            alphaTexture.Delete();
        }

        public void ReallocateFramebuffer(int width, int height)
        {
            int numOfSamples = 4;
            colorTexture?.Bind();
            GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, numOfSamples, PixelInternalFormat.Rgba16f, width, height, true);
            alphaTexture?.Bind();
            GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, numOfSamples, PixelInternalFormat.Rgba16f, width, height, true);
        }

        public void Render(List<IRenderable> objects)
        {
            framebuffer.Bind();
            GL.Enable(EnableCap.Blend);
            // Do blending
            float[] clearColor = { 0f, 0f, 0f, 0f };
            float clearAlpha = 1f;
            GL.ClearBuffer(ClearBuffer.Color, 0, clearColor);
            GL.ClearBuffer(ClearBuffer.Color, 1, ref clearAlpha);
            GL.MemoryBarrier(MemoryBarrierFlags.FramebufferBarrierBit);
            GL.DepthMask(false);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Disable(EnableCap.CullFace);
            GL.BlendFunc(0, BlendingFactorSrc.One, BlendingFactorDest.One);
            GL.BlendFunc(1, BlendingFactorSrc.Zero, BlendingFactorDest.OneMinusSrcAlpha);
            wboitShader.Bind();
            Scene.SetPVMNShaderUniforms(wboitShader);
            foreach (var @object in objects)
            {
                @object.Render();
            }
            wboitShader.Unbind();
            // Render result image
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.BlendFunc(BlendingFactor.OneMinusSrcAlpha, BlendingFactor.SrcAlpha);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MemoryBarrier(MemoryBarrierFlags.TextureFetchBarrierBit);
            resultImageShader.Bind();
            resultImageShader.SetTextureUniform("colorTexture", TextureTarget.Texture2DMultisample, colorTexture.Buffer, 0);
            resultImageShader.SetTextureUniform("alphaTexture", TextureTarget.Texture2DMultisample, alphaTexture.Buffer, 1);
            GL.Enable(EnableCap.Multisample);
            GL.Disable(EnableCap.DepthTest);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, 4);
        }
    }
}
