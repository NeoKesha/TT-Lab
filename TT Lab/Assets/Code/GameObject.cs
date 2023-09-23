using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class GameObject : SerializableAsset
    {
        public GameObject() { }

        public GameObject(String package, String subpackage, String? variant, UInt32 id, String name, PS2AnyObject @object) : base(id, name, package, subpackage, variant)
        {
            assetData = new GameObjectData(@object);
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

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new GameObjectData(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
