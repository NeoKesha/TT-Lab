using org.ogre;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.Util;
using System.Diagnostics;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Rendering
{
    public static class MaterialManager
    {
        private static org.ogre.MaterialManager instance;

        public static void Initialize()
        {
            instance = org.ogre.MaterialManager.getSingleton();
        }

        public static MaterialPtr GetDefault()
        {
            return instance.getByName("BoatGuy");
        }

        /// <summary>
        /// Creates new material based on a preloaded one or returns already existing material instance
        /// </summary>
        /// <param name="name"></param>
        /// <param name="material"></param>
        /// <param name="baseOnMaterial"></param>
        /// <returns>Whether existing material was found or not</returns>
        public static bool CreateOrGetMaterial(string name, out MaterialPtr material, string baseOnMaterial = "DiffuseTexture")
        {
            var formattedName = name.Replace(" ", "_") + baseOnMaterial;
            if (instance.resourceExists(formattedName, GlobalConsts.OgreGroup))
            {
                material = instance.getByName(formattedName);
                return false;
            }

            material = instance.getByName(baseOnMaterial).clone(formattedName, GlobalConsts.OgreGroup);
            ShaderGenerator.getSingleton().invalidateMaterial(Ogre.MSN_DEFAULT, material.__deref__());
            return true;
        }

        public static MaterialPtr GetMaterial(string name)
        {
            return instance.getByName(name);
        }

        public static void SetupMaterial(MaterialPtr material, AssetData.Graphics.Shaders.LabShader shader)
        {
            Debug.Assert(shader.TxtMapping == TwinShader.TextureMapping.ON, "Shader must have a texture mapped");
            
            var technique = material.getTechnique(0);
            var textureName = AssetManager.Get().GetAsset<Assets.Graphics.Texture>(shader.TextureId).Name.Replace(" ", "_");

            var textureManager = TextureManager.getSingleton();
            if (!textureManager.resourceExists(textureName, GlobalConsts.OgreGroup))
            {
                using var textureData = AssetManager.Get().GetAssetData<AssetData.Graphics.TextureData>(shader.TextureId);
                var texturePtr = textureData.Bitmap.LockBits(new Rectangle(0, 0, textureData.Bitmap.Width, textureData.Bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                org.ogre.Image img = new();
                img = img.loadDynamicImage(new SWIGTYPE_p_unsigned_char(texturePtr.Scan0, false), (uint)texturePtr.Width, (uint)texturePtr.Height, 1, org.ogre.PixelFormat.PF_A8R8G8B8, false, 1, 0);

                var texture = textureManager.create(textureName, GlobalConsts.OgreGroup);
                texture.setTextureType(TextureType.TEX_TYPE_2D);
                texture.loadImage(img);

                textureData.Bitmap.UnlockBits(texturePtr);

                texture.load();
                img.Dispose();
            }
            var texturePass = technique.getPass(0);
            // if (shader.ABlending == Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaBlending.ON)
            // {
            //     SetupAlphaRendering(texturePass, shader);
            // }
            //
            // SetupDepthTesting(texturePass, shader);
            //
            // if (shader.DAlphaTest == Twinsanity.TwinsanityInterchange.Common.TwinShader.DestinationAlphaTest.ON)
            // {
            //     SetupDestinationAlphaTesting(texturePass, shader);
            // }
            //
            // // TODO: Figure it out
            // switch (shader.ProcessMethodWhenAlphaTestFailed)
            // {
            //     case Twinsanity.TwinsanityInterchange.Common.TwinShader.ProcessAfterAlphaTestFailed.KEEP:
            //         break;
            //     case Twinsanity.TwinsanityInterchange.Common.TwinShader.ProcessAfterAlphaTestFailed.FB_ONLY:
            //         break;
            //     case Twinsanity.TwinsanityInterchange.Common.TwinShader.ProcessAfterAlphaTestFailed.ZB_ONLY:
            //         break;
            //     case Twinsanity.TwinsanityInterchange.Common.TwinShader.ProcessAfterAlphaTestFailed.RGB_ONLY:
            //         break;
            // }
            //
            // switch (shader.ShdMethod)
            // {
            //     case Twinsanity.TwinsanityInterchange.Common.TwinShader.ShadingMethod.FLAT:
            //         texturePass.setShadingMode(ShadeOptions.SO_FLAT);
            //         break;
            //     case Twinsanity.TwinsanityInterchange.Common.TwinShader.ShadingMethod.GOURAND:
            //         texturePass.setShadingMode(ShadeOptions.SO_GOURAUD);
            //         break;
            //     default:
            //         break;
            // }

            var textureUnit = texturePass.getTextureUnitState(0);
            textureUnit.setTextureName(textureName);
            material.compile();
            
            ShaderGenerator.getSingleton().invalidateMaterial(Ogre.MSN_DEFAULT, material.__deref__());
            
            material.compile();
        }

        public static void SetupMaterialPlainTexture(MaterialPtr material, LabURI textureUri, uint passIdx = 0)
        {
            if (textureUri == LabURI.Empty)
            {
                return;
            }
            
            var technique = material.getTechnique(0);
            var textureName = AssetManager.Get().GetAsset<Assets.Graphics.Texture>(textureUri).Name.Replace(" ", "_");
            var textureManager = TextureManager.getSingleton();
            if (!textureManager.resourceExists(textureName, GlobalConsts.OgreGroup))
            {
                using var textureData = AssetManager.Get().GetAssetData<AssetData.Graphics.TextureData>(textureUri);
                var texturePtr = textureData.Bitmap.LockBits(new Rectangle(0, 0, textureData.Bitmap.Width, textureData.Bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                org.ogre.Image img = new();
                img = img.loadDynamicImage(new SWIGTYPE_p_unsigned_char(texturePtr.Scan0, false), (uint)texturePtr.Width, (uint)texturePtr.Height, 1, org.ogre.PixelFormat.PF_A8R8G8B8, false, 1, 0);

                var texture = textureManager.create(textureName, GlobalConsts.OgreGroup);
                texture.setTextureType(TextureType.TEX_TYPE_2D);
                texture.setNumMipmaps(0);
                texture.loadImage(img);

                textureData.Bitmap.UnlockBits(texturePtr);

                texture.load();
                img.Dispose();
            }

            var texturePass = technique.getPass(passIdx);
            var textureUnit = texturePass.getTextureUnitState(0);
            textureUnit.setTextureName(textureName);
            material.compile();
            
            ShaderGenerator.getSingleton().invalidateMaterial(material.getName(), material.__deref__());
            
            material.compile();
        }

        private static void SetupAlphaRendering(Pass pass, AssetData.Graphics.Shaders.LabShader shader)
        {
            if (shader.ATest == Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaTest.ON)
            {
                switch (shader.ATestMethod)
                {
                    case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaTestMethod.NEVER:
                        pass.setAlphaRejectFunction(CompareFunction.CMPF_ALWAYS_FAIL);
                        break;
                    case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaTestMethod.ALWAYS:
                        pass.setAlphaRejectFunction(CompareFunction.CMPF_ALWAYS_PASS);
                        break;
                    case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaTestMethod.LESS:
                        pass.setAlphaRejectFunction(CompareFunction.CMPF_LESS);
                        break;
                    case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaTestMethod.LEQUAL:
                        pass.setAlphaRejectFunction(CompareFunction.CMPF_LESS_EQUAL);
                        break;
                    case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaTestMethod.EQUAL:
                        pass.setAlphaRejectFunction(CompareFunction.CMPF_EQUAL);
                        break;
                    case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaTestMethod.GEQUAL:
                        pass.setAlphaRejectFunction(CompareFunction.CMPF_GREATER_EQUAL);
                        break;
                    case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaTestMethod.GREATER:
                        pass.setAlphaRejectFunction(CompareFunction.CMPF_GREATER);
                        break;
                    case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaTestMethod.NOTEQUAL:
                        pass.setAlphaRejectFunction(CompareFunction.CMPF_NOT_EQUAL);
                        break;
                    default:
                        Debug.Assert(false, "UNSUPPORTED ALPHA TEST METHOD");
                        break;
                }
                pass.setAlphaRejectValue(shader.AlphaValueToBeComparedTo);
            }
            
            switch (shader.AlphaRegSettingsIndex)
            {
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaBlendPresets.Mix:
                    pass.setSceneBlending(SceneBlendFactor.SBF_ONE, SceneBlendFactor.SBF_ONE_MINUS_SOURCE_ALPHA);
                    break;
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaBlendPresets.Add:
                    pass.setSceneBlending(SceneBlendType.SBT_ADD);
                    break;
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaBlendPresets.Sub:
                    pass.setSceneBlending(SceneBlendFactor.SBF_SOURCE_ALPHA, SceneBlendFactor.SBF_ONE);
                    break;
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaBlendPresets.Alpha:
                    pass.setSceneBlending(SceneBlendType.SBT_TRANSPARENT_ALPHA);
                    break;
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaBlendPresets.Zero:
                    pass.setSceneBlending(SceneBlendFactor.SBF_ZERO, SceneBlendFactor.SBF_ZERO);
                    break;
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaBlendPresets.Destination:
                    pass.setSceneBlending(SceneBlendFactor.SBF_DEST_COLOUR, SceneBlendFactor.SBF_ZERO);
                    break;
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.AlphaBlendPresets.Source:
                    pass.setSceneBlending(SceneBlendFactor.SBF_SOURCE_COLOUR, SceneBlendFactor.SBF_ZERO);
                    break;
                default:
                    pass.setSceneBlending(SceneBlendType.SBT_ADD);
                    break;
            }
        }

        private static void SetupDepthTesting(Pass pass, AssetData.Graphics.Shaders.LabShader shader)
        {
            switch (shader.DepthTest)
            {
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.DepthTestMethod.NEVER:
                    pass.setDepthFunction(CompareFunction.CMPF_ALWAYS_FAIL);
                    break;
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.DepthTestMethod.ALWAYS:
                    pass.setDepthFunction(CompareFunction.CMPF_ALWAYS_PASS);
                    break;
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.DepthTestMethod.GEQUAL:
                    pass.setDepthFunction(CompareFunction.CMPF_LESS_EQUAL);
                    break;
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.DepthTestMethod.GREATER:
                    pass.setDepthFunction(CompareFunction.CMPF_LESS);
                    break;
            }
        }

        private static void SetupDestinationAlphaTesting(Pass pass, AssetData.Graphics.Shaders.LabShader shader)
        {
            // TODO: Figure it out
            switch (shader.DAlphaTestMode)
            {
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.DestinationAlphaTestMode.Alpha0Pass:
                    break;
                case Twinsanity.TwinsanityInterchange.Common.TwinShader.DestinationAlphaTestMode.Alpha1Pass:
                    break;
            }
        }
    }
}
