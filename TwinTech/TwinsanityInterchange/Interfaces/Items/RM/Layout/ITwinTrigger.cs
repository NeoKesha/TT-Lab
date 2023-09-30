using System;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout
{
    public interface ITwinTrigger : ITwinItem
    {
        TwinTrigger Trigger { get; set; }
        UInt16[] TriggerScripts { get; set; }
    }
}
