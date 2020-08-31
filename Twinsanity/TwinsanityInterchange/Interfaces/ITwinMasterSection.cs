using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinMasterSection : ITwinSerializeable
    {
        ITwinSection GetSectionByKey(EnumSections key);
        List<EnumSections> GetSectionKeyset();
    }
}
