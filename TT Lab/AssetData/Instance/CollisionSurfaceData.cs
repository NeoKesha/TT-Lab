using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets.Code;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
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
            twinRef = collisionSurface;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt16 SurfaceID { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Flags { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Guid[] StepSoundIds { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single[] Parameters { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 UnkVec { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4[] UnkBoundingBox { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
            PS2AnyCollisionSurface collisionSurface = (PS2AnyCollisionSurface)twinRef;
            SurfaceID = collisionSurface.SurfaceId;
            Flags = collisionSurface.Flags;
            StepSoundIds = new Guid[collisionSurface.StepSoundIds.Length];
            for (var i = 0; i < collisionSurface.StepSoundIds.Length; ++i)
            {
                if (collisionSurface.StepSoundIds[i] == 65535)
                {
                    StepSoundIds[i] = Guid.Empty;
                    continue;
                }
                StepSoundIds[i] = GuidManager.GetGuidByTwinId(collisionSurface.StepSoundIds[i], typeof(SoundEffect));
            }
            Parameters = CloneUtils.CloneArray(collisionSurface.UnkFloatParams);
            UnkVec = CloneUtils.Clone(collisionSurface.UnkVec);
            UnkBoundingBox = new Vector4[2];
            for (var i = 0; i < UnkBoundingBox.Length; ++i)
            {
                UnkBoundingBox[i] = CloneUtils.Clone(collisionSurface.UnkBoundingBox[i]);
            }
        }
    }
}
