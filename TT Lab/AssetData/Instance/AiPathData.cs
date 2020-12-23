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
            Args = (UInt16[])aiPath.Args.Clone();
        }

        [JsonProperty(Required = Required.Always)]
        public UInt16[] Args { get; } = new UInt16[5];

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
