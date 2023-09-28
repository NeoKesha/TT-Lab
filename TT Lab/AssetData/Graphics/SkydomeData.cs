using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
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
            SetTwinItem(skydome);
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> Meshes { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(LabURI package, String? variant)
        {
            PS2AnySkydome skydome = GetTwinItem<PS2AnySkydome>();
            Header = skydome.Header;
            Meshes = new List<LabURI>();
            foreach (var mesh in skydome.Meshes)
            {
                Meshes.Add(AssetManager.Get().GetUri(package, typeof(Mesh).Name, variant, mesh));
            }
        }
    }
}
