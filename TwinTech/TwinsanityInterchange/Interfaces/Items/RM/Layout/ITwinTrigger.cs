using System;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout
{
    public interface ITwinTrigger : ITwinItem
    {
        /// <summary>
        /// Trigger's information
        /// </summary>
        TwinTrigger Trigger { get; set; }
        /// <summary>
        /// Unknown arguments
        /// </summary>
        UInt16[] TriggerArguments { get; set; }
    }
}
