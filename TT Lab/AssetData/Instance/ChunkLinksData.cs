using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            foreach (var l in link.LinksList)
            {
                Links.Add(new ChunkLink(l));
            }
        }

        [JsonProperty(Required = Required.Always)]
        public List<ChunkLink> Links { get; set; } = new List<ChunkLink>();

        protected override void Dispose(Boolean disposing)
        {
            Links.Clear();
        }
    }
}
