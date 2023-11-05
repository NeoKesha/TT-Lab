using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.AssetData.Instance
{
    [JsonObject]
    public class ChunkLink
    {
        [JsonProperty(Required = Required.Always)]
        public Boolean UnkFlag { get; set; }
        [JsonProperty(Required = Required.Always)]
        public String Path { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Boolean IsRendered { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkNum { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Boolean IsLoadWallActive { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Boolean KeepLoaded { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Matrix4 ObjectMatrix { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Matrix4 ChunkMatrix { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public Matrix4? LoadingWall { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<TwinChunkLinkBoundingBoxBuilder> ChunkLinksCollisionData { get; set; }

        public ChunkLink()
        {
            Path = "levels\\earth\\hub\\beach";
            IsRendered = true;
            IsLoadWallActive = true;
            ObjectMatrix = new Matrix4();
            ChunkMatrix = new Matrix4();
            ChunkLinksCollisionData = new List<TwinChunkLinkBoundingBoxBuilder>();
        }

        public ChunkLink(TwinChunkLink link)
        {
            UnkFlag = link.UnkFlag;
            Path = link.Path[..];
            IsRendered = link.IsRendered;
            UnkNum = link.UnkNum;
            IsLoadWallActive = link.IsLoadWallActive;
            KeepLoaded = link.KeepLoaded;
            ObjectMatrix = CloneUtils.DeepClone(link.ObjectMatrix);
            ChunkMatrix = CloneUtils.DeepClone(link.ChunkMatrix);
            LoadingWall = CloneUtils.DeepClone(link.LoadingWall);
            ChunkLinksCollisionData = CloneUtils.DeepClone(link.ChunkLinksCollisionData);
        }
    }
}
