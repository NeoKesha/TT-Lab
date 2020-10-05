using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.VIF
{
    public class GIFTag
    {
        public UInt16 NLOOP { get; set; }
        public Byte EOP { get; set; }
        public Byte PRE { get; set; }
        public UInt16 PRIM { get; set; }
        public GIFModeEnum FLG { get; set; }
        public Byte NREG { get; set; }
        public REGSEnum[] REGS { get; set; }
        public List<RegOutput> Data { get; set; }
        private UInt64 Q = 0x3F800000;
        public void Read(BinaryReader reader)
        {
            UInt64 low = reader.ReadUInt64();
            NLOOP = (UInt16)((low & (UInt64)0b111111111111111) >> 0);
            EOP = (Byte)((low & ((UInt64)0b1 << 15)) >> 15);
            PRE = (Byte)((low & ((UInt64)0b1 << 46)) >> 46);
            PRIM = (UInt16)((low & ((UInt64)0b11111111111 << 47)) >> 47);
            FLG = (GIFModeEnum)((low & (((UInt64)0b11) << 58)) >> 58);
            NREG = (Byte)((low & ((UInt64)0b1111 << 60)) >> 60);
            NREG = (NREG == 0) ? (Byte)16 : NREG;
            REGS = new REGSEnum[16];
            UInt64 high = reader.ReadUInt64();
            for (int i = 0; i < 16; ++i)
            {
                REGS[i] = (REGSEnum)(high & 0b1111);
                high >>= 4;
            }
            Data = new List<RegOutput>();
            if (PRE == 1)
            {
                RegOutput prim = new RegOutput();
                prim.REG = REGSEnum.PRIM;
                prim.Output = PRIM;
                Data.Add(prim);
            }
            if (FLG == GIFModeEnum.IMAGE)
            {
                for (int i = 0; i < NLOOP; ++i)
                {
                    Interpret(reader, REGSEnum.HWREG, Data);
                }
            } else
            {
                for (int i = 0; i < NLOOP; ++i)
                {
                    for (int j = 0; j < NREG; ++j)
                    {
                        Interpret(reader, REGS[j], Data);
                    }
                }
            }
            
        }
        private void Interpret(BinaryReader reader, REGSEnum REG, List<RegOutput> list)
        {
            RegOutput output = new RegOutput();
            UInt64 low, high;
            switch (FLG)
            {
                case GIFModeEnum.PACKED:
                    high = reader.ReadUInt64();
                    low = reader.ReadUInt64();
                    output.REG = REG;
                    switch (REG)
                    {
                        case REGSEnum.RGBAQ:
                            UInt64 r = GetBits(low, 8, 0);
                            UInt64 g = GetBits(low, 8, 23);
                            UInt64 b = GetBits(low, 8, 46);
                            UInt64 a = GetBits(low, 8, 69);
                            output.Output = SetBits(SetBits(SetBits(r, g, 23), b, 46), a, 69);
                            break;
                        case REGSEnum.ST:
                            UInt64 s = GetBits(low, 32, 0);
                            UInt64 t = GetBits(low, 32, 32);
                            UInt64 q = GetBits(high, 32, 0);
                            Q = q;
                            output.Output = SetBits(s,t,32);
                            break;
                        case REGSEnum.UV:
                            UInt64 v = GetBits(low, 14, 0);
                            UInt64 u = GetBits(low, 14, 32);
                            output.Output = SetBits(v, u, 16);
                            break;
                        case REGSEnum.XYZF2:
                            {
                                UInt64 x = GetBits(low, 16, 0);
                                UInt64 y = GetBits(low, 16, 32);
                                UInt64 z = GetBits(high, 24, 4);
                                UInt64 f = GetBits(high, 8, 36);
                                UInt64 adc = GetBits(high, 1, 47);
                                if (adc == 0)
                                {
                                    output.REG = REGSEnum.XYZF2;
                                }
                                else
                                {
                                    output.REG = REGSEnum.XYZF3;
                                }
                                output.Output = SetBits(SetBits(SetBits(x, y, 16), z, 32), f, 56);
                            }
                            break;
                        case REGSEnum.XYZ2:
                            {
                                UInt64 x = GetBits(low, 16, 0);
                                UInt64 y = GetBits(low, 16, 32);
                                UInt64 z = GetBits(high, 32, 0);
                                UInt64 adc = GetBits(high, 1, 47);
                                if (adc == 0)
                                {
                                    output.REG = REGSEnum.XYZ2;
                                }
                                else
                                {
                                    output.REG = REGSEnum.XYZ3;
                                }
                                output.Output = SetBits(SetBits(x, y, 16), z, 32);
                            }
                            
                            break;
                        case REGSEnum.FOG:
                            {
                                UInt64 f = GetBits(high, 8, 36);
                                output.Output = SetBits(0, f, 56);
                            }
                            break;
                        case REGSEnum.ApD:
                            UInt64 Data = GetBits(low, 64, 0);
                            UInt64 Addr = GetBits(high, 7, 0);
                            output.Output = Data;
                            output.Address = Addr;
                            break;
                        case REGSEnum.TEX0_1:
                        case REGSEnum.TEX1_1:
                        case REGSEnum.CLAMP_1:
                        case REGSEnum.CLAMP_2:
                        case REGSEnum.XYZF3:
                        case REGSEnum.XYZ3:
                            output.Output = low;
                            break;
                        case REGSEnum.NOP:
                        default:
                            break;
                    }
                    list.Add(output);
                    break;
                case GIFModeEnum.REGLIST:

                    break;
                case GIFModeEnum.IMAGE:
                    RegOutput output1 = new RegOutput();
                    RegOutput output2 = new RegOutput();
                    high = reader.ReadUInt64();
                    output2.REG = REGSEnum.HWREG;
                    output2.Output = high;
                    low = reader.ReadUInt64();
                    output1.REG = REGSEnum.HWREG;
                    output1.Output = low;
                    list.Add(output1);
                    list.Add(output2);
                    break;
                case GIFModeEnum.DISABLE:
                    break;
            }
        }
        private UInt64 GetBits(UInt64 src, Byte len, Byte offset)
        {
            UInt64 mask = 0;
            for (int i = 0; i < len; ++i)
            {
                mask = (mask << 1) | 1;
            }
            return ((src & ((UInt64)0b11111111 << offset)) >> offset);
        }
        private UInt64 SetBits(UInt64 src, UInt64 val, Byte offset)
        {
            return src | (val << offset);
        }
        public void Write(BinaryWriter writer)
        {
            UInt64 low = 0;
            low |= (UInt64)NLOOP & (0b111111111111111 << 0);
            low |= (UInt64)EOP & (0b1 << 15);
            low |= (UInt64)PRE & (0b1 << 46);
            low |= (UInt64)PRIM & (0b1 << 47);
            low |= (UInt64)FLG & (0b1 << 58);
            low |= (UInt64)NREG & (0b1 << 60);
            writer.Write(low);
            UInt64 high = 0;
            for (int i = 0; i < 16; ++i)
            {
                high |= (UInt64)REGS[REGS.Length - i] & 0b1111;
                high <<= 4;
            }
            writer.Write(high);
            //TODO: DATA WRITE
        }
        public Int32 GetLength()
        {
            switch (FLG)
            {
                case GIFModeEnum.PACKED:
                    return NREG * NLOOP;
                case GIFModeEnum.REGLIST:
                    return NREG * NLOOP;
                case GIFModeEnum.IMAGE:
                    return NLOOP;
                case GIFModeEnum.DISABLE:
                    return 0;
            }
            return 0;
        }
    }
    public enum GIFModeEnum
    {
        PACKED = 0b00,
        REGLIST = 0b01,
        IMAGE = 0b10,
        DISABLE = 0b11
    }
    public enum REGSEnum
    {
        PRIM = 0x00,
        RGBAQ = 0x01,
        ST = 0x02,
        UV = 0x03,
        XYZF2 = 0x04,
        XYZ2 = 0x05,
        TEX0_1 = 0x06,
        TEX1_1 = 0x07,
        CLAMP_1 = 0x08,
        CLAMP_2 = 0x09,
        FOG = 0x0a,
        RESERVED = 0x0b,
        XYZF3 = 0x0c,
        XYZ3 = 0x0d,
        ApD = 0x0e,
        NOP = 0x0f,
        HWREG = 0xff
    }
    public class RegOutput
    {
        public UInt64 Output { get; set; }
        public REGSEnum REG { get; set; }
        public UInt64 Address { get; set; }
    }
}
