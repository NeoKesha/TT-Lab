using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class Record : ITwinSerializeable
    {
        UInt32 Offset;
        UInt32 Size;
        UInt32 ID;
        public int FromAsset(BinaryReader reader, int length)
        {
            return Read(reader, length);
        }

        public uint GetID()
        {
            return ID;
        }

        public int GetLength()
        {
            return 12;
        }

        public int Read(BinaryReader reader, int length)
        {
            Offset = reader.ReadUInt32();
            Size = reader.ReadUInt32();
            ID = reader.ReadUInt32();
            return GetLength();
        }

        public int ToAsset(BinaryWriter writer)
        {
            return Write(writer);
        }

        public int Write(BinaryWriter writer)
        {
            writer.Write(Offset);
            writer.Write(Size);
            writer.Write(ID);
            return GetLength();
        }
    }
}
