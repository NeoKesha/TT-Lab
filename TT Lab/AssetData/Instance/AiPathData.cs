using Newtonsoft.Json;
using System;
using TT_Lab.Assets;
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
            SetTwinItem(aiPath);
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

        public override void Import(LabURI package, String? variant)
        {
            PS2AnyAIPath aiPath = GetTwinItem<PS2AnyAIPath>();
            PathBegin = aiPath.Args[0];
            PathEnd = aiPath.Args[1];
            Args = new UInt16[] { aiPath.Args[2], aiPath.Args[3], aiPath.Args[4] };
        }
    }
}
