using System;
using System.Collections.Generic;
using TT_Lab.AssetData;

namespace TT_Lab.Assets
{
    public class Package : SerializableAsset
    {
        public List<LabURI> Dependencies { get; private set; } = new();

        public override AbstractAssetData GetData()
        {
            throw new NotImplementedException();
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
