using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class AiPathData : AbstractAssetData
    {
        public AiPathData()
        {
        }

        public AiPathData(PS2AnyAIPath aiPath) : this()
        {
            twinRef = aiPath;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt16 PathBegin { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 PathEnd { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16[] Args { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
            PS2AnyAIPath aiPath = (PS2AnyAIPath)twinRef;
            PathBegin = aiPath.Args[0];
            PathEnd = aiPath.Args[1];
            Args = new UInt16[] { aiPath.Args[2], aiPath.Args[3], aiPath.Args[4] };
        }
    }
}
