using System;

namespace TT_Lab.Assets
{
    public class Texture : SerializableAsset
    {
        public override String Type => "Texture";

        public Texture(UInt32 id) : base(id) {}

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
