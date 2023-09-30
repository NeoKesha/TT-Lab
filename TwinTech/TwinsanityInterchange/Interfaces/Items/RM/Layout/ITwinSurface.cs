using System;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout
{
    public interface ITwinSurface : ITwinItem
    {
        UInt32 Flags { get; set; }
        UInt16 SurfaceId { get; set; }
        UInt16 StepSoundId1 { get; set; }
        UInt16 StepSoundId2 { get; set; }
        UInt16 UnkId1 { get; set; }
        UInt16 UnkId2 { get; set; }
        UInt16 LandSoundId1 { get; set; }
        UInt16 UnkId3 { get; set; }
        UInt16 UnkId4 { get; set; }
        UInt16 LandSoundId2 { get; set; }
        UInt16 UnkSoundId { get; set; }
        UInt16 UnkId5 { get; set; }
        Single[] UnkFloatParams { get; set; }
        Vector4 UnkVec { get; set; }
        Vector4[] UnkBoundingBox { get; set; }
    }
}
