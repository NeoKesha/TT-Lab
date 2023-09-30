using System;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Graphics
{
    public class BlendSkinData : AbstractAssetData
    {
        public BlendSkinData()
        {
        }

        public BlendSkinData(ITwinBlendSkin blendSkin) : this()
        {
            SetTwinItem(blendSkin);
        }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
        public override void Import(LabURI package, String? variant)
        {
            ITwinBlendSkin blendSkin = GetTwinItem<ITwinBlendSkin>();

        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
