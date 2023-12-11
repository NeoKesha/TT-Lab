using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.Assets.Graphics
{
    public class BlendSkin : SerializableAsset
    {
        protected override String DataExt => ".glb";
        public override UInt32 Section => Constants.GRAPHICS_BLEND_SKINS_SECTION;

        public BlendSkin() { }

        public BlendSkin(LabURI package, String? variant, UInt32 id, String Name, ITwinBlendSkin blendSkin) : base(id, Name, package, variant)
        {
            assetData = new BlendSkinData(blendSkin);
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new BlendSkinData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
