using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.Assets.Graphics
{
    public class LodModel : SerializableAsset
    {
        public override UInt32 Section => Constants.GRAPHICS_LODS_SECTION;
        public override String IconPath => "LOD.png";

        public LodModel(LabURI package, Boolean needVariant, String variant, UInt32 id, String Name, ITwinLOD lod) : base(id, Name, package, needVariant, variant)
        {
            assetData = new LodModelData(lod);
        }

        public LodModel()
        {
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new LodModelData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
