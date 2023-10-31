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
        /// List of scripts/behaviours to activate when touched
        /// </summary>
        UInt16[] TriggerScripts { get; set; }
    }
}
