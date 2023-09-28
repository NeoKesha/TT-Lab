using System;
using TT_Lab.Assets;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.AssetData.Code
{
    public class AnimationData : AbstractAssetData
    {
        public AnimationData()
        {
        }

        public AnimationData(ITwinAnimation animation) : this()
        {
            SetTwinItem(animation);
        }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(LabURI package, String? variant)
        {
        }
    }
}
