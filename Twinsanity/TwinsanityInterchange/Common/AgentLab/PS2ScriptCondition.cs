using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class PS2ScriptCondition : ITwinSerializable
    {
        public Int32 Bitfield;
        public Single Modifier;
        public Single ReturnCheck;
        public Single ConditionPower;

        public UInt16 ConditionIndex
        {
            get
            {
                return (UInt16)(Bitfield & 0xFFFF);
            }
            set
            {
                Bitfield = (Int32)(Bitfield & 0xFFFF0000) | (value & 0xFFFF);
            }
        }

        public UInt16 Parameter
        {
            get
            {
                return (UInt16)((Bitfield & 0xFFFF0000) >> 17);
            }
            set
            {
                Bitfield = (Bitfield & 0x1FFFF) | (Int32)((value << 17) & 0xFFFE0000);
            }
        }

        public bool NotGate
        {
            get
            {
                return (Bitfield & 0x10000) != 0;
            }
            set
            {
                if (value)
                {
                    Bitfield |= 0x10000;
                    return;
                }
                Bitfield = (Int32)(Bitfield & 0xFFFEFFFF);
            }
        }

        public int GetLength()
        {
            return 16;
        }

        public void Read(BinaryReader reader, int length)
        {
            Bitfield = reader.ReadInt32();
            Modifier = reader.ReadSingle();
            ReturnCheck = reader.ReadSingle();
            ConditionPower = reader.ReadSingle();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Bitfield);
            writer.Write(Modifier);
            writer.Write(ReturnCheck);
            writer.Write(ConditionPower);
        }
    }
}
