using Newtonsoft.Json;
using System;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class AiPositionData : AbstractAssetData
    {
        public AiPositionData()
        {
        }

        public AiPositionData(ITwinAIPosition aiPosition) : this()
        {
            SetTwinItem(aiPosition);
        }

        [JsonProperty(Required = Required.Always)]
        public Vector4 Coords { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 Arg { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinAIPosition aiPosition = GetTwinItem<ITwinAIPosition>();
            Coords = CloneUtils.Clone(aiPosition.Position);
            Arg = aiPosition.UnkShort;
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            Coords.Write(writer);
            writer.Write(Arg);

            ms.Position = 0;
            return factory.GenerateAIPosition(ms);
        }
    }
}
