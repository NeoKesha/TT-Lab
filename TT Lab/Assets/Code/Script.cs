using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public abstract class Script : SerializableAsset
    {
        [JsonProperty(Required = Required.Always)]
        public Byte Mask { get; set; }

        public override String Type => "Script";

        public Script(UInt32 id, String name, PS2AnyScript script) : base(id, name)
        {
            Mask = script.Mask;
        }
    }
}
