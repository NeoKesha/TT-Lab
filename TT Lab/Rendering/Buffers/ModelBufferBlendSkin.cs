using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Rendering.Buffers
{
    public class ModelBufferBlendSkin : ModelBuffer
    {
        public Int32 BlendShapesAmount { get; set; }
        public Single[] BlendShapesValues { get; set; }

        Vector3 blendShape;
        System.Drawing.Bitmap morphTexture;
        TextureBuffer morphBuffer;
        Dictionary<Int32, Int32> MorphStartOffset = new();
        Dictionary<(Int32, Int32), Int32> MorphShapesOffsets = new();

        public ModelBufferBlendSkin(Scene root, BlendSkinData blendSkin) : base(root, blendSkin)
        {
            if (blendSkin.Blends.Count > 0)
            {
                BlendShapesAmount = blendSkin.Blends[0].Models[0].BlendFaces.Count;
            }
            BlendShapesValues = new Single[15];

            var morphData = new Byte[512 * 512];
            var morphDataHandle = GCHandle.Alloc(morphData, GCHandleType.Pinned);
            var morphBmp = new System.Drawing.Bitmap(512, 512, 512, System.Drawing.Imaging.PixelFormat.Format8bppIndexed, morphDataHandle.AddrOfPinnedObject());

            var offset = 0;
            var morphStartCount = 0;
            foreach (var blend in blendSkin.Blends)
            {
                foreach (var model in blend.Models)
                {
                    blendShape ??= model.BlendShape;

                    var indices = new List<Int32>();
                    foreach (var face in model.Faces)
                    {
                        indices.Add(face.Indexes![0]);
                        indices.Add(face.Indexes[1]);
                        indices.Add(face.Indexes[2]);
                    }

                    MorphStartOffset.Add(morphStartCount, offset);
                    var shapeId = 0;
                    foreach (var shape in model.BlendFaces)
                    {
                        MorphShapesOffsets[(shapeId++, morphStartCount)] = offset;
                        foreach (var index in indices)
                        {
                            var twinVertex = new VertexBlendShape
                            {
                                Offset = shape.BlendShapes[index].Offset,
                                BlendShape = blendShape
                            };
                            var converted = twinVertex.GetVector4();
                            morphData[offset] = (Byte)converted.GetBinaryX();
                            morphData[offset + 1] = (Byte)converted.GetBinaryY();
                            morphData[offset + 2] = (Byte)converted.GetBinaryZ();
                            morphData[offset + 3] = (Byte)converted.GetBinaryW();
                            offset += 4;
                        }
                    }

                    morphStartCount++;
                }
            }

            blendShape ??= new Vector3(1.0f, 1.0f, 1.0f);

            morphTexture = morphBmp;
            morphBuffer = new TextureBuffer(TextureTarget.Texture2D, 512, 512, morphTexture, false, System.Drawing.Imaging.PixelFormat.Format8bppIndexed, PixelFormat.Red, PixelInternalFormat.R8, PixelType.UnsignedByte);

            morphDataHandle.Free();
        }

        public override void Bind()
        {
            base.Bind();

            var renderProgram = Root.Renderer.RenderProgram;
            renderProgram.SetUniform1("BlendShapesAmount", BlendShapesAmount);
            renderProgram.SetUniform3("BlendShape", -blendShape.X, blendShape.Y, blendShape.Z);
            renderProgram.SetTextureUniform("Morphs", TextureTarget.Texture2D, morphBuffer.Buffer, 2);
            for (Int32 i = 0; i < BlendShapesAmount; i++)
            {
                renderProgram.SetUniform1($"MorphWeights[{i}]", BlendShapesValues[i]);
            }
        }

        public override void Render()
        {
            Bind();
            var renderProgram = Root.Renderer.RenderProgram;
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
