using Newtonsoft.Json;
using System;
using TT_Lab.Assets;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class PositionData : AbstractAssetData
    {
        public PositionData()
        {

        }

        public PositionData(PS2AnyPosition position)
        {
            SetTwinItem(position);
        }

        [JsonProperty(Required = Required.Always)]
        public Vector4 Coords;

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(LabURI package, String? variant)
        {
            PS2AnyPosition position = GetTwinItem<PS2AnyPosition>();
            Coords = CloneUtils.Clone(position.Position);
        }
    }
}
