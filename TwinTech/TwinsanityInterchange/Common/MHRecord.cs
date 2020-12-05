using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class MHRecord : ITwinSerializable
    {
        public RecordType Type;
        public Int32 Size;
        public UInt32 Offset;
        public Int32 SampleRate;
        public Int32 UnkInt;

        public Int32 GetLength()
        {
            return 20;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            Type = (RecordType)reader.ReadInt32();
            Size = reader.ReadInt32();
            Offset = reader.ReadUInt32();
            SampleRate = reader.ReadInt32();
            UnkInt = reader.ReadInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((Int32)Type);
            writer.Write(Size);
            writer.Write(Offset);
            writer.Write(SampleRate);
            writer.Write(UnkInt);
        }

        public enum RecordType
        {
            MONO,
            STEREO,
            NULL
        }
    }
}
