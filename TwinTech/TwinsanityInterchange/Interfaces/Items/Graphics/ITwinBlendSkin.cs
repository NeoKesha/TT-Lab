using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items
{
    public interface ITwinBlendSkin : ITwinItem
    {
        /// <summary>
        /// Submodels/Subblends the model consists of
        /// </summary>
        List<ITwinSubBlendSkin> SubBlends { get; set; }
    }
}
