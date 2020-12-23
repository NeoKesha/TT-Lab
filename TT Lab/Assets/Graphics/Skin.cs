using System;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class Skin : SerializableAsset<SkinData>
    {
        public override String Type => "Skin";

        public Skin(UInt32 id, String name, PS2AnySkin skin) : base(id, name)
        {
            assetData = new SkinData(skin);
        }

        public Skin()
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
