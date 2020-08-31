using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinSerializeable
    {
        Int32 Read(BinaryReader reader, Int32 length);
        Int32 Write(BinaryWriter writer);
        Int32 FromAsset(BinaryReader reader, Int32 length);
        Int32 ToAsset(BinaryWriter writer);
        Int32 GetLength();
        UInt32 GetID();
    }
}
