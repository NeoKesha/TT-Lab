using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items
{
    public interface ITwinSkin : ITwinItem
    {
        List<ITwinSubSkin> SubSkins { get; set; }
    }
}
