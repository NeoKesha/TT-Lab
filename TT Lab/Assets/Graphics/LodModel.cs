using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class LodModel : SerializableAsset
    {

        public LodModel(String package, String subpackage, String? variant, UInt32 id, String name, PS2AnyLOD lod) : base(id, name, package, subpackage, variant)
        {
            assetData = new LodModelData(lod);
        }

        public LodModel()
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
                assetData = new LodModelData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
