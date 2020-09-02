using System;
using System.Collections.Generic;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinSection : ITwinItem
    {
        /**
         * Get's item by it's id
         */
        ITwinItem GetItem(UInt32 id);
        /**
         * Get's item by it's key. For unique items like "GameObjects", "Scripts" and etc.
         */
        ITwinItem GetItem(String key);
        /**
         * Get keyset of unique special items
         */
        HashSet<String> GetKeyset();
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
