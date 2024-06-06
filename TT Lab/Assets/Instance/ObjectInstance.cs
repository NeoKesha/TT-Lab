using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels.Editors.Instance;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.Assets.Instance
{
    public class ObjectInstance : SerializableInstance
    {
        public override UInt32 Section => Constants.LAYOUT_INSTANCES_SECTION;

        public ObjectInstance(LabURI package, UInt32 id, String name, String chunk, Int32 layId, ITwinInstance instance) : base(package, id, name, chunk, layId)
        {
            assetData = new ObjectInstanceData(instance);
        }

        public ObjectInstance()
        {
        }

        public override Type GetEditorType()
        {
            return typeof(ObjectInstanceViewModel);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new ObjectInstanceData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
