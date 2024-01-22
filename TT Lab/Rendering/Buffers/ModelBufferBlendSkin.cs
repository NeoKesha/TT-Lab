using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Rendering.Buffers
{
    public class ModelBufferBlendSkin : ModelBuffer
    {

        public Single[] BlendShapesValues { get; set; }

        readonly Int32 blendShapesAmount;
        readonly Vector3 blendShape;
        readonly System.Drawing.Bitmap morphTexture;
        readonly TextureBuffer morphBuffer;
        readonly Dictionary<Int32, Int32> morphStartOffset = new();
        readonly Dictionary<(Int32, Int32), Int32> morphShapesOffsets = new();

        const Int32 MORPH_DATA_TEXTURE_SIZE = 256;

        public ModelBufferBlendSkin(Scene root, BlendSkinData blendSkin) : base(root, blendSkin)
        {
            if (blendSkin.Blends.Count > 0)
            {
                blendShapesAmount = blendSkin.Blends[0].Models[0].BlendFaces.Count;
                blendShape = blendSkin.Blends[0].Models[0].BlendShape;
            }
            BlendShapesValues = new Single[15];

            var morphData = new UInt32[MORPH_DATA_TEXTURE_SIZE * MORPH_DATA_TEXTURE_SIZE];
            var morphDataHandle = GCHandle.Alloc(morphData, GCHandleType.Pinned);
            var morphBmp = new System.Drawing.Bitmap(MORPH_DATA_TEXTURE_SIZE, MORPH_DATA_TEXTURE_SIZE, MORPH_DATA_TEXTURE_SIZE * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, morphDataHandle.AddrOfPinnedObject());

            var offset = 0;
            var morphStartCount = 0;
            foreach (var blend in blendSkin.Blends)
            {
                foreach (var model in blend.Models)
                {
                    var indices = new List<Int32>();
                    foreach (var face in model.Faces)
                    {
                        indices.Add(face.Indexes![0]);
                        indices.Add(face.Indexes[1]);
                        indices.Add(face.Indexes[2]);
                    }

                    morphStartOffset.Add(morphStartCount, offset);
                    var shapeId = 0;
                    var shapeOffset = 0;
                    foreach (var shape in model.BlendFaces)
                    {
                        morphShapesOffsets[(shapeId++, morphStartCount)] = shapeOffset;
                        foreach (var index in indices)
                        {
                            var twinVertex = new VertexBlendShape
                            {
                                Offset = shape.BlendShapes[index].Offset,
                                BlendShape = blendShape
                            };
                            var converted = twinVertex.GetVector4();
                            morphData[offset++] = (new Color((Byte)converted.GetBinaryX(), (Byte)converted.GetBinaryY(), (Byte)converted.GetBinaryZ(), (Byte)converted.GetBinaryW())).ToABGR();
                        }
                        shapeOffset += indices.Count;
                    }

                    morphStartCount++;
                }
            }

            blendShape ??= new Vector3(1.0f, 1.0f, 1.0f);

            morphTexture = morphBmp;
            morphBuffer = new TextureBuffer(TextureTarget.Texture2D, MORPH_DATA_TEXTURE_SIZE, MORPH_DATA_TEXTURE_SIZE, morphTexture, false, System.Drawing.Imaging.PixelFormat.Format32bppArgb, PixelFormat.Rgba, PixelInternalFormat.Rgba8Snorm, PixelType.Byte);

            morphDataHandle.Free();
        }

        public override void Bind()
        {
            base.Bind();

            var renderProgram = Root.Renderer.RenderProgram;
            renderProgram.SetUniform3("BlendShape", -blendShape.X, blendShape.Y, blendShape.Z);
            renderProgram.SetTextureUniform("Morphs", TextureTarget.Texture2D, morphBuffer.Buffer, 6);
            for (Int32 i = 0; i < blendShapesAmount; i++)
            {
                renderProgram.SetUniform1($"MorphWeights[{i}]", BlendShapesValues[i]);
            }
        }

        protected override void RenderSelf()
        {
            Bind();
            var renderProgram = Root.Renderer.RenderProgram;
            var shapeStart = 0;
            for (var i = 0; i < modelBuffers.Count; ++i)
            {
                if (textureBuffers.TryGetValue(i, out TextureBuffer? value))
                {
                    renderProgram.SetTextureUniform("Texture[0]", TextureTarget.Texture2D, value.Buffer, 0);
                }
                for (var j = 0; j < blendShapesAmount; j++)
                {
                    renderProgram.SetUniform1($"ShapeOffset[{j}]", morphShapesOffsets[(j, i)]);
                }
                renderProgram.SetUniform1("ShapeStart", morphStartOffset[i]);
                modelBuffers[i].Bind();
                GL.DrawElements(PrimitiveType.Triangles, modelBuffers[i].Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                modelBuffers[i].Unbind();
                shapeStart += modelBuffers[i].Indices.Length * blendShapesAmount;
            }
            Unbind();
        }

        public override void Delete()
        {
            morphBuffer.Delete();
            morphTexture.Dispose();

            base.Delete();
        }

        public override void Unbind()
        {
            base.Unbind();
        }
    }
}
