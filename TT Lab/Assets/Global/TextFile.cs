using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Global;
using TT_Lab.AssetData.Graphics;

namespace TT_Lab.Assets.Global
{
    public class TextFile : SerializableAsset
    {
        protected override String DataExt => ".txt";

        public TextFile() { }

        public TextFile(LabURI package, String? variant, String name, String data) : base((UInt32)Guid.NewGuid().GetHashCode(), name, package, variant)
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
