using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.Assets.Graphics
{
    public class Skydome : SerializableAsset
    {
        public override UInt32 Section => Constants.GRAPHICS_SKYDOMES_SECTION;

        public Skydome(LabURI package, Boolean needVariant, String variant, UInt32 id, String Name, ITwinSkydome skydome) : base(id, Name, package, needVariant, variant)
        {
            assetData = new SkydomeData(skydome);
        }

        public Skydome()
        {
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
                assetData = new SkydomeData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
