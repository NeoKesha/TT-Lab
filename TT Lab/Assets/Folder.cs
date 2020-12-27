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
    public class Folder : SerializableAsset
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
                ((FolderData)GetData()).Parent = parent.UUID;
                parent.AddChild(this);
            }
            else
            {
                Order = rootOrder++;
            }
        }

        protected Folder(UInt32 id, String name) : base(id, name)
        {
            IsLoaded = true;
            assetData = new FolderData();
        }

        public void AddChild(IAsset asset)
        {
            ((FolderData)GetData()).Children.Add(asset.UUID);
            asset.Order = GetOrder();
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

        internal UInt32 GetOrder()
        {
            return order++;
        }
        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new FolderData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
