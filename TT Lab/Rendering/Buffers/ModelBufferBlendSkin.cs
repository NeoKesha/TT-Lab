using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using GlmSharp;
using org.ogre;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Color = Twinsanity.TwinsanityInterchange.Common.Color;
using PixelFormat = org.ogre.PixelFormat;
using Vector3 = org.ogre.Vector3;

namespace TT_Lab.Rendering.Buffers
{
    public class ModelBufferBlendSkin : ModelBuffer
    {

        private float[] _blendShapesValues;

        private readonly int _blendShapesAmount = 0;
        private readonly vec3 _blendShape = vec3.Ones;
        private readonly Dictionary<int, int> _morphStartOffset = new();
        private readonly Dictionary<(int, int), int> _morphShapesOffsets = new();

        const int MORPH_DATA_TEXTURE_SIZE = 256;

        public ModelBufferBlendSkin(SceneManager sceneManager, string name, BlendSkinData blendSkin) : this(
            sceneManager, sceneManager.getRootSceneNode().createChildSceneNode(), name, blendSkin)
        {
        }

        public ModelBufferBlendSkin(SceneManager sceneManager, SceneNode parent, string name, BlendSkinData blendSkin) : base(sceneManager, parent, name, blendSkin)
        {
            if (blendSkin.Blends.Count > 0)
            {
                _blendShapesAmount = blendSkin.Blends[0].Models[0].BlendFaces.Count;
                var twinShape = blendSkin.Blends[0].Models[0].BlendShape;
                _blendShape = new vec3(twinShape.X, twinShape.Y, twinShape.Z);
            }
            _blendShapesValues = new float[15];

            var morphData = new uint[MORPH_DATA_TEXTURE_SIZE * MORPH_DATA_TEXTURE_SIZE];
            var morphDataHandle = GCHandle.Alloc(morphData, GCHandleType.Pinned);
            var morphBmp = new Bitmap(MORPH_DATA_TEXTURE_SIZE, MORPH_DATA_TEXTURE_SIZE, MORPH_DATA_TEXTURE_SIZE * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, morphDataHandle.AddrOfPinnedObject());

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

                    _morphStartOffset.Add(morphStartCount, offset);
                    var shapeId = 0;
                    var shapeOffset = 0;
                    foreach (var shape in model.BlendFaces)
                    {
                        _morphShapesOffsets[(shapeId++, morphStartCount)] = shapeOffset;
                        foreach (var index in indices)
                        {
                            var twinVertex = new VertexBlendShape
                            {
                                Offset = shape.BlendShapes[index].Offset,
                                BlendShape = new Twinsanity.TwinsanityInterchange.Common.Vector3(_blendShape.x, _blendShape.y, _blendShape.z)
                            };
                            var converted = twinVertex.GetVector4();
                            morphData[offset++] = (new Color((Byte)converted.GetBinaryX(), (Byte)converted.GetBinaryY(), (Byte)converted.GetBinaryZ(), (Byte)converted.GetBinaryW())).ToABGR();
                        }
                        shapeOffset += indices.Count;
                    }

                    morphStartCount++;
                }
            }
            //
            // var morphTexture = morphBmp;
            // TexturePtr ogreTexture;
            //
            // if (!TextureManager.getSingleton().resourceExists($"{name}_MorphTexture"))
            // {
            //     var morphTexturePtr = morphTexture.LockBits(new Rectangle(0, 0, morphBmp.Width, morphBmp.Height),
            //         ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //     org.ogre.Image img = new();
            //     img = img.loadDynamicImage(new SWIGTYPE_p_unsigned_char(morphTexturePtr.Scan0, false),
            //         (uint)morphTexturePtr.Width, (uint)morphTexturePtr.Height, 1, PixelFormat.PF_A8R8G8B8, false,
            //         1,
            //         0);
            //     ogreTexture = TextureManager.getSingleton().create($"{name}_MorphTexture", GlobalConsts.OgreGroup);
            //     ogreTexture.setTextureType(TextureType.TEX_TYPE_2D);
            //     ogreTexture.setFormat(PixelFormat.PF_R8G8B8A8_SNORM);
            //     ogreTexture.setNumMipmaps(0);
            //     ogreTexture.loadImage(img);
            //     morphTexture.UnlockBits(morphTexturePtr);
            //     
            //     ogreTexture.load();
            //     img.Dispose();
            //     morphDataHandle.Free();
            //     morphBmp.Dispose();
            // }
            // else
            // {
            //     ogreTexture = TextureManager.getSingleton().getByName($"{name}_MorphTexture");
            // }
            //
            // //morphBuffer = new TextureBuffer(GL, TbTextureTarget.Texture2D, MORPH_DATA_TEXTURE_SIZE, MORPH_DATA_TEXTURE_SIZE, _morphTexture, false, System.Drawing.Imaging.PixelFormat.Format32bppArgb, TbPixelFormat.Rgba, TbPixelInternalFormat.Rgba8SNorm, TbPixelType.Byte);
            //
            // for (var j = 0; j < MeshNodes.Count; j++)
            // {
            //     var entity = MeshNodes[j].MeshNode.getAttachedObject(0).castEntity();
            //     var subEntity = entity.getSubEntity(0);
            //     subEntity.getMaterial().getTechnique(0).getPass(0).getTextureUnitState(1).setTexture(ogreTexture);
            //     var vertexProgram = subEntity.getMaterial().getTechnique(0).getPass(0).getVertexProgram();
            //     var vertexParams = vertexProgram.getDefaultParameters();
            //     vertexParams.setNamedConstant("uBlendShape", new Vector3(-_blendShape.x, _blendShape.y, _blendShape.z));
            //     vertexParams.setNamedConstant("uShapeStart", _morphStartOffset[j]);
            //     vertexParams.setNamedConstant("uShapeAmounts", _blendShapesAmount);
            //     for (var i = 0; i < _blendShapesAmount; i++)
            //     {
            //         vertexParams.setNamedConstant($"uShapeOffsets[{i}]", _morphShapesOffsets[(i, j)]);
            //     }
            //     
            //     // for (var i = 0; i < 15; ++i)
            //     // {
            //     //     vertexParams.setNamedConstant($"uMorphWeights[{i}]", 0.0f);
            //     // }
            // }
        }

        public void SetBlendShapeWeight(int index, float value)
        {
            _blendShapesValues[index] = value;

            // for (var i = 0; i < MeshNodes.Count; i++)
            // {
            //     var entity = MeshNodes[i].MeshNode.getAttachedObject(0).castEntity();
            //     var subEntity = entity.getSubEntity(0);
            //     var vertexProgram = subEntity.getMaterial().getTechnique(0).getPass(0).getVertexProgram();
            //     var vertexParams = vertexProgram.getDefaultParameters();
            //     vertexParams.setNamedConstant($"uMorphWeights[{index}]", _blendShapesValues[index]);
            // }
        }

        //public override void SetUniforms(ShaderProgram shader)
        //{
        //    base.SetUniforms(shader);

        //    shader.SetUniform3("BlendShape", -blendShape.X, blendShape.Y, blendShape.Z);
        //    shader.SetTextureUniform("Morphs", TbTextureTarget.Texture2D, morphBuffer.Buffer, 6);
        //    for (Int32 i = 0; i < blendShapesAmount; i++)
        //    {
        //        shader.SetUniform1($"MorphWeights[{i}]", BlendShapesValues[i]);
        //    }
        //}

        //public override void Bind()
        //{
        //    base.Bind();
        //}

        //protected override void RenderSelf(ShaderProgram shader)
        //{
        //    Bind();
        //    var shapeStart = 0;
        //    for (var i = 0; i < modelBuffers.Count; ++i)
        //    {
        //        if (textureBuffers.TryGetValue(i, out TextureBuffer? value))
        //        {
        //            shader.SetTextureUniform("Texture[0]", TbTextureTarget.Texture2D, value.Buffer, 0);
        //        }
        //        for (var j = 0; j < blendShapesAmount; j++)
        //        {
        //            shader.SetUniform1($"ShapeOffset[{j}]", morphShapesOffsets[(j, i)]);
        //        }
        //        shader.SetUniform1("ShapeStart", morphStartOffset[i]);
        //        modelBuffers[i].Bind();
        //        unsafe
        //        {
        //            DrawElements(GL, OpenGL.GL_TRIANGLES, modelBuffers[i].Indices.Length, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
        //        }
        //        modelBuffers[i].Unbind();
        //        shapeStart += modelBuffers[i].Indices.Length * blendShapesAmount;
        //    }
        //    shader.SetTextureUniform("Texture[0]", TbTextureTarget.Texture2D, 0, 0);
        //    Unbind();
        //}
    }
}
