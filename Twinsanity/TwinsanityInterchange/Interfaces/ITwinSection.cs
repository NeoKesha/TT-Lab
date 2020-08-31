using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinSection : ITwinSerializeable
    {
        ITwinItem GetItemById(UInt32 id);
        void RemoveItem(UInt32 id);
        void RemoveItem(ITwinItem item);
        void AddItem(ITwinItem item);
    }
}
