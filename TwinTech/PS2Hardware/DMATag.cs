using System;
using System.IO;

namespace Twinsanity.PS2Hardware
{
    public class DMATag
    {
        public UInt16 QWC;
        public Byte PCE;
        public IdType ID;
        public Byte IRQ;
        public UInt32 ADDR;
        public Byte SPR;
        public UInt64 Extra;
        public void Read(BinaryReader reader)
        {
            var low = reader.ReadUInt64();
            QWC = (UInt16)(low & 0xFFFF);
            PCE = (Byte)(low >> 26 & 0b11);
            ID = (IdType)(low >> 28 & 0b111);
            IRQ = (Byte)(low >> 31 & 0b1);
            ADDR = (UInt32)(low >> 32 & 0x7FFFFFFF);
            SPR = (Byte)(low >> 63 & 0b1);
            Extra = reader.ReadUInt64();
        }

        public void Write(BinaryWriter writer)
        {
            UInt64 low = QWC;
            low |= (UInt64)PCE << 26;
            low |= (UInt64)ID << 28;
            low |= (UInt64)IRQ << 31;
            low |= (UInt64)ADDR << 32;
            low |= (UInt64)SPR << 63;
            writer.Write(low);
            writer.Write(Extra);
        }

        public enum IdType
        {
            REFE = 0b000,
            CNT = 0b001,
            NEXT = 0b010,
            REF = 0b011,
            REFS = 0b100,
            CALL = 0b101,
            RET = 0b110,
            END = 0b111
        }
    }
}
