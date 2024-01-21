﻿using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Renderers
{
    public class WBOITRenderer : IRenderer
    {
        public Scene Scene { get; set; }
        public ShaderProgram RenderProgram { get => wboitShader; }

        private readonly TextureBuffer screenTexture = new TextureBuffer();
        private readonly TextureBuffer colorTexture = new TextureBuffer(TextureTarget.Texture2DMultisample);
        private readonly TextureBuffer alphaTexture = new TextureBuffer(TextureTarget.Texture2DMultisample);
        private readonly FrameBuffer framebuffer = new FrameBuffer();
        private readonly ShaderProgram resultImageShader;
        private readonly ShaderProgram wboitShader;
        private readonly ShaderProgram opaqueShader;

        public WBOITRenderer(RenderBuffer depthBuffer, float width, float height, ShaderStorage.LibraryFragmentShaders fragmentLib, ShaderStorage.LibraryVertexShaders vertexLib)
        {
            resultImageShader = ShaderStorage.BuildShaderProgram(ShaderStorage.StoredVertexShaders.WboitScreenResult, ShaderStorage.StoredFragmentShaders.WboitScreenResult, vertexLib, fragmentLib);
            wboitShader = ShaderStorage.BuildShaderProgram(ShaderStorage.StoredVertexShaders.WboitBlend, ShaderStorage.StoredFragmentShaders.WboitBlend, vertexLib, fragmentLib);
            opaqueShader = ShaderStorage.BuildShaderProgram(ShaderStorage.StoredVertexShaders.ModelRender, ShaderStorage.StoredFragmentShaders.ModelTextured, vertexLib, fragmentLib);

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
            screenTexture.Delete();
            framebuffer.Delete();
            colorTexture.Delete();
            alphaTexture.Delete();
        }

        public void ReallocateFramebuffer(int width, int height)
        {
            screenTexture.Bind();
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, nint.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            int numOfSamples = 4;
            colorTexture?.Bind();
            GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, numOfSamples, PixelInternalFormat.Rgba16f, width, height, true);
            alphaTexture?.Bind();
            GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, numOfSamples, PixelInternalFormat.Rgba16f, width, height, true);
        }

        public void Render(List<IRenderable> objects)
        {
            framebuffer.Bind();
            float[] clearColor = { 0f, 0f, 0f, 0f };
            float clearAlpha = 1f;
            GL.ClearBuffer(ClearBuffer.Color, 0, clearColor);
            GL.ClearBuffer(ClearBuffer.Color, 1, ref clearAlpha);
            GL.MemoryBarrier(MemoryBarrierFlags.FramebufferBarrierBit);
            // Setup transparent rendering
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(false);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.Multisample);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(0, BlendingFactorSrc.One, BlendingFactorDest.One);
            GL.BlendEquation(0, BlendEquationMode.FuncAdd);
            GL.BlendFunc(1, BlendingFactorSrc.DstColor, BlendingFactorDest.Zero);
            GL.BlendEquation(1, BlendEquationMode.FuncAdd);
            wboitShader.Bind();
            Scene.SetGlobalUniforms(wboitShader);
            foreach (var @object in objects)
            {
                @object.SetUniforms(wboitShader);
                @object.Render();
            }
            wboitShader.Unbind();
            GL.DepthMask(true);
            GL.Disable(EnableCap.Blend);
            // Render result image
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Scene.RenderFramebuffer);
            //GL.BlendFunc(BlendingFactor.OneMinusSrcAlpha, BlendingFactor.SrcAlpha);
            //GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MemoryBarrier(MemoryBarrierFlags.TextureFetchBarrierBit);
            resultImageShader.Bind();
            resultImageShader.SetTextureUniform("colorTextureNT", TextureTarget.Texture2DMultisample, Scene.ColorTextureNT.Buffer, 0);
            resultImageShader.SetTextureUniform("colorTexture", TextureTarget.Texture2DMultisample, colorTexture.Buffer, 1);
            resultImageShader.SetTextureUniform("alphaTexture", TextureTarget.Texture2DMultisample, alphaTexture.Buffer, 2);
            GL.Enable(EnableCap.Multisample);
            GL.Disable(EnableCap.DepthTest);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, 4);
        }

        public void RenderOpaque(List<IRenderable> objects)
        {
            opaqueShader.Bind();
            Scene.SetGlobalUniforms(opaqueShader);
            foreach (var @object in objects)
            {
                @object.SetUniforms(opaqueShader);
                @object.Render();
            }
            opaqueShader.Unbind();
        }

        public void PostProcess()
        {

        }
    }
}
