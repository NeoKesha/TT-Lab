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
                } 
                else
                {
                    switch (vif.OP)
                    {
                        case VIFCodeEnum.NOP:

                            break;
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
                    }
                }
            }
        }
    }
}
