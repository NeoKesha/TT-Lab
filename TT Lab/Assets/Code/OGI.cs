using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class OGI : SerializableAsset
    {
        public override UInt32 Section => Constants.CODE_OGIS_SECTION;

        public OGI() { }

        public OGI(LabURI package, String? variant, UInt32 id, String Name, ITwinOGI ogi) : base(id, Name, package, variant)
        {
            assetData = new OGIData(ogi);
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
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
                assetData = new OGIData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
