using System;
using Twinsanity.TwinsanityInterchange.Common.Animation;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code
{
    public interface ITwinAnimation : ITwinItem
    {
        Boolean HasAnimationData { get; set; }
        Boolean HasFacialAnimationData { get; set; }
        UInt16 TotalFrames { get; set; }
        Byte DefaultFPS { get; set; }
        TwinAnimation MainAnimation { get; set; }
        TwinAnimation FacialAnimation { get; set; }
    }
}
