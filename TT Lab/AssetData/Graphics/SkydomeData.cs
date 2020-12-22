using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.AssetData.Graphics
{
    public class SkydomeData : AbstractAssetData
    {
        public SkydomeData()
        {
        }

        public SkydomeData(PS2AnySkydome skydome) : this()
        {
            Header = skydome.Header;
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 Header { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
