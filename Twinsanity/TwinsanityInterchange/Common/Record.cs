using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class Record : ITwinSerializeable
    {
        public UInt32 Offset { get; set; }
        public UInt32 Size { get; set; }
        public UInt32 ItemId { get; set; }
        public int GetLength()
        {
            return Constants.SIZE_RECORD;
        }

        public void Read(BinaryReader reader, int length)
        {
            Offset = reader.ReadUInt32();
            Size = reader.ReadUInt32();
            ItemId = reader.ReadUInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Offset);
            writer.Write(Size);
            writer.Write(ItemId);
        }
    }
}
