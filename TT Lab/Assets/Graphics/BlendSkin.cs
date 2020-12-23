using System;
using System.Windows.Controls;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class BlendSkin : SerializableAsset<BlendSkinData>
    {
        public override String Type => "BlendSkin";

        public BlendSkin() { }

        public BlendSkin(UInt32 id, String name, PS2AnyBlendSkin blendSkin) : base(id, name)
        {
            assetData = new BlendSkinData(blendSkin);
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override UserControl GetEditor()
        {
            throw new NotImplementedException();
        }
    }
}
