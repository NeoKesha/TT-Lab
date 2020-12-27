using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.AssetData.Instance.Collision
{
    [JsonObject]
    public class CollisionTriangle
    {
        [JsonProperty(Required = Required.Always)]
        public Int32 Vector1Index { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 Vector2Index { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 Vector3Index { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 SurfaceIndex { get; set; }

        public CollisionTriangle() { }

        public CollisionTriangle(Twinsanity.TwinsanityInterchange.Common.Collision.CollisionTriangle triangle)
        {
            Vector1Index = triangle.Vector1Index;
            Vector2Index = triangle.Vector2Index;
            Vector3Index = triangle.Vector3Index;
            SurfaceIndex = triangle.SurfaceIndex;
        }
    }
}
