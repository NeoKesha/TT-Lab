using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.Editors.Instance;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class ObjectInstance : SerializableInstance
    {
        public ObjectInstance(String package, String subpackage, UInt32 id, String name, String chunk, Int32 layId, PS2AnyInstance instance) : base(package, subpackage, id, name, chunk, layId)
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
