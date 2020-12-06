using System;
using System.IO;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class MBRecord : ITwinSerializable
    {
        private MHRecord RecordHeader;

        // Validation stuff
        private readonly Char[] MSVp = "MSVp".ToCharArray();
        private readonly UInt32 Key = 0x20000000;

        public Int32 SampleRate;
        public String Name;
        public Byte[] TrackData;

        public MBRecord(MHRecord header)
        {
            RecordHeader = header;
        }

        public Int32 GetLength()
        {
            switch (RecordHeader.Type)
            {
                case MHRecord.RecordType.MONO:
                    return RecordHeader.Size - 0x30;
                case MHRecord.RecordType.STEREO:
                    return RecordHeader.Size;
            }
            return 0;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            switch (RecordHeader.Type)
            {
                case MHRecord.RecordType.MONO:
                    var msvp = reader.ReadChars(4);
                    if (!String.Equals(new String(msvp), new String(MSVp), StringComparison.Ordinal))
                    {
                        throw new Exception("MSVp key not provided!");
                    }
                    var testKey = reader.ReadUInt32();
                    var testZero = reader.ReadInt32();
                    if (testKey != Key || testZero != 0)
                    {
                        throw new Exception("Invalid key provided!");
                    }
                    var testSize = BitConv.FlipBytes(reader.ReadUInt32());
                    if (testSize != GetLength())
                    {
                        throw new Exception("Sizes in header and main archives do not match!");
                    }
                    SampleRate = BitConv.FlipBytes(reader.ReadInt32());
                    reader.ReadInt32();
                    reader.ReadInt64();
                    Name = new String(reader.ReadChars(0x10));
                    break;
            }
            TrackData = reader.ReadBytes(GetLength());
        }

        public void Write(BinaryWriter writer)
        {
            switch (RecordHeader.Type)
            {
                case MHRecord.RecordType.MONO:
                    writer.Write(MSVp);
                    writer.Write(Key);
                    writer.Write(0);
                    writer.Write(BitConv.FlipBytes(GetLength()));
                    writer.Write(BitConv.FlipBytes(SampleRate));
                    writer.Write(0);
                    writer.Write((Int64)0);
                    writer.Write(Name.ToCharArray(), 0, 0x10);
                    break;
            }
            writer.Write(TrackData);
        }
    }
}
