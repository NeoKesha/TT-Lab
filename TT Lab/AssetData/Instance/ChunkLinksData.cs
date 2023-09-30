using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.AssetData.Instance
{
    public class ChunkLinksData : AbstractAssetData
    {
        public ChunkLinksData()
        {
        }

        public ChunkLinksData(ITwinLink link) : this()
        {
            SetTwinItem(link);
        }

        [JsonProperty(Required = Required.Always)]
        public List<ChunkLink> Links { get; set; } = new List<ChunkLink>();

        protected override void Dispose(Boolean disposing)
        {
            Links.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinLink link = GetTwinItem<ITwinLink>();
            foreach (var l in link.LinksList)
            {
                Links.Add(new ChunkLink(l));
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
