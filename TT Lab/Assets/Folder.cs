using System;
using System.Linq;
using TT_Lab.AssetData;

namespace TT_Lab.Assets
{
    public class Folder : SerializableAsset
    {

        private static UInt32 rootOrder = 0;
        private UInt32 order = 0;

        public Folder()
        {
            IsLoaded = true;
            assetData = new FolderData();
        }

        public static Folder CreatePS2Folder(String name, String? variant = null)
        {
            return new Folder("base", "PS2", name, variant);
        }

        public static Folder CreatePS2Folder(String name, Folder parent, String? variant = null)
        {
            return new Folder("base", "PS2", name, variant, parent);
        }

        public static Folder CreateXboxFolder(String name, String? variant = null)
        {
            return new Folder("base", "XBox", name, variant);
        }

        public static Folder CreateXboxFolder(String name, Folder parent, String? variant = null)
        {
            return new Folder("base", "XBox", name, variant, parent);
        }

        public Folder(String package, String subpackage, String name, String? variant = null) : this(package, subpackage, name, variant, null)
        {
        }

        public Folder(String package, String subpackage, String name, String? variant = null, Folder? parent = null) : this(package, subpackage, variant, (UInt32)Guid.NewGuid().ToByteArray().Sum(b => b), name)
        {
            if (parent != null)
            {
                ((FolderData)GetData()).Parent = parent.URI;
                parent.AddChild(this);
            }
            else
            {
                Order = rootOrder++;
            }
        }

        protected Folder(String package, String subpackage, String? variant, UInt32 id, String name) : base(id, name, package, subpackage, variant)
        {
            IsLoaded = true;
            assetData = new FolderData();
        }

        public void AddChild(IAsset asset)
        {
            ((FolderData)GetData()).Children.Add(asset.URI);
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

        public override Type GetEditorType()
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
