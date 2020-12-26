using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            Meshes = CloneUtils.CloneList(skydome.Meshes);
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> Meshes;

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
