using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData;

namespace TT_Lab.Assets
{
    public class Folder : SerializableAsset<FolderData>
    {
        public override String Type => "Folder";

        public Folder()
        {
            IsLoaded = true;
            assetData = new FolderData();
        }

        public Folder(String name) : this(name, null)
        {
            IsLoaded = true;
            assetData = new FolderData();
        }

        public Folder(String name, Folder parent) : this((UInt32)Guid.NewGuid().ToByteArray().Sum(b => b), name)
        {
            if (parent != null)
            {
                assetData.Parent = parent.UUID;
                parent.GetData().Children.Add(UUID);
            }
        }

        private Folder(UInt32 id, String name) : base(id, name)
        {
        }

        public override void Deserialize(String json)
        {
            base.Deserialize(json);
            assetData.Load(Data);
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
