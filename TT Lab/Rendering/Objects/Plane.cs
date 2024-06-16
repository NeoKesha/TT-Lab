﻿using GlmSharp;
using SharpGL;
using SharpGL.Enumerations;
using System;
using System.Collections.Generic;
using System.Drawing;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;

namespace TT_Lab.Rendering.Objects
{
    public class Plane : BaseRenderable
    {
        IndexedBufferArray planeBuffer;
        TextureBuffer? texture;

        public Plane(OpenGL gl, GLWindow window, Scene root) : this(gl, window, root, new vec3())
        {
        }

        public Plane(OpenGL gl, GLWindow window, Scene root, vec3 position) : base(gl, window, root)
        {
            planeBuffer = BufferGeneration.GetModelBuffer(GL,
                new List<vec3>
                {
                    new vec3(1 + position.x, 1 + position.y, -1 + position.z),
                    new vec3(-1 + position.x, 1 + position.y, -1 + position.z),
                    new vec3(-1 + position.x, -1 + position.y, -1 + position.z),
                    new vec3(1 + position.x, -1 + position.y, -1 + position.z)
                },
                new List<AssetData.Graphics.SubModels.IndexedFace>
                {
                    new AssetData.Graphics.SubModels.IndexedFace(new int[] { 0, 1, 2 }),
                    new AssetData.Graphics.SubModels.IndexedFace(new int[] { 2, 3, 0 })
                },
                new List<System.Drawing.Color> { System.Drawing.Color.White },
                new List<vec3>
                {
                    new vec3(0, 0, 0),
                    new vec3(1, 0, 0),
                    new vec3(1, 1, 0),
                    new vec3(0, 1, 0)
                });
        }

        public Plane(OpenGL gl, GLWindow window, Scene root, MaterialData material) : this(gl, window, root)
        {
            if (material.Shaders[0].TxtMapping == Twinsanity.TwinsanityInterchange.Common.TwinShader.TextureMapping.ON)
            {
                var tex = AssetManager.Get().GetAssetData<TextureData>(material.Shaders[0].TextureId);
                texture = new TextureBuffer(GL, tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap);
            }
        }

        public Plane(OpenGL gl, GLWindow window, Scene root, TextureData tex) : this(gl, window, root, tex.Bitmap)
        {
        }

        public Plane(OpenGL gl, GLWindow window, Scene root, Bitmap texImage) : this(gl, window, root)
        {
            texture = new TextureBuffer(GL, texImage.Width, texImage.Height, texImage);
        }

        public Plane(OpenGL gl, GLWindow window, Scene root, TextureBuffer texture) : this(gl, window, root)
        {
            this.texture = texture;
        }

        public void Bind()
        {
            if (texture != null)
            {
                Window.Renderer?.RenderProgram.SetTextureUniform("Texture[0]", TextureBuffer.TextureTarget.Texture2D, texture.Buffer, 0);
            }
            Window.Renderer?.RenderProgram.SetUniform1("Opacity", Opacity);
            planeBuffer.Bind();
        }

        public void Delete()
        {
            texture?.Delete();
            planeBuffer.Delete();
        }

        protected override void RenderSelf(ShaderProgram shader)
        {
            Bind();
            GL.DrawElements(OpenGL.GL_TRIANGLES, planeBuffer.Indices.Length, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
            Unbind();
        }

        public void Unbind()
        {
            texture?.Unbind();
            planeBuffer.Unbind();
        }
    }
}
