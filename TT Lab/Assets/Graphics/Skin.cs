using System;

namespace TT_Lab.Assets.Graphics
{
    public class Skin : SerializableAsset
    {
        public override String Type => "Skin";

        public Skin(UInt32 id, String name) : base(id, name) {}

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
