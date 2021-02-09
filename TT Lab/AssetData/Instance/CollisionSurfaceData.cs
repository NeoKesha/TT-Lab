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
        public Guid StepSoundId1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Guid StepSoundId2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkId1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkId2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Guid LandSoundId1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkId3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkId4 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Guid LandSoundId2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Guid UnkSoundId { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkId5 { get; set; }
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
            StepSoundId1 = Guid.Empty;
            StepSoundId2 = Guid.Empty;
            LandSoundId1 = Guid.Empty;
            LandSoundId2 = Guid.Empty;
            UnkSoundId = Guid.Empty;
            if (collisionSurface.StepSoundId1 != 0xFFFF)
            {
                StepSoundId1 = GuidManager.GetGuidByTwinId(collisionSurface.StepSoundId1, typeof(SoundEffect));
            }
            if (collisionSurface.StepSoundId2 != 0xFFFF)
            {
                StepSoundId2 = GuidManager.GetGuidByTwinId(collisionSurface.StepSoundId1, typeof(SoundEffect));
            }
            if (collisionSurface.LandSoundId1 != 0xFFFF)
            {
                LandSoundId1 = GuidManager.GetGuidByTwinId(collisionSurface.LandSoundId1, typeof(SoundEffect));
            }
            if (collisionSurface.LandSoundId2 != 0xFFFF)
            {
                LandSoundId2 = GuidManager.GetGuidByTwinId(collisionSurface.LandSoundId2, typeof(SoundEffect));
            }
            if (collisionSurface.UnkSoundId != 0xFFFF)
            {
                UnkSoundId = GuidManager.GetGuidByTwinId(collisionSurface.UnkSoundId, typeof(SoundEffect));
            }
            UnkId1 = collisionSurface.UnkId1;
            UnkId2 = collisionSurface.UnkId2;
            UnkId3 = collisionSurface.UnkId3;
            UnkId4 = collisionSurface.UnkId4;
            UnkId5 = collisionSurface.UnkId5;
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
