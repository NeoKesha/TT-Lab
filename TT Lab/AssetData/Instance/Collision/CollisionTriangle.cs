using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics.SubModels;

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

        public CollisionTriangle(Twinsanity.TwinsanityInterchange.Common.Collision.CollisionTriangle triangle)
        {
            Face = new IndexedFace(new int[] { triangle.Vector1Index, triangle.Vector2Index, triangle.Vector3Index });
            SurfaceIndex = triangle.SurfaceIndex;
        }
    }
}
