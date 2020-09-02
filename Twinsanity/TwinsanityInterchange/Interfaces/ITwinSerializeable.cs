using System;
using System.IO;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinSerializeable
    {
        Int32 Read(BinaryReader reader, Int32 length);
        Int32 Write(BinaryWriter writer);
        Int32 GetLength();
    }
}
