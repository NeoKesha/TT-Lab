using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;

namespace TT_Lab.Assets.Instance
{
    public class ChunkLinks : SerializableInstance
    {
        [JsonProperty(Required = Required.Always)]
        public List<ChunkLink> Links = new List<ChunkLink>();

        public ChunkLinks()
        {
        }

        public ChunkLinks(UInt32 id, String name, String chunk, PS2AnyLink links) : base(id, name, chunk, -1)
        {
            foreach (var link in links.LinksList)
            {
                Links.Add(new ChunkLink(link));
            }
        }

        public override String Type => "ChunkLinks";

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
