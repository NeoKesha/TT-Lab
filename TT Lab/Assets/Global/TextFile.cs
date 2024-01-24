using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Global;
using TT_Lab.Assets.Factory;

namespace TT_Lab.Assets.Global
{
    public class TextFile : SerializableAsset
    {
        protected override String DataExt => ".txt";
        protected override String TwinDataExt => "txt";
        public override UInt32 Section => throw new NotImplementedException();

        public TextFile() { }

        public TextFile(LabURI package, Boolean needVariant, String variant, String name, String data) : base((UInt32)Guid.NewGuid().GetHashCode(), name, package, needVariant, variant)
        {
            assetData = new TextFileData(data);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new TextFileData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }

        public override void ExportToFile(ITwinItemFactory factory)
        {
            GetData().Save($"{Name}.{TwinDataExt}");
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
