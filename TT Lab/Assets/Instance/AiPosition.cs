using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class AiPosition : SerializableInstance
    {

        [JsonProperty(Required = Required.Always)]
        public Vector4 Coords { get; set; } = new Vector4();

        public AiPosition(UInt32 id, String name, String chunk, Int32 layId, PS2AnyAIPosition position) : base(id, name, chunk, layId)
        {
            Coords = position.Position;
        }

        public override String Type => base.Type + "AiPosition";

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
