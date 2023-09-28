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

        public static Folder CreatePS2Folder(String Name, String? variant = null)
        {
            return new Folder((LabURI)"res://PS2", Name, variant);
        }

        public static Folder CreatePS2Folder(String Name, Folder parent, String? variant = null)
        {
            return new Folder((LabURI)"res://PS2", Name, variant, parent);
        }

        public static Folder CreateXboxFolder(String Name, String? variant = null)
        {
            return new Folder((LabURI)"res://XBOX", Name, variant);
        }

        public static Folder CreateXboxFolder(String Name, Folder parent, String? variant = null)
        {
            return new Folder((LabURI)"res://XBOX", Name, variant, parent);
        }

        public Folder(LabURI package, String Name, String? variant = null) : this(package, Name, variant, null)
        {
        }

        public Folder(LabURI package, String Name, String? variant = null, Folder? parent = null) : this(package, variant, (UInt32)Guid.NewGuid().GetHashCode(), Name)
        {
            if (parent != null)
            {
                GetData().To<FolderData>().Parent = parent.URI;
                parent.AddChild(this);
            }
            else
            {
                Order = rootOrder++;
            }
        }

        protected Folder(LabURI package, String? variant, UInt32 id, String Name) : base(id, Name, package, variant)
        {
            IsLoaded = true;
            assetData = new FolderData();
        }

        public void AddChild(IAsset asset)
        {
            GetData().To<FolderData>().Children.Add(asset.URI);
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
