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
    public class MainScript : Script
    {
        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt { get; set; }

        public MainScript(UInt32 id, String name, PS2MainScript script) : base(id, name, script)
        {
            UnkInt = script.UnkInt;
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
