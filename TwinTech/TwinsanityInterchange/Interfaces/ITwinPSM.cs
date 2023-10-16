using System.Collections.Generic;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinPSM : ITwinSerializable
    {
        public List<ITwinPTC> PTCs { get; set; }
    }
}
