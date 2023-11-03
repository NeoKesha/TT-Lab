using Newtonsoft.Json;
using System;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class AiPathData : AbstractAssetData
    {
        public AiPathData()
        {
        }

        public AiPathData(ITwinAIPath aiPath) : this()
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
            ITwinAIPath aiPath = GetTwinItem<ITwinAIPath>();
            PathBegin = aiPath.Args[0];
            PathEnd = aiPath.Args[1];
            Args = new UInt16[] { aiPath.Args[2], aiPath.Args[3], aiPath.Args[4] };
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(PathBegin);
            writer.Write(PathEnd);
            foreach (var arg in Args)
            {
                writer.Write(arg);
            }

            ms.Position = 0;
            return factory.GenerateAIPath(ms);
        }
    }
}
