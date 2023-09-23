using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class AnimationData : AbstractAssetData
    {
        public AnimationData()
        {
        }

        public AnimationData(PS2AnyAnimation animation) : this()
        {
            SetTwinItem(animation);
        }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(String package, String subpackage, String? variant)
        {
        }
    }
}
