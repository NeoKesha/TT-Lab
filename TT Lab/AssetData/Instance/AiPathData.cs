using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Args = CloneUtils.CloneArray(aiPath.Args);
        }

        [JsonProperty(Required = Required.Always)]
        public UInt16[] Args { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
