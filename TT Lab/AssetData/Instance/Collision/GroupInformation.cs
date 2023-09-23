using Newtonsoft.Json;
using System;

namespace TT_Lab.AssetData.Instance.Collision
{
    public class GroupInformation
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 Size { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Offset { get; set; }

        public GroupInformation() { }

        public GroupInformation(Twinsanity.TwinsanityInterchange.Common.Collision.GroupInformation group)
        {
            Size = group.Size;
            Offset = group.Offset;
        }
    }
}
