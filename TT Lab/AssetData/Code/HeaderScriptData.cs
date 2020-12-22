using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class HeaderScriptData : ScriptData
    {
        public HeaderScriptData()
        {
        }

        public HeaderScriptData(PS2HeaderScript headerScript) : base(headerScript)
        {
        }

        [JsonProperty(Required = Required.Always)]
        public List<KeyValuePair<int, uint>> Pairs { get; private set; } = new List<KeyValuePair<int, uint>>();

        protected override void Dispose(Boolean disposing)
        {
            Pairs.Clear();
        }
    }
}
