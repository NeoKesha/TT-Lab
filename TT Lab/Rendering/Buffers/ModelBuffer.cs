using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Rendering.Buffers
{
    public class ModelBuffer : BaseRenderable
    {
        protected List<IndexedBufferArray> modelBuffers = new();
        protected Dictionary<Int32, TextureBuffer> textureBuffers = new();

        public ModelBuffer(Scene root, ModelData model) : base(root)
        {
            for (var i = 0; i < model.Vertexes.Count; ++i)
            {
                modelBuffers.Add(BufferGeneration.GetModelBuffer(model.Vertexes[i], model.Faces[i], false));
            }
        }

        public ModelBuffer(Scene root, RigidModelData rigid) : base(root)
        {
            var model = AssetManager.Get().GetAssetData<ModelData>(rigid.Model);
            var materials = rigid.Materials;
            for (var i = 0; i < model.Vertexes.Count; ++i)
            {
                var matData = AssetManager.Get().GetAssetData<MaterialData>(materials[i]);
                var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
                if (texturedShaderIndex == -1)
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(model.Vertexes[i], model.Faces[i], false));
                    continue;
                }

                modelBuffers.Add(BufferGeneration.GetModelBuffer(model.Vertexes[i], model.Faces[i]));
                var tex = AssetManager.Get().GetAssetData<TextureData>(matData.Shaders[texturedShaderIndex].TextureId);
                textureBuffers.Add(modelBuffers.Count - 1, new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap));
            }
        }

        public ModelBuffer(Scene root, SkinData skin) : base(root)
        {
            foreach (var subSkin in skin.SubSkins)
            {
                var matData = AssetManager.Get().GetAssetData<MaterialData>(subSkin.Material);
                var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
                if (texturedShaderIndex == -1)
                {
                    modelBuffers.Add(BufferGeneration.GetModelBuffer(subSkin.Vertexes, subSkin.Faces, false));
                    continue;
                }

                var buffer = BufferGeneration.GetModelBuffer(subSkin.Vertexes, subSkin.Faces);
                modelBuffers.Add(buffer);
                var tex = AssetManager.Get().GetAssetData<TextureData>(matData.Shaders[texturedShaderIndex].TextureId);
                textureBuffers.Add(modelBuffers.Count - 1, new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap));
            }
        }

        public ModelBuffer(Scene root, BlendSkinData blendSkin) : base(root)
        {
            foreach (var blend in blendSkin.Blends)
            {
                foreach (var model in blend.Models)
                {
                    var matData = AssetManager.Get().GetAssetData<MaterialData>(blend.Material);
                    var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
                    var buffer = BufferGeneration.GetModelBuffer(model.Vertexes, model.Faces);
                    modelBuffers.Add(buffer);
                    if (texturedShaderIndex != -1)
                    {
                        var tex = AssetManager.Get().GetAssetData<TextureData>(matData.Shaders[texturedShaderIndex].TextureId);
                        textureBuffers.Add(modelBuffers.Count - 1, new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap));
                    }
                }
            }
        }

        public virtual void Bind()
        {
            Root.Renderer.RenderProgram.SetUniform1("Opacity", Opacity);
        }

        public override void Render()
        {
            Bind();
            for (var i = 0; i < modelBuffers.Count; ++i)
            {
                if (textureBuffers.TryGetValue(i, out TextureBuffer? value))
                {
                    Root?.Renderer.RenderProgram.SetTextureUniform("Texture[0]", TextureTarget.Texture2D, value.Buffer, 0);
                }
                modelBuffers[i].Bind();
                GL.DrawElements(PrimitiveType.Triangles, modelBuffers[i].Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                modelBuffers[i].Unbind();
            }
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
