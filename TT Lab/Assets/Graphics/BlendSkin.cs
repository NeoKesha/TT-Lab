using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class BlendSkin : SerializableAsset
    {
        public override String Type => "BlendSkin";

        public BlendSkin(UInt32 id, String name, PS2AnyBlendSkin blendSkin) : base(id, name)
        {
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }
    }
}
