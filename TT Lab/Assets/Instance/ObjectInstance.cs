using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets.Factory;
using TT_Lab.Editors.Instance;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;
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
            return typeof(ObjectInstanceEditor);
        }

        public override AssetViewModel GetViewModel(AssetViewModel? parent = null)
        {
            viewModel ??= new ObjectInstanceViewModel(URI, parent);
            return viewModel;
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
