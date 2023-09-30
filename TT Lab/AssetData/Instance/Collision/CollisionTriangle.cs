using Newtonsoft.Json;
using System;
using TT_Lab.AssetData.Graphics.SubModels;
using Twinsanity.TwinsanityInterchange.Common.Collision;

namespace TT_Lab.AssetData.Instance.Collision
{
    [JsonObject]
    public class CollisionTriangle
    {
        [JsonProperty(Required = Required.Always)]
        public IndexedFace Face { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 SurfaceIndex { get; set; }

        public CollisionTriangle() { }

        public CollisionTriangle(TwinCollisionTriangle triangle)
        {
            Face = new IndexedFace(new int[] { triangle.Vector1Index, triangle.Vector2Index, triangle.Vector3Index });
            SurfaceIndex = triangle.SurfaceIndex;
        }
    }
}
