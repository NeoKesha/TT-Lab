using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.VIF
{
    public class VIFInterpreter
    {
        public UInt32[] VIFn_R = { 0, 0, 0, 0 };
        public UInt32[] VIFn_C = { 0, 0, 0, 0 };
        public UInt32 VIFn_CYCLE;
        public UInt32 VIFn_MASK;
        public UInt32 VIFn_MODE;
        public UInt32 VIFn_ITOP;
        public UInt32 VIFn_ITOPS;
        public UInt32 VIF1_BASE;
        public UInt32 VIF1_OFST;
        public UInt32 VIF1_TOP;
        public UInt32 VIF1_TOPS;
        public UInt32 VIFn_MARK;
        public UInt32 VIFn_NUM;
        public UInt32 VIFn_CODE;
        public void Execute(BinaryReader reader)
        {
            while(reader.BaseStream.Position < reader.BaseStream.Length)
            {
                VIFCode vif = new VIFCode();
                vif.Read(reader);
                if (vif.isUnpack())
                {
                    Byte cmd = (Byte)vif.OP;
                    Byte vn = (Byte)((cmd & 0b1100) >> 2);
                    Byte vl = (Byte)((cmd & 0b0011) >> 0);
                    Byte m = (Byte)((cmd & 0b10000) >> 4);
                    Byte amount = vif.Amount;
                    UInt16 addr = (UInt16)(vif.Immediate & 0b111111111);
                    Byte usn = (Byte)(vif.Immediate & 0b010000000000000);
                    Byte flg = (Byte)(vif.Immediate & 0b100000000000000);
                    Byte WL = (Byte)((VIFn_CYCLE & 0xFF) >> 8);
                    Byte CL = (Byte)((VIFn_CYCLE & 0xFF) >> 0);
                    UInt32 dimensions = (UInt32)(vn + 1);
                    UInt32 size = 0;
                    switch (vl)
                    {
                        case 0b00: size = 4; break;
                        case 0b01: size = 2; break;
                        case 0b10: size = 1; break;
                        case 0b11: size = 2; break;
                    }
                    UInt32 packet_length = 0;
                    if (WL <= CL)
                    {
                        UInt32 a = (UInt32)(32 >> vl);
                        UInt32 b = (UInt32)(vn + 1);
                        Single c = (Single)(a * b * amount);
                        Single d = c / 32.0f;
                        Single e = (Single)Math.Ceiling(d);
                        UInt32 f = (UInt32)e;
                        packet_length = (UInt32)(1 + f) ;
                    } else
                    {
                        UInt32 n = (UInt32)(CL * (amount / WL) + ((amount % WL) > CL ?CL: (amount % WL)));
                        packet_length = (UInt32)(1 + (Math.Ceiling(((32 >> vl) * (vn + 1)) * n / 32.0)));
                    }
                    reader.ReadBytes(((int)packet_length - 1)*4);
                    Console.WriteLine($"UNPACK {((int)packet_length - 1) * 4} bytes into {amount} 128bit vectors");
                } 
                else
                {
                    Console.WriteLine(vif.OP.ToString());
                    switch (vif.OP)
                    {
                        case VIFCodeEnum.NOP:

                            break;
                        case VIFCodeEnum.STCYCL:
                            VIFn_CYCLE = vif.Immediate;
                            break;
                        case VIFCodeEnum.OFFSET:
                            VIF1_OFST = (uint)(vif.Immediate & 0b1111111111);
                            break;
                        case VIFCodeEnum.BASE:
                            VIF1_BASE = (uint)(vif.Immediate & 0b1111111111);
                            break;
                        case VIFCodeEnum.ITOP:
                            VIFn_ITOPS = (uint)(vif.Immediate & 0b1111111111);
                            break;
                        case VIFCodeEnum.STMOD:
                            VIFn_MODE = (uint)(vif.Immediate & 0b11);
                            break;
                        case VIFCodeEnum.MSKPATH3:
                            throw new NotImplementedException();
                        case VIFCodeEnum.MARK:
                            VIFn_MARK = vif.Immediate;
                            break;
                        case VIFCodeEnum.FLUSHE:

                            break;
                        case VIFCodeEnum.FLUSH:

                            break;
                        case VIFCodeEnum.FLUSHA:

                            break;
                        case VIFCodeEnum.MSCAL:
                            //throw new NotImplementedException();
                            break;
                        case VIFCodeEnum.MSCNT:
                            //throw new NotImplementedException();
                            break;
                        case VIFCodeEnum.MSCALF:
                            //throw new NotImplementedException();
                            break;
                        case VIFCodeEnum.STMASK:
                            VIFn_MASK = reader.ReadUInt32();
                            break;
                        case VIFCodeEnum.STROW:
                            VIFn_R[0] = reader.ReadUInt32();
                            VIFn_R[1] = reader.ReadUInt32();
                            VIFn_R[2] = reader.ReadUInt32();
                            VIFn_R[3] = reader.ReadUInt32();
                            break;
                        case VIFCodeEnum.STCOL:
                            VIFn_C[0] = reader.ReadUInt32();
                            VIFn_C[1] = reader.ReadUInt32();
                            VIFn_C[2] = reader.ReadUInt32();
                            VIFn_C[3] = reader.ReadUInt32();
                            break;
                        case VIFCodeEnum.MPG:
                            throw new NotImplementedException();
                        case VIFCodeEnum.DIRECT:
                            UInt32 amount = (UInt32)((vif.Immediate == 0)?65536*16 : vif.Immediate*16);
                            List<GIFTag> tags = new List<GIFTag>();
                            bool flag = false;
                            int len = 0;
                            do
                            {
                                GIFTag tag = new GIFTag();
                                tag.Read(reader);
                                tags.Add(tag);
                                flag = tag.EOP != 1;
                                int tagLen = tag.GetLength();
                                len += tagLen;
                            } while (flag);
                            break;
                        case VIFCodeEnum.DIRECTHL:

                            break;
                    }
                }
            }
        }
    }
}
