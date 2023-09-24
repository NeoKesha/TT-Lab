using System;
using System.IO;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinItem : ITwinSerializable
    {
        /**
         * Get item's ID
         */
        UInt32 GetID();
        /**
         * Set item's ID
         */
        void SetID(UInt32 id);
        /**
         * Get item's name
         */
        String GetName();

        String GetHash();
        void ComputeHash(Stream stream);
        void ComputeHash(Stream stream, UInt32 length);
        bool GetIsLoaded();
        void SetIsLazy(bool isLazy);
        ITwinItem GetRoot();
        void SetRoot(ITwinItem root);
        MemoryStream GetStream();
        void SetStream(MemoryStream stream);
    }
}
