using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets.Code;
using TT_Lab.Util;
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
            twinRef = headerScript;
        }

        [JsonProperty(Required = Required.Always)]
        public List<KeyValuePair<Guid, UInt32>> Pairs { get; set; } = new List<KeyValuePair<Guid, UInt32>>();

        protected override void Dispose(Boolean disposing)
        {
            Pairs.Clear();
        }

        public override void Import()
        {
            PS2HeaderScript headerScript = (PS2HeaderScript)twinRef;
            foreach (var pair in headerScript.Pairs)
            {
                if (pair.Key - 1 == -1)
                {
                    Pairs.Add(new KeyValuePair<Guid, uint>(Guid.Empty, pair.Value));
                }
                else
                {
                    Pairs.Add(new KeyValuePair<Guid, uint>(GuidManager.GetGuidByTwinId((UInt32)pair.Key - 1, typeof(MainScript)), pair.Value));
                }
            }
        }
    }
}
