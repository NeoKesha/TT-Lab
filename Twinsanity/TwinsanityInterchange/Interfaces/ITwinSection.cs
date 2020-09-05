using System;
using System.Collections.Generic;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinSection : ITwinItem
    {
        /**
         * Get length of payload
         */
        int GetContentLength();
        /**
         * Get's item by it's id
         */
        ITwinItem GetItem(UInt32 id);
        /**
         * Remove item by ID
         */
        void RemoveItem(UInt32 id);
        /**
         * Add new item. 
         */
        void AddItem(ITwinItem item);
    }
}
