using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinSerializeable
    {
        UInt64 Read(BinaryReader reader);
        UInt64 Writer(BinaryWriter writer);
        UInt64 FromAsset(BinaryReader reader);
        UInt64 ToAsset(BinaryWriter writer);
        UInt64 GetLength();
        UInt64 GetID();
    }
}
