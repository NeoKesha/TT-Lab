using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class ObjectInstance : SerializableInstance<ObjectInstanceData>
    {
        public ObjectInstance(UInt32 id, String name, String chunk, Int32 layId, PS2AnyInstance instance) : base(id, name, chunk, layId)
        {
            assetData = new ObjectInstanceData(instance);
        }

        public ObjectInstance()
        {
        }

        public override String Type => "ObjectInstance";

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
