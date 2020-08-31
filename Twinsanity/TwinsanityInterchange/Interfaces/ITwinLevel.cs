using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinLevel : ITwinSerializeable
    {
        ITwinMasterSection GetMasterSectionByKey(EnumMasterSections key);
        List<EnumMasterSections> GetMasterSectionKeyset();
        ITwinItem GetItemByKey(EnumItems key);
        List<ITwinItem> GetItemKeyset();
    }
}
