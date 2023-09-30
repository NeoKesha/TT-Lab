using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Common.Collision;

namespace TT_Lab.AssetData.Instance.Collision
{
    public class GroupInformation
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 Size { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Offset { get; set; }

        public GroupInformation() { }

        public GroupInformation(TwinGroupInformation group)
        {
            Size = group.Size;
            Offset = group.Offset;
        }
    }
}
