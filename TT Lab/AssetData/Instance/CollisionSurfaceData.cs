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
            Flags = collisionSurface.Flags;
            StepSoundIds = (UInt16[])collisionSurface.StepSoundIds.Clone();
            Parameters = (Single[])collisionSurface.Parameters.Clone();
            UnkShorts = (UInt16[])collisionSurface.UnkShorts.Clone();
        }

        [JsonProperty(Required = Required.Always)]
        public UInt16 SurfaceID { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Flags { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16[] StepSoundIds { get; private set; }
        [JsonProperty(Required = Required.Always)]
        public Single[] Parameters { get; private set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16[] UnkShorts { get; private set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
