using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Rendering.Objects
{
    public class BlendSkin : IRenderable
    {
        public Scene? Parent { get; set; }
        public Single Opacity { get; set; } = 1.0f;
        public Int32 BlendShapesAmount { get; set; }
        public Single[] BlendShapesValues { get; set; }
        public Vector3 BlendShape { get; set; }

        List<IndexedBufferArray> modelBuffers = new();
        Dictionary<Int32, TextureBuffer> textureBuffers = new();
        System.Drawing.Bitmap morphTexture;
        TextureBuffer morphBuffer;
        Dictionary<Int32, Int32> MorphStartOffset = new();
        Dictionary<(Int32, Int32), Int32> MorphShapesOffsets = new();

        public BlendSkin(BlendSkinData blendSkin)
        {
            if (blendSkin.Blends.Count > 0)
            {
                BlendShapesAmount = blendSkin.Blends[0].Models[0].BlendFaces.Count;
            }
            BlendShapesValues = new Single[15];
            BlendShapesValues[1] = 1.0f;

            var morphData = new Byte[512 * 512];
            var morphDataHandle = GCHandle.Alloc(morphData, GCHandleType.Pinned);
            var morphBmp = new System.Drawing.Bitmap(512, 512, 512, System.Drawing.Imaging.PixelFormat.Format8bppIndexed, morphDataHandle.AddrOfPinnedObject());

            var offset = 0;
            foreach (var blend in blendSkin.Blends)
            {
                foreach (var model in blend.Models)
                {
                    BlendShape ??= model.BlendShape;

                    var matData = AssetManager.Get().GetAssetData<MaterialData>(blend.Material);
                    var texturedShaderIndex = matData.Shaders.FindIndex(0, s => s.TxtMapping == TwinShader.TextureMapping.ON);
                    if (texturedShaderIndex != -1)
                    {
                        var buffer = BufferGeneration.GetModelBuffer(model.Vertexes.Select(v => v.Position).ToList(), model.Faces,
                            model.Vertexes.Select((v) =>
                            {
                                var col = v.Color.GetColor();
                                return System.Drawing.Color.FromArgb((int)col.ToARGB());
                            }).ToList(),
                            model.Vertexes.Select(v => v.UV).ToList(),
                            model.BlendFaces);
                        modelBuffers.Add(buffer);
                        var tex = AssetManager.Get().GetAssetData<TextureData>(matData.Shaders[texturedShaderIndex].TextureId);
                        textureBuffers.Add(modelBuffers.Count - 1, new TextureBuffer(tex.Bitmap.Width, tex.Bitmap.Height, tex.Bitmap));
                    }
                    else
                    {
                        modelBuffers.Add(BufferGeneration.GetModelBuffer(model.Vertexes.Select(v => v.Position).ToList(), model.Faces,
                            model.Vertexes.Select((v) =>
                            {
                                var col = v.Color.GetColor();
                                return System.Drawing.Color.FromArgb((int)col.ToARGB());
                            }).ToList()));
                    }

                    var indices = new List<Int32>();
                    foreach (var face in model.Faces)
                    {
                        indices.Add(face.Indexes[0]);
                        indices.Add(face.Indexes[1]);
                        indices.Add(face.Indexes[2]);
                    }

                    MorphStartOffset.Add(modelBuffers.Count - 1, offset);
                    var shapeId = 0;
                    foreach (var shape in model.BlendFaces)
                    {
                        MorphShapesOffsets[(shapeId++, modelBuffers.Count - 1)] = offset;
                        foreach (var index in indices)
                        {
                            var twinVertex = new VertexBlendShape
                            {
                                Offset = shape.BlendShapes[index].Offset,
                                BlendShape = BlendShape
                            };
                            var converted = twinVertex.GetVector4();
                            morphData[offset] = (Byte)converted.GetBinaryX();
                            morphData[offset + 1] = (Byte)converted.GetBinaryY();
                            morphData[offset + 2] = (Byte)converted.GetBinaryZ();
                            morphData[offset + 3] = (Byte)converted.GetBinaryW();
                            offset += 4;
                        }
                    }
                }
            }

            morphTexture = morphBmp;
            morphBuffer = new TextureBuffer(TextureTarget.Texture2D, 512, 512, morphTexture, false, System.Drawing.Imaging.PixelFormat.Format8bppIndexed, PixelFormat.Red, PixelInternalFormat.R8Snorm, PixelType.Byte);

            morphDataHandle.Free();
        }

        public void Bind()
        {
            var renderProgram = Parent?.Renderer.RenderProgram;
            if (renderProgram == null) return;

            renderProgram.SetUniform1("Alpha", Opacity);
            renderProgram.SetUniform1("BlendShapesAmount", BlendShapesAmount);
            renderProgram.SetUniform3("BlendShape", BlendShape.X, BlendShape.Y, BlendShape.Z);
            renderProgram.SetTextureUniform("Morphs", TextureTarget.Texture2D, morphBuffer.Buffer, 2);
            for (Int32 i = 0; i < BlendShapesAmount; i++)
            {
                renderProgram.SetUniform1($"MorphWeights[{i}]", BlendShapesValues[i]);
            }
        }

        public void Delete()
        {
            morphBuffer.Delete();
            morphTexture.Dispose();
            foreach (var tex in textureBuffers)
            {
                tex.Value.Delete();
            }
            foreach (var model in modelBuffers)
            {
                model.Delete();
            }
        }

        public void PostRender()
        {
        }

        public void PreRender()
        {
        }

        public void Render()
        {
            Bind();
            if (Parent == null)
            {
                Unbind();
                return;
            }
            var renderProgram = Parent.Renderer.RenderProgram;
            var shapeStart = 0;
            for (var i = 0; i < modelBuffers.Count; ++i)
            {
                if (textureBuffers.ContainsKey(i))
                {
                    renderProgram.SetTextureUniform("tex", TextureTarget.Texture2D, textureBuffers[i].Buffer, 0);
                }
                for (var j = 0; j < BlendShapesAmount; j++)
                {
                    renderProgram.SetUniform1($"ShapeOffset[{j}]", MorphShapesOffsets[(j, i)]);
                }
                renderProgram.SetUniform1("ShapeStart", MorphStartOffset[i]);
                modelBuffers[i].Bind();
                GL.DrawElements(PrimitiveType.Triangles, modelBuffers[i].Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                modelBuffers[i].Unbind();
                shapeStart += modelBuffers[i].Indices.Length * BlendShapesAmount;
            }
            Unbind();
        }

        public void Unbind()
        {
        }
    }
}
