using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    interface ITwinTypeMapper
    {
        EnumType IdToType(UInt32 id);
        String IdToKey(UInt32 id);
        UInt32 TypeToId(EnumType type);
        String TypeToKey(EnumType type);
        UInt32 KeyToId(String key);
        EnumType KeyToType(String key);
    }
}
