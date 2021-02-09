using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.AssetData.Graphics
{
    public class BlendSkinData : AbstractAssetData
    {
        public BlendSkinData()
        {
        }

        public BlendSkinData(PS2AnyBlendSkin blendSkin) : this()
        {
            twinRef = blendSkin;
        }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
        public override void Import()
        {
            PS2AnyBlendSkin blendSkin = (PS2AnyBlendSkin)twinRef;

        }
    }
}
