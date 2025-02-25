using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Graphics;
using TT_Lab.ViewModels.Editors.Graphics;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.Assets.Graphics
{
    public class RigidModel : SerializableAsset
    {
        public override UInt32 Section => Constants.GRAPHICS_RIGID_MODELS_SECTION;
        public override String IconPath => "Mesh.png";

        public RigidModel(LabURI package, Boolean needVariant, String variant, UInt32 id, String Name, ITwinRigidModel rigidModel) : base(id, Name, package, needVariant, variant)
        {
            assetData = new RigidModelData(rigidModel);
        }

        public RigidModel()
        {
        }

        public override Type GetEditorType()
        {
            return typeof(RigidModelViewModel);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new RigidModelData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
