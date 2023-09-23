using Newtonsoft.Json;
using System;
using TT_Lab.Assets;
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
            SetTwinItem(collisionSurface);
        }

        [JsonProperty(Required = Required.Always)]
        public UInt16 SurfaceID { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Flags { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI StepSoundId1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI StepSoundId2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkId1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkId2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI LandSoundId1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkId3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkId4 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI LandSoundId2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI UnkSoundId { get; set; }
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

        public override void Import(String package, String subpackage, String? variant)
        {
            PS2AnyCollisionSurface collisionSurface = GetTwinItem<PS2AnyCollisionSurface>();
            SurfaceID = collisionSurface.SurfaceId;
            Flags = collisionSurface.Flags;
            StepSoundId1 = LabURI.Empty;
            StepSoundId2 = LabURI.Empty;
            LandSoundId1 = LabURI.Empty;
            LandSoundId2 = LabURI.Empty;
            UnkSoundId = LabURI.Empty;
            if (collisionSurface.StepSoundId1 != 0xFFFF)
            {
                StepSoundId1 = AssetManager.Get().GetUri(package, subpackage, typeof(SoundEffect).Name, variant, collisionSurface.StepSoundId1);
            }
            if (collisionSurface.StepSoundId2 != 0xFFFF)
            {
                StepSoundId2 = AssetManager.Get().GetUri(package, subpackage, typeof(SoundEffect).Name, variant, collisionSurface.StepSoundId2);
            }
            if (collisionSurface.LandSoundId1 != 0xFFFF)
            {
                LandSoundId1 = AssetManager.Get().GetUri(package, subpackage, typeof(SoundEffect).Name, variant, collisionSurface.LandSoundId1);
            }
            if (collisionSurface.LandSoundId2 != 0xFFFF)
            {
                LandSoundId2 = AssetManager.Get().GetUri(package, subpackage, typeof(SoundEffect).Name, variant, collisionSurface.LandSoundId2);
            }
            if (collisionSurface.UnkSoundId != 0xFFFF)
            {
                UnkSoundId = AssetManager.Get().GetUri(package, subpackage, typeof(SoundEffect).Name, variant, collisionSurface.UnkSoundId);
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
