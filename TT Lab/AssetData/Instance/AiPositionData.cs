using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class AiPositionData : AbstractAssetData
    {
        public AiPositionData()
        {
        }

        public AiPositionData(PS2AnyAIPosition aiPosition) : this()
        {
            twinRef = aiPosition;
        }

        [JsonProperty(Required = Required.Always)]
        public Vector4 Coords { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
            PS2AnyAIPosition aiPosition = (PS2AnyAIPosition)twinRef;
            Coords = CloneUtils.Clone(aiPosition.Position);
        }
    }
}
