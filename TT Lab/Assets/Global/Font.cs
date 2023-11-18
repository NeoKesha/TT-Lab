using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Global;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.Assets.Global
{
    public class Font : SerializableAsset
    {
        protected override String TwinDataExt => "psf";

        public Font() { }

        public Font(LabURI package, String? variant, String name, ITwinPSF psf) : base((UInt32)Guid.NewGuid().GetHashCode(), name, package, variant)
        {
            assetData = new FontData(psf);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new FontData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
