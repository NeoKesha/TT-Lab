using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Assets.Code
{
    public class CodeModel : SerializableAsset
    {
        public override String Type => "CodeModel";

        public CodeModel(UInt32 id, String name) : base(id, name)
        {
        }

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
