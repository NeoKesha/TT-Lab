using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Global;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.Assets.Global
{
    public class PSM : SerializableAsset
    {
        protected override String TwinDataExt => "psm";
        public override UInt32 Section => throw new NotImplementedException();

        public PSM() { }

        public PSM(LabURI package, Boolean needVariant, String variant, String name, ITwinPSM psm) : base((UInt32)Guid.NewGuid().GetHashCode(), name, package, needVariant, variant)
        {
            assetData = new PSMData(psm);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new PSMData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }

        public override void PreResolveResources()
        {
            base.PreResolveResources();
            var assetManager = AssetManager.Get();
            PSMData data = (PSMData)GetData();
            foreach (var ptc in data.PTCs)
            {
                assetManager.GetAsset<PTC>(ptc).PreResolveResources();
            }
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }
    }
}
