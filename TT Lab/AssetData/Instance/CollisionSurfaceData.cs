using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class CollisionSurfaceData : AbstractAssetData
    {
        public CollisionSurfaceData()
        {
        }

        public CollisionSurfaceData(PS2AnyCollisionSurface collisionSurface) : this()
        {
            SurfaceID = collisionSurface.SurfaceId;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt16 SurfaceID { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
