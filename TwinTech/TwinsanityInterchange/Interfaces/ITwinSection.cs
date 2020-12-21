using System;
using System.Collections.Generic;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinSection : ITwinItem
    {
        /**
         * Get length of payload
         */
        Int32 GetContentLength();
        /**
         * Gets item by collection's id
         */
        ITwinItem GetItem(Int32 index);
        /**
         * Gets item by its id
         */
        T GetItem<T>(UInt32 id) where T : ITwinItem;
        UInt32 GetIdByIndex(Int32 index);
        /**
         * Remove item by ID
         */
        void RemoveItem<T>(UInt32 id) where T : ITwinItem;
        /**
         * Add new item. 
         */
        void AddItem(ITwinItem item);
        /**
         * Get amount of stored items
         */
        Int32 GetItemsAmount();
    }
}
