using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public abstract class ScriptData : AbstractAssetData
    {
        protected ScriptData()
        {
        }

        protected ScriptData(PS2AnyScript script) : this()
        {
            Mask = script.Mask;
        }

        [JsonProperty(Required = Required.Always)]
        public Byte Mask { get; set; }
    }
}
