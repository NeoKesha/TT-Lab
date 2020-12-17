using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Assets.Instance
{
    public class InstanceTemplate : SerializableInstance
    {
        public InstanceTemplate(UInt32 id, String name, String chunk, Int32 layId) : base(id, name, chunk, layId)
        {
        }

        public override String Type => base.Type + "InstanceTemplate";

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
