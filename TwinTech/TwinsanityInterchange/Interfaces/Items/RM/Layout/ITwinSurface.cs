using System;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout
{
    public interface ITwinSurface : ITwinItem
    {
        /// <summary>
        /// Surface flags
        /// </summary>
        UInt32 Flags { get; set; }
        /// <summary>
        /// Surface's ID
        /// </summary>
        UInt16 SurfaceId { get; set; }
        /// <summary>
        /// Sound/SFX ID to play when stepped on
        /// </summary>
        UInt16 StepSoundId1 { get; set; }
        /// <summary>
        /// Sound/SFX ID to play when stepped on (2nd variant?)
        /// </summary>
        UInt16 StepSoundId2 { get; set; }
        /// <summary>
        /// Unknown ID
        /// </summary>
        UInt16 UnkId1 { get; set; }
        /// <summary>
        /// Unknown ID
        /// </summary>
        UInt16 UnkId2 { get; set; }
        /// <summary>
        /// Sound/SFX to play when landed on
        /// </summary>
        UInt16 LandSoundId1 { get; set; }
        /// <summary>
        /// Unknown ID
        /// </summary>
        UInt16 UnkId3 { get; set; }
        /// <summary>
        /// Unknown ID
        /// </summary>
        UInt16 UnkId4 { get; set; }
        /// <summary>
        /// Sound/SFX to play when landed on (2nd variant?)
        /// </summary>
        UInt16 LandSoundId2 { get; set; }
        /// <summary>
        /// Unknown sound ID
        /// </summary>
        UInt16 UnkSoundId { get; set; }
        /// <summary>
        /// Unknown ID
        /// </summary>
        UInt16 UnkId5 { get; set; }
        /// <summary>
        /// Float parameters. Slippiness, friction, etc.
        /// </summary>
        Single[] UnkFloatParams { get; set; }
        /// <summary>
        /// Unknown vector
        /// </summary>
        Vector4 UnkVec { get; set; }
        /// <summary>
        /// Unknown bounding box
        /// </summary>
        Vector4[] UnkBoundingBox { get; set; }
    }
}
