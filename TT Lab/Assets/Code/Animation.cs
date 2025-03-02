using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class Animation : SerializableAsset
    {
        public override UInt32 Section => Constants.CODE_ANIMATIONS_SECTION;
        public override String IconPath => "Animation.png";

        public Animation() { }

        public Animation(LabURI package, Boolean needVariant, String variant, UInt32 id, String name, ITwinAnimation? animation = null) : base(id, name, package, needVariant, variant)
        {
            if (animation == null)
            {
                return;
            }
            
            assetData = new AnimationData(animation);
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new AnimationData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
