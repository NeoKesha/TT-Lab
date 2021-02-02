using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.AssetData.Instance
{
    [JsonObject]
    public class ChunkLink
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 Type { get; set; }
        [JsonProperty(Required = Required.Always)]
        public String Path { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Flags { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Matrix4 ObjectMatrix { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Matrix4 ChunkMatrix { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public Matrix4? LoadingWall { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<TwinChunkLinkCollisionData> CollisionDatas { get; set; }

        public ChunkLink() { }

        public ChunkLink(TwinChunkLink link)
        {
            Type = link.Type;
            Path = link.Path[..];
            Flags = link.Flags;
            ObjectMatrix = CloneUtils.DeepClone(link.ObjectMatrix);
            ChunkMatrix = CloneUtils.DeepClone(link.ChunkMatrix);
            LoadingWall = CloneUtils.DeepClone(link.LoadingWall);
            CollisionDatas = CloneUtils.DeepClone(link.ChunkLinksCollisionData);
        }
    }
}
