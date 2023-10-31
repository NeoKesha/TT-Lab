using System;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code
{
    public interface ITwinBehaviour : ITwinItem
    {
        /// <summary>
        /// Behaviour's priority in execution queue
        /// </summary>
        Byte Priority { get; set; }
    }
}
