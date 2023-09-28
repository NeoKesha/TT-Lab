using System;
using TT_Lab.Assets;
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
            SetTwinItem(blendSkin);
        }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
        public override void Import(LabURI package, String? variant)
        {
            PS2AnyBlendSkin blendSkin = GetTwinItem<PS2AnyBlendSkin>();

        }
    }
}
