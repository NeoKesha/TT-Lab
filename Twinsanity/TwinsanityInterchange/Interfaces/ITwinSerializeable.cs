using System;
using System.IO;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinSerializeable
    {
        /**
         * Deserialize item from stream
         */
        Int32 Read(BinaryReader reader, Int32 length);
        /**
         * Serialize item
         */
        Int32 Write(BinaryWriter writer);
        /**
         * Get length of item
         */
        Int32 GetLength();
    }
}
