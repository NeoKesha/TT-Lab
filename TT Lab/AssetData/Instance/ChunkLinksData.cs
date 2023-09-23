using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;

namespace TT_Lab.AssetData.Instance
{
    public class ChunkLinksData : AbstractAssetData
    {
        public ChunkLinksData()
        {
        }

        public ChunkLinksData(PS2AnyLink link) : this()
        {
            SetTwinItem(link);
        }

        [JsonProperty(Required = Required.Always)]
        public List<ChunkLink> Links { get; set; } = new List<ChunkLink>();

        protected override void Dispose(Boolean disposing)
        {
            Links.Clear();
        }

        public override void Import(String package, String subpackage, String? variant)
        {
            PS2AnyLink link = GetTwinItem<PS2AnyLink>();
            foreach (var l in link.LinksList)
            {
                Links.Add(new ChunkLink(l));
            }
        }
    }
}
