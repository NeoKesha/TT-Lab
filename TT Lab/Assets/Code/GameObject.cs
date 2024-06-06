using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class GameObject : SerializableAsset
    {
        public override UInt32 Section => Constants.CODE_GAME_OBJECTS_SECTION;

        public GameObject() { }

        public GameObject(LabURI package, Boolean needVariant, String variant, UInt32 id, String Name, ITwinObject @object) : base(id, Name, package, needVariant, variant)
        {
            assetData = new GameObjectData(@object);
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new GameObjectData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
