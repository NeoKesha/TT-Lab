using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class CameraData : AbstractAssetData
    {
        public CameraData()
        {
        }

        public CameraData(PS2AnyCamera camera) : this()
        {
            Enabled = camera.CamTrigger.Enabled > 0;
            Position = camera.CamTrigger.Position;
        }

        [JsonProperty(Required = Required.Always)]
        public Boolean Enabled { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Position { get; set; } = new Vector4();

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
