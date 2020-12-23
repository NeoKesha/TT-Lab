using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData;

namespace TT_Lab.Assets
{
    public class Folder : SerializableAsset<FolderData>
    {
        public override String Type => "Folder";

        private static UInt32 rootOrder = 0;
        private UInt32 order = 0;

        public Folder()
        {
            IsLoaded = true;
            assetData = new FolderData();
        }

        public Folder(String name) : this(name, null)
        {
        }

        public Folder(String name, Folder parent) : this((UInt32)Guid.NewGuid().ToByteArray().Sum(b => b), name)
        {
            if (parent != null)
            {
                assetData.Parent = parent.UUID;
                parent.AddChild(this);
            }
            else
            {
                Order = rootOrder++;
            }
        }

        public void AddChild(IAsset asset)
        {
            GetData().Children.Add(asset.UUID);
            asset.Order = GetOrder();
        }

        internal UInt32 GetOrder()
        {
            return order++;
        }

        private Folder(UInt32 id, String name) : base(id, name)
        {
            IsLoaded = true;
            assetData = new FolderData();
        }

        public override void Deserialize(String json)
        {
            base.Deserialize(json);
            assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override UserControl GetEditor()
        {
            throw new NotImplementedException();
        }
    }
}
