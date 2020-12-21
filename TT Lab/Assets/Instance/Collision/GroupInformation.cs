using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Assets.Instance.Collision
{
    public class GroupInformation
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 Size;
        [JsonProperty(Required = Required.Always)]
        public UInt32 Offset;

        public GroupInformation() { }

        public GroupInformation(Twinsanity.TwinsanityInterchange.Common.Collision.GroupInformation group)
        {
            Size = group.Size;
            Offset = group.Offset;
        }
    }
}
