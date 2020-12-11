using System;

namespace TT_Lab.Assets.Graphics
{
    public class RigidModel : SerializableAsset
    {
        public override String Type => "RigidModel";

        public RigidModel(UInt32 id, String name) : base(id, name) {}

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
