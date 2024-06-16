using SharpGL;
using SharpGL.Enumerations;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Shaders;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Rendering.Buffers
{
    public class ModelBuffer : BaseRenderable
    {
        protected List<IndexedBufferArray> modelBuffers = new();
        protected Dictionary<Int32, TextureBuffer> textureBuffers = new();

        public ModelBuffer(OpenGL gl, GLWindow window, Scene root, ModelData model) : base(gl, window, root)
        {
            for (var i = 0; i < model.Vertexes.Count; ++i)
            {
                modelBuffers.Add(BufferGeneration.GetModelBuffer(gl, model.Vertexes[i], model.Faces[i], false));
            }
        }

        public ModelBuffer(OpenGL gl, GLWindow window, Scene root, RigidModelData rigid) : base(gl, window, root)
        {
            var model = AssetManager.Get().GetAssetData<ModelData>(rigid.Model);
            var materials = rigid.Materials;
            for (var i = 0; i < model.Vertexes.Count; ++i)
            {
                var matData = AssetManager.Get().GetAssetData<MaterialData>(materials[i]);
                var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
                if (texturedShaderIndex == -1)
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(gl, model.Vertexes[i], model.Faces[i], false));
                    continue;
                }

                modelBuffers.Add(BufferGeneration.GetModelBuffer(gl, model.Vertexes[i], model.Faces[i]));
                var tex = AssetManager.Get().GetAssetData<TextureData>(matData.Shaders[texturedShaderIndex].TextureId);
                var alphaBlendingEnabled = matData.Shaders[texturedShaderIndex].ABlending == TwinShader.AlphaBlending.ON;
                if (alphaBlendingEnabled)
                {
                    EnableAlphaBlending();
                }
                textureBuffers.Add(modelBuffers.Count - 1, new TextureBuffer(gl, tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap));
            }
        }

        public ModelBuffer(OpenGL gl, GLWindow window, Scene root, SkinData skin) : base(gl, window, root)
        {
            foreach (var subSkin in skin.SubSkins)
            {
                var matData = AssetManager.Get().GetAssetData<MaterialData>(subSkin.Material);
                var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
                if (texturedShaderIndex == -1)
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(gl, subSkin.Vertexes, subSkin.Faces, false));
                    continue;
                }

                var buffer = BufferGeneration.GetModelBuffer(gl, subSkin.Vertexes, subSkin.Faces);
                modelBuffers.Add(buffer);
                var tex = AssetManager.Get().GetAssetData<TextureData>(matData.Shaders[texturedShaderIndex].TextureId);
                var alphaBlendingEnabled = matData.Shaders[texturedShaderIndex].ABlending == TwinShader.AlphaBlending.ON;
                if (alphaBlendingEnabled)
                {
                    EnableAlphaBlending();
                }
                textureBuffers.Add(modelBuffers.Count - 1, new TextureBuffer(gl, tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap));
            }
        }

        protected ModelBuffer(OpenGL gl, GLWindow window, Scene root, BlendSkinData blendSkin) : base(gl, window, root)
        {
            foreach (var blend in blendSkin.Blends)
            {
                foreach (var model in blend.Models)
                {
                    var matData = AssetManager.Get().GetAssetData<MaterialData>(blend.Material);
                    var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
                    var buffer = BufferGeneration.GetModelBuffer(gl, model.Vertexes, model.Faces);
                    modelBuffers.Add(buffer);
                    if (texturedShaderIndex != -1)
                    {
                        var tex = AssetManager.Get().GetAssetData<TextureData>(matData.Shaders[texturedShaderIndex].TextureId);
                        var alphaBlendingEnabled = matData.Shaders[texturedShaderIndex].ABlending == TwinShader.AlphaBlending.ON;
                        if (alphaBlendingEnabled)
                        {
                            EnableAlphaBlending();
                        }
                        textureBuffers.Add(modelBuffers.Count - 1, new TextureBuffer(gl, tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap));
                    }
                }
            }
        }

        public override void SetUniforms(ShaderProgram shader)
        {
        }

        public virtual void Bind()
        {
        }

        protected override void RenderSelf(ShaderProgram shader)
        {
            Bind();
            for (var i = 0; i < modelBuffers.Count; ++i)
            {
                if (textureBuffers.TryGetValue(i, out TextureBuffer? value))
                {
                    shader.SetTextureUniform("Texture[0]", TextureBuffer.TextureTarget.Texture2D, value.Buffer, 0);
                }
                modelBuffers[i].Bind();
                unsafe
                {
                    GL.DrawElements(OpenGL.GL_TRIANGLES, modelBuffers[i].Indices.Length, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                }
                modelBuffers[i].Unbind();
            }
            shader.SetTextureUniform("Texture[0]", TextureBuffer.TextureTarget.Texture2D, 0, 0);
            Unbind();
        }

        public virtual void Delete()
        {
            foreach (var tex in textureBuffers)
            {
                tex.Value.Delete();
            }
            foreach (var model in modelBuffers)
            {
                model.Delete();
            }
        }

        public virtual void Unbind()
        {
        }
    }
}
