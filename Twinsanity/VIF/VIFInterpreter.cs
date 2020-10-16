using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;

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

        private List<List<Vector4>> VUMem = new List<List<Vector4>>();
        private List<UInt32> tmpStack = new List<UInt32>();
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
                    } 
                    else
                    {
                        UInt32 n = (UInt32)(CL * (amount / WL) + ((amount % WL) > CL ?CL: (amount % WL)));
                        UInt32 a = (UInt32)(32 >> vl);
                        UInt32 b = (UInt32)(vn + 1);
                        Single c = (Single)(a * b * n);
                        Single d = c / 32.0f;
                        Single e = (Single)Math.Ceiling(d);
                        UInt32 f = (UInt32)e;
                        packet_length = (UInt32)(1 + f);
                    }
                    PackFormat fmt = (PackFormat)(vl | (vn << 2));
                    List<Vector4> vectors = new List<Vector4>();
                    tmpStack.Clear();
                    for (int i = 0; i < packet_length - 1; ++i)
                    {
                        tmpStack.Add(reader.ReadUInt32());
                    }
                    Unpack(tmpStack, vectors, fmt);
                    VUMem.Add(vectors);
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

        private void Unpack(List<UInt32> src, List<Vector4> dst, PackFormat fmt)
        {
            switch(fmt)
            {
                case PackFormat.S_32:
                    for (int i = 0; i < src.Count; ++i)
                    {
                        Vector4 v = new Vector4();
                        v.SetBinaryX(src[i]);
                        v.SetBinaryY(src[i]);
                        v.SetBinaryZ(src[i]);
                        v.SetBinaryW(src[i]);
                        dst.Add(v);
                    }
                    break;
                case PackFormat.S_16:
                    for (int i = 0; i < src.Count / 2; ++i)
                    {
                        Vector4 v1 = new Vector4(); UInt32 w1 = src[i * 2 + 0] & 0x0000FFFF;
                        Vector4 v2 = new Vector4(); UInt32 w2 = (src[i * 2 + 0] & 0xFFFF0000) >> 16;
                        Vector4 v3 = new Vector4(); UInt32 w3 = src[i * 2 + 1] & 0x0000FFFF;
                        w1 = ((w1 & 0x8000) != 0) ? w1 & 0xFFFF0000 : w1;
                        w2 = ((w2 & 0x8000) != 0) ? w2 & 0xFFFF0000 : w2;
                        w3 = ((w3 & 0x8000) != 0) ? w3 & 0xFFFF0000 : w3;
                        v1.SetBinaryX(w1);
                        v1.SetBinaryY(w1);
                        v1.SetBinaryZ(w1);
                        v1.SetBinaryW(w1);
                        v2.SetBinaryX(w2);
                        v2.SetBinaryY(w2);
                        v2.SetBinaryZ(w2);
                        v2.SetBinaryW(w2);
                        v3.SetBinaryX(w3);
                        v3.SetBinaryY(w3);
                        v3.SetBinaryZ(w3);
                        v3.SetBinaryW(w3);
                        dst.Add(v1);
                        dst.Add(v2);
                        dst.Add(v3);
                    }
                    break;
                case PackFormat.S_8:
                    for (int i = 0; i < src.Count; ++i)
                    {
                        Vector4 v1 = new Vector4(); UInt32 w1 = src[i + 0] & 0x000000FF;
                        Vector4 v2 = new Vector4(); UInt32 w2 = (src[i + 0] & 0x0000FF00) >> 8;
                        Vector4 v3 = new Vector4(); UInt32 w3 = (src[i + 0] & 0x00FF0000) >> 16;
                        w1 = ((w1 & 0x80) != 0) ? w1 & 0xFFFFFF00 : w1;
                        w2 = ((w2 & 0x80) != 0) ? w2 & 0xFFFFFF00 : w2;
                        w3 = ((w3 & 0x80) != 0) ? w3 & 0xFFFFFF00 : w3;
                        v1.SetBinaryX(w1);
                        v1.SetBinaryY(w1);
                        v1.SetBinaryZ(w1);
                        v1.SetBinaryW(w1);
                        v2.SetBinaryX(w2);
                        v2.SetBinaryY(w2);
                        v2.SetBinaryZ(w2);
                        v2.SetBinaryW(w2);
                        v3.SetBinaryX(w3);
                        v3.SetBinaryY(w3);
                        v3.SetBinaryZ(w3);
                        v3.SetBinaryW(w3);
                        dst.Add(v1);
                        dst.Add(v2);
                        dst.Add(v3);
                    }
                    break;
                case PackFormat.V2_32:
                    for (int i = 0; i < src.Count / 2; ++i)
                    {
                        Vector4 v = new Vector4();
                        v.SetBinaryX(src[i * 2 + 0]);
                        v.SetBinaryY(src[i * 2 + 1]);
                        dst.Add(v);
                    }
                    break;
                case PackFormat.V2_16:
                    for (int i = 0; i < src.Count; ++i)
                    {
                        Vector4 v = new Vector4();
                        UInt32 w1 = src[i] & 0x0000FFFF;
                        UInt32 w2 = (src[i] & 0xFFFF0000) >> 16;
                        w1 = ((w1 & 0x8000) != 0) ? w1 & 0xFFFF0000 : w1;
                        w2 = ((w2 & 0x8000) != 0) ? w2 & 0xFFFF0000 : w2;
                        v.SetBinaryX(w1);
                        v.SetBinaryY(w2);
                        dst.Add(v);
                    }
                    break;
                case PackFormat.V2_8:
                    for (int i = 0; i < src.Count / 2; ++i)
                    {
                        Vector4 v1 = new Vector4();
                        UInt32 w1 = src[i * 2] & 0x000000FF;
                        UInt32 w2 = (src[i * 2] & 0x0000FF00) >> 8;
                        Vector4 v2 = new Vector4();
                        UInt32 w3 = (src[i * 2] & 0x00FF0000) >> 16;
                        UInt32 w4 = (src[i * 2] & 0xFF000000) >> 24;
                        Vector4 v3 = new Vector4();
                        UInt32 w5 = src[i * 2 + 1] & 0x000000FF;
                        UInt32 w6 = (src[i * 2 + 1] & 0x0000FF00) >> 8;
                        w1 = ((w1 & 0x80) != 0) ? w1 & 0xFFFFFF00 : w1;
                        w2 = ((w2 & 0x80) != 0) ? w2 & 0xFFFFFF00 : w2;
                        w3 = ((w1 & 0x80) != 0) ? w3 & 0xFFFFFF00 : w3;
                        w4 = ((w2 & 0x80) != 0) ? w4 & 0xFFFFFF00 : w4;
                        w5 = ((w1 & 0x80) != 0) ? w5 & 0xFFFFFF00 : w5;
                        w6 = ((w2 & 0x80) != 0) ? w6 & 0xFFFFFF00 : w6;
                        v1.SetBinaryX(w1);
                        v1.SetBinaryY(w2);
                        v1.SetBinaryX(w3);
                        v1.SetBinaryY(w4);
                        v1.SetBinaryX(w5);
                        v1.SetBinaryY(w6);
                        dst.Add(v1);
                        dst.Add(v2);
                        dst.Add(v3);
                    }
                    break;
                case PackFormat.V3_32:
                    for (int i = 0; i < src.Count / 3; ++i)
                    {
                        Vector4 v = new Vector4();
                        v.SetBinaryX(src[i * 3 + 0]);
                        v.SetBinaryY(src[i * 3 + 1]);
                        v.SetBinaryZ(src[i * 3 + 2]);
                        dst.Add(v);
                    }
                    break;
                case PackFormat.V3_16:
                    for (int i = 0; i < src.Count / 3; ++i)
                    {
                        Vector4 v1 = new Vector4();
                        Vector4 v2 = new Vector4();
                        UInt32 w1 = src[i * 3] & 0x0000FFFF;
                        UInt32 w2 = (src[i * 3] & 0xFFFF0000) >> 16;
                        UInt32 w3 = src[i * 3 + 1] & 0x0000FFFF;
                        UInt32 w4 = (src[i * 3 + 1] & 0xFFFF0000) >> 16;
                        UInt32 w5 = src[i * 3 + 2] & 0x0000FFFF;
                        UInt32 w6 = (src[i * 3 + 2] & 0xFFFF0000) >> 16;
                        w1 = ((w1 & 0x8000) != 0) ? w1 & 0xFFFF0000 : w1;
                        w2 = ((w2 & 0x8000) != 0) ? w2 & 0xFFFF0000 : w2;
                        w3 = ((w3 & 0x8000) != 0) ? w3 & 0xFFFF0000 : w3;
                        w4 = ((w4 & 0x8000) != 0) ? w4 & 0xFFFF0000 : w4;
                        w5 = ((w5 & 0x8000) != 0) ? w5 & 0xFFFF0000 : w5;
                        w6 = ((w6 & 0x8000) != 0) ? w6 & 0xFFFF0000 : w6;
                        v1.SetBinaryX(w1);
                        v1.SetBinaryY(w2);
                        v1.SetBinaryZ(w3);
                        v2.SetBinaryX(w4);
                        v2.SetBinaryY(w5);
                        v2.SetBinaryZ(w6);
                        dst.Add(v1);
                        dst.Add(v2);
                    }
                    break;
                case PackFormat.V3_8:
                    for (int i = 0; i < src.Count / 3; ++i)
                    {
                        Vector4 v1 = new Vector4();
                        Vector4 v2 = new Vector4();
                        Vector4 v3 = new Vector4();
                        UInt32 w1 = src[i * 3] & 0x000000FF;
                        UInt32 w2 = (src[i * 3] & 0x0000FF00) >> 8;
                        UInt32 w3 = (src[i * 3] & 0x00FF0000) >> 16;
                        UInt32 w4 = (src[i * 3] & 0xFF000000) >> 24;
                        UInt32 w5 = src[i * 3 + 1] & 0x000000FF;
                        UInt32 w6 = (src[i * 3 + 1] & 0x0000FF00) >> 8;
                        UInt32 w7 = (src[i * 3 + 1] & 0x00FF0000) >> 16;
                        UInt32 w8 = (src[i * 3 + 1] & 0xFF000000) >> 24;
                        UInt32 w9 = src[i * 3 + 2] & 0x000000FF;
                        w1 = ((w1 & 0x80) != 0) ? w1 & 0xFFFFFF00 : w1;
                        w2 = ((w2 & 0x80) != 0) ? w2 & 0xFFFFFF00 : w2;
                        w3 = ((w3 & 0x80) != 0) ? w3 & 0xFFFFFF00 : w3;
                        w4 = ((w4 & 0x80) != 0) ? w4 & 0xFFFFFF00 : w4;
                        w5 = ((w5 & 0x80) != 0) ? w5 & 0xFFFFFF00 : w5;
                        w6 = ((w6 & 0x80) != 0) ? w6 & 0xFFFFFF00 : w6;
                        w7 = ((w7 & 0x80) != 0) ? w7 & 0xFFFFFF00 : w7;
                        w8 = ((w8 & 0x80) != 0) ? w8 & 0xFFFFFF00 : w8;
                        w9 = ((w9 & 0x80) != 0) ? w9 & 0xFFFFFF00 : w9;
                        v1.SetBinaryX(w1);
                        v1.SetBinaryY(w2);
                        v1.SetBinaryZ(w3);
                        v2.SetBinaryX(w4);
                        v2.SetBinaryY(w5);
                        v2.SetBinaryZ(w6);
                        v2.SetBinaryX(w7);
                        v2.SetBinaryY(w8);
                        v2.SetBinaryZ(w9);
                        dst.Add(v1);
                        dst.Add(v2);
                        dst.Add(v3);
                    }
                    break;
                case PackFormat.V4_32:
                    for (int i = 0; i < src.Count / 4; ++i)
                    {
                        Vector4 v = new Vector4();
                        v.SetBinaryX(src[i * 4 + 0]);
                        v.SetBinaryY(src[i * 4 + 1]);
                        v.SetBinaryZ(src[i * 4 + 2]);
                        v.SetBinaryW(src[i * 4 + 3]);
                        dst.Add(v);
                    }
                    break;
                case PackFormat.V4_16:
                    for (int i = 0; i < src.Count / 2; ++i)
                    {
                        Vector4 v = new Vector4();
                        UInt32 w1 = src[i * 2] & 0x0000FFFF;
                        UInt32 w2 = (src[i * 2] & 0xFFFF0000) >> 16;
                        UInt32 w3 = src[i * 2 + 1] & 0x0000FFFF;
                        UInt32 w4 = (src[i * 2 + 1] & 0xFFFF0000) >> 16;
                        w1 = ((w1 & 0x8000) != 0) ? w1 & 0xFFFF0000 : w1;
                        w2 = ((w2 & 0x8000) != 0) ? w2 & 0xFFFF0000 : w2;
                        w3 = ((w3 & 0x8000) != 0) ? w3 & 0xFFFF0000 : w3;
                        w4 = ((w4 & 0x8000) != 0) ? w4 & 0xFFFF0000 : w4;
                        v.SetBinaryX(w1);
                        v.SetBinaryY(w2);
                        v.SetBinaryZ(w3);
                        v.SetBinaryW(w4);
                        dst.Add(v);
                    }
                    break;
                case PackFormat.V4_8:
                    for (int i = 0; i < src.Count; ++i)
                    {
                        Vector4 v = new Vector4();
                        UInt32 w1 = src[i] & 0x000000FF;
                        UInt32 w2 = (src[i] & 0x0000FF00) >> 8;
                        UInt32 w3 = (src[i] & 0x00FF0000) >> 16;
                        UInt32 w4 = (src[i] & 0xFF000000) >> 24;
                        w1 = ((w1 & 0x80) != 0) ? w1 & 0xFFFFFF00 : w1;
                        w2 = ((w2 & 0x80) != 0) ? w2 & 0xFFFFFF00 : w2;
                        w3 = ((w3 & 0x80) != 0) ? w3 & 0xFFFFFF00 : w3;
                        w4 = ((w4 & 0x80) != 0) ? w4 & 0xFFFFFF00 : w4;
                        v.X = (Single)(w1);
                        v.Y = (Single)(w2);
                        v.Z = (Single)(w3);
                        v.W = (Single)(w4);
                        dst.Add(v);
                    }
                    break;
                case PackFormat.V4_5:
                    for (int i = 0; i < src.Count / 2; ++i)
                    {
                        Color c1 = new Color();
                        Color c2 = new Color();
                        Color c3 = new Color();
                        UInt32 rgba1 = src[i * 2] & 0x0000FFFF;
                        UInt32 rgba2 = (src[i * 2] & 0xFFFF0000) >> 16;
                        UInt32 rgba3 = src[i * 2 + 1] & 0x0000FFFF;
                        c1.R = (Byte)(rgba1 & 0x1111);
                        c1.G = (Byte)((rgba1 & (0x1111 << 5)) >> 5);
                        c1.B = (Byte)((rgba1 & (0x1111 << 10)) >> 10);
                        c1.A = (Byte)((rgba1 & (0x1 << 15)) >> 15);
                        c2.R = (Byte)(rgba2 & 0x1111);
                        c2.G = (Byte)((rgba2 & (0x1111 << 5)) >> 5);
                        c2.B = (Byte)((rgba2 & (0x1111 << 10)) >> 10);
                        c2.A = (Byte)((rgba2 & (0x1 << 15)) >> 15);
                        c3.R = (Byte)(rgba3 & 0x1111);
                        c3.G = (Byte)((rgba3 & (0x1111 << 5)) >> 5);
                        c3.B = (Byte)((rgba3 & (0x1111 << 10)) >> 10);
                        c3.A = (Byte)((rgba3 & (0x1 << 15)) >> 15);
                        dst.Add(c1.GetVector());
                        dst.Add(c2.GetVector());
                        dst.Add(c3.GetVector());
                    }
                    break;
            }
        }
    }
    public enum PackFormat
    {
        S_32 =      0b0000,
        S_16 =      0b0001,
        S_8 =       0b0010,
        V2_32 =     0b0100,
        V2_16 =     0b0101,
        V2_8 =      0b0110,
        V3_32 =     0b1000,
        V3_16 =     0b1001,
        V3_8 =      0b1010,
        V4_32 =     0b1100,
        V4_16 =     0b1101,
        V4_8 =      0b1110,
        V4_5 =      0b1111,
    }
}
