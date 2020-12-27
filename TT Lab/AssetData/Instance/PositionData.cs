using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class PositionData : AbstractAssetData
    {
        public PositionData()
        {
        }

        public PositionData(PS2AnyPosition position) : this()
        {
            Coords = CloneUtils.Clone(position.Position);
        }

        [JsonProperty(Required = Required.Always)]
        public Vector4 Coords { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
