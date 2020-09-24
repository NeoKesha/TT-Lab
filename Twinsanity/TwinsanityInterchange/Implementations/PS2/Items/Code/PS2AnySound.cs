using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Code
{
    public class PS2AnySound : ITwinSound
    {
        UInt32 id;
        public UInt32 Header;
        public Byte UnkFlag;
        public Byte FreqFac;
        public UInt16 Param1;
        public UInt16 Param2;
        public UInt16 Param3;
        public UInt16 Param4;
        public UInt32 SoundSize;
        public UInt32 SoundOffset;

        public UInt32 GetID()
        {
            return id;
        }

        public Int32 GetLength()
        {
            return 22;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            Header = reader.ReadUInt32();
            UnkFlag = reader.ReadByte();
            FreqFac = reader.ReadByte();
            Param1 = reader.ReadUInt16();
            Param2 = reader.ReadUInt16();
            Param3 = reader.ReadUInt16();
            Param4 = reader.ReadUInt16();
            SoundSize = reader.ReadUInt32();
            SoundOffset = reader.ReadUInt32();
        }

        public void SetID(UInt32 id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Header);
            writer.Write(UnkFlag);
            writer.Write(FreqFac);
            writer.Write(Param1);
            writer.Write(Param2);
            writer.Write(Param3);
            writer.Write(Param4);
            writer.Write(SoundSize);
            writer.Write(SoundOffset);
        }
    }
}
