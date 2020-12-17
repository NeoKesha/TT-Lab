using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class AiPath : SerializableInstance
    {

        [JsonProperty(Required = Required.Always)]
        public UInt16[] Args { get; } = new UInt16[5];

        public AiPath(UInt32 id, String name, String chunk, Int32 layId, PS2AnyAIPath path) : base(id, name, chunk, layId)
        {
            Args = path.Args;
        }

        public override String Type => base.Type + "AiPath";

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
