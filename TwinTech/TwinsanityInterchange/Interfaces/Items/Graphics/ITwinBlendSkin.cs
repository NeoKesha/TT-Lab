using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items
{
    public interface ITwinBlendSkin : ITwinItem
    {
        List<ITwinSubBlendSkin> SubBlends { get; set; }
    }
}
