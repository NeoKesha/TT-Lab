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
    public class DDPRenderer : IRenderer
    {
        public Scene Scene { get; set; }
        public ShaderProgram RenderProgram { get => ddpPeelShader; }

        private readonly TextureBuffer[] dualDepthTex = { new TextureBuffer(TextureTarget.TextureRectangleArb), new TextureBuffer(TextureTarget.TextureRectangleArb) };
        private readonly TextureBuffer[] dualFrontBlenderTex = { new TextureBuffer(TextureTarget.TextureRectangleArb), new TextureBuffer(TextureTarget.TextureRectangleArb) };
        private readonly TextureBuffer[] dualBackTempTex = { new TextureBuffer(TextureTarget.TextureRectangleArb), new TextureBuffer(TextureTarget.TextureRectangleArb) };
        private readonly TextureBuffer dualBackBlenderTex = new TextureBuffer(TextureTarget.TextureRectangleArb);
        private readonly FrameBuffer dualBackBlenderFbo = new FrameBuffer();
        private readonly FrameBuffer dualPeelingSingleFbo = new FrameBuffer();
        private readonly DisplayList displayList = new DisplayList(ListMode.Compile);
        private readonly ShaderProgram ddpInitShader =
            new ShaderProgram(ManifestResourceLoader.LoadTextFile("Shaders\\DDP_init.vert"), ManifestResourceLoader.LoadTextFile("Shaders\\DDP_init.frag"));
        private readonly ShaderProgram ddpPeelShader =
            new ShaderProgram(ManifestResourceLoader.LoadTextFile("Shaders\\DDP_peel.vert"), ManifestResourceLoader.LoadTextFile("Shaders\\DDP_peel.frag"));
        private readonly ShaderProgram ddpBlendShader =
            new ShaderProgram(ManifestResourceLoader.LoadTextFile("Shaders\\DDP_blend.vert"), ManifestResourceLoader.LoadTextFile("Shaders\\DDP_blend.frag"));
        private readonly ShaderProgram ddpFinalShader =
            new ShaderProgram(ManifestResourceLoader.LoadTextFile("Shaders\\DDP_final.vert"), ManifestResourceLoader.LoadTextFile("Shaders\\DDP_final.frag"));

        public DDPRenderer(float width, float height)
        {
            ReallocateFramebuffer((int)width, (int)height);
            dualBackBlenderFbo.Bind();
            GL.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment0Ext,
                TextureTarget.TextureRectangleArb, dualBackBlenderTex.Buffer, 0);
            dualPeelingSingleFbo.Bind();
            var j = 0;
            {
                GL.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment0Ext,
                    TextureTarget.TextureRectangleArb, dualDepthTex[j].Buffer, 0);
                GL.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment1Ext,
                    TextureTarget.TextureRectangleArb, dualFrontBlenderTex[j].Buffer, 0);
                GL.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment2Ext,
                    TextureTarget.TextureRectangleArb, dualBackTempTex[j].Buffer, 0);
            }
            j = 1;
            {
                GL.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment3Ext,
                    TextureTarget.TextureRectangleArb, dualDepthTex[j].Buffer, 0);
                GL.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment4Ext,
                    TextureTarget.TextureRectangleArb, dualFrontBlenderTex[j].Buffer, 0);
                GL.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment5Ext,
                    TextureTarget.TextureRectangleArb, dualBackTempTex[j].Buffer, 0);
            }

            GL.FramebufferTexture2D(FramebufferTarget.FramebufferExt, FramebufferAttachment.ColorAttachment6Ext,
                TextureTarget.TextureRectangleArb, dualBackBlenderTex.Buffer, 0);

            displayList.Bind();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(0.0, 1.0, 0.0, 1.0, 1.0, 1.0);
            GL.Begin(PrimitiveType.Quads);
            {
                GL.Vertex2(-1.0, -1.0);
                GL.Vertex2(1.0, -1.0);
                GL.Vertex2(1.0, 1.0);
                GL.Vertex2(-1.0, 1.0);
            }
            GL.End();
            GL.PopMatrix();
            displayList.Unbind();
        }

        public void Delete()
        {
            for (var i = 0; i < 2; ++i)
            {
                dualDepthTex[i].Delete();
                dualFrontBlenderTex[i].Delete();
                dualBackTempTex[i].Delete();
            }
            dualBackBlenderTex.Delete();
            dualBackBlenderFbo.Delete();
            dualPeelingSingleFbo.Delete();
            ddpInitShader.Delete();
            ddpPeelShader.Delete();
            ddpBlendShader.Delete();
            ddpFinalShader.Delete();
            displayList.Delete();
        }

        public void ReallocateFramebuffer(int width, int height)
        {
            for (var i = 0; i < 2; ++i)
            {
                dualDepthTex[i].Bind();
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                GL.TexImage2D(TextureTarget.TextureRectangleArb, 0, PixelInternalFormat.Rg32f, width, height,
                    0, PixelFormat.Rgb, PixelType.Float, IntPtr.Zero);

                dualFrontBlenderTex[i].Bind();
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                GL.TexImage2D(TextureTarget.TextureRectangleArb, 0, PixelInternalFormat.Rgba, width, height,
                    0, PixelFormat.Rgba, PixelType.Float, IntPtr.Zero);

                dualBackTempTex[i].Bind();
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                GL.TexImage2D(TextureTarget.TextureRectangleArb, 0, PixelInternalFormat.Rgba, width, height,
                    0, PixelFormat.Rgba, PixelType.Float, IntPtr.Zero);
            }
            dualBackBlenderTex.Bind();
            GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.TextureRectangleArb, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexImage2D(TextureTarget.TextureRectangleArb, 0, PixelInternalFormat.Rgb, width, height,
                0, PixelFormat.Rgb, PixelType.Float, IntPtr.Zero);
        }

        public void Render(List<IRenderable> objects)
        {
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            dualPeelingSingleFbo.Bind();
            DrawBuffersEnum[] attachments = new DrawBuffersEnum[2] { DrawBuffersEnum.ColorAttachment1, DrawBuffersEnum.ColorAttachment2 };
            GL.DrawBuffers(2, attachments);
            GL.ClearColor(0f, 0f, 0f, 0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
            GL.ClearColor(-1f, -1f, 0f, 0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Ext.BlendEquation(BlendEquationMode.Max);
            ddpInitShader.Bind();
            Scene.SetPVMNShaderUniforms(ddpInitShader);
            foreach (var @object in objects)
            {
                @object.Render();
            }
            ddpInitShader.Unbind();
            GL.DrawBuffer(DrawBufferMode.ColorAttachment6);
            var backColor = System.Drawing.Color.LightGray.ToArray();
            GL.ClearColor(backColor[0], backColor[1], backColor[2], 0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            var currId = 0;
            var numOfPasses = 4;
            DrawBuffersEnum[] allAttachments = { DrawBuffersEnum.ColorAttachment0, DrawBuffersEnum.ColorAttachment1,
                                                         DrawBuffersEnum.ColorAttachment2, DrawBuffersEnum.ColorAttachment3,
                                                         DrawBuffersEnum.ColorAttachment4, DrawBuffersEnum.ColorAttachment5,
                                                         DrawBuffersEnum.ColorAttachment6};
            for (var pass = 1; pass < numOfPasses; ++pass)
            {
                currId = pass % 2;
                var prevId = 1 - currId;
                var bufId = currId * 3;

                GL.DrawBuffers(2, ref allAttachments[bufId + 1]);
                GL.ClearColor(0f, 0f, 0f, 0f);
                GL.Clear(ClearBufferMask.ColorBufferBit);

                GL.DrawBuffers(1, ref allAttachments[bufId + 0]);
                GL.ClearColor(-1f, -1f, 0f, 0f);
                GL.Clear(ClearBufferMask.ColorBufferBit);

                GL.DrawBuffers(3, ref allAttachments[bufId + 0]);
                GL.Ext.BlendEquation(BlendEquationMode.Max);

                ddpPeelShader.Bind();
                ddpPeelShader.SetTextureUniform("DepthBlenderTex", TextureTarget.TextureRectangleArb, dualDepthTex[prevId].Buffer, 0);
                ddpPeelShader.SetTextureUniform("FrontBlenderTex", TextureTarget.TextureRectangleArb, dualFrontBlenderTex[prevId].Buffer, 1);
                Scene.SetPVMNShaderUniforms(ddpPeelShader);
                foreach (var @object in objects)
                {
                    @object.Render();
                }
                ddpPeelShader.Unbind();

                GL.DrawBuffers(1, ref allAttachments[6]);
                GL.BlendEquation(BlendEquationMode.FuncAdd);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

                ddpBlendShader.Bind();
                ddpBlendShader.SetTextureUniform("TempTex", TextureTarget.TextureRectangleArb, dualBackTempTex[currId].Buffer, 0);
                GL.CallList(displayList.Buffer);
                ddpBlendShader.Unbind();
            }

            GL.Disable(EnableCap.Blend);
            GL.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
            GL.DrawBuffer(DrawBufferMode.Back);

            ddpFinalShader.Bind();
            ddpFinalShader.SetTextureUniform("DepthBlenderTex", TextureTarget.TextureRectangleArb, dualDepthTex[currId].Buffer, 0);
            ddpFinalShader.SetTextureUniform("FrontBlenderTex", TextureTarget.TextureRectangleArb, dualFrontBlenderTex[currId].Buffer, 1);
            ddpFinalShader.SetTextureUniform("BackBlenderTex", TextureTarget.TextureRectangleArb, dualBackBlenderTex.Buffer, 2);
            GL.CallList(displayList.Buffer);
            ddpFinalShader.Unbind();
        }
    }
}
