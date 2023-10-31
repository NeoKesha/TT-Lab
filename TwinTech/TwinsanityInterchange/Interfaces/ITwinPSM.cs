using System.Collections.Generic;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinPSM : ITwinSerializable
    {
        /// <summary>
        /// Parts of the PSM
        /// </summary>
        public List<ITwinPTC> PTCs { get; set; }
    }
}
