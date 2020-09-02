using System;
using System.IO;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinSerializeable
    {
        /**
         * Deserialize item from stream
         */
        void Read(BinaryReader reader, Int32 length);
        /**
         * Serialize item
         */
        void Write(BinaryWriter writer);
        /**
         * Get length of item
         */
        Int32 GetLength();
    }
}
