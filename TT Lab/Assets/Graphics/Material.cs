using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Assets.Graphics
{
    public class Material : SerializableAsset
    {
        public override String Type => "Material";

        public Material(UInt32 id, String name) : base(id, name) { }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
