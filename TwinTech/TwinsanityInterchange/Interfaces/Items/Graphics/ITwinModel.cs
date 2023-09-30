using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items
{
    public interface ITwinModel : ITwinItem
    {
        List<ITwinSubModel> SubModels { get; set; }
    }
}
