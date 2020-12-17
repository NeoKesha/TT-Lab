using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class HeaderScript : Script
    {
        [JsonProperty(Required = Required.Always)]
        public List<KeyValuePair<int, uint>> Pairs { get; private set; } = new List<KeyValuePair<int, uint>>();

        public HeaderScript(UInt32 id, String name, PS2HeaderScript script) : base(id, name, script)
        {
            Pairs = script.Pairs;
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
