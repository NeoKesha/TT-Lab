using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
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
            twinRef = skydome;
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> Meshes { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
            PS2AnySkydome skydome = (PS2AnySkydome)twinRef;
            Header = skydome.Header;
            Meshes = new List<Guid>();
            foreach (var mesh in skydome.Meshes)
            {
                Meshes.Add(GuidManager.GetGuidByTwinId(mesh, typeof(Mesh)));
            }
        }
    }
}
