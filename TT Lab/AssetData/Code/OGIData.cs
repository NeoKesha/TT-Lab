using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class OGIData : AbstractAssetData
    {
        public OGIData()
        {
        }

        public OGIData(PS2AnyOGI ogi) : this()
        {
            BoundingBox = ogi.BoundingBox;
        }

        [JsonProperty(Required = Required.Always)]
        public Vector4[] BoundingBox { get; set; } = new Vector4[2];

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
