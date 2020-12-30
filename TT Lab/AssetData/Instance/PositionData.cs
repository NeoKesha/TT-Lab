using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class PositionData : AbstractAssetData
    {
        private Vector4 coords = new Vector4();

        public PositionData()
        {
            
        }

        public PositionData(PS2AnyPosition position)
        {
            twinRef = position;
        }

        [JsonProperty(Required = Required.Always)]
        public Vector4 Coords {
            get => coords;
            set
            {
                if (coords.X != value.X || coords.Y != value.Y || coords.Z != value.Z || coords.W != value.W)
                {
                    coords = value;
                    NotifyChange("Coords");
                }
            }
        }



        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
            PS2AnyPosition position = (PS2AnyPosition)twinRef;
            coords = CloneUtils.Clone(position.Position);
        }
    }
}
