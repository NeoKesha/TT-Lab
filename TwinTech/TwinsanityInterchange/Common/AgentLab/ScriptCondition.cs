using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class ScriptCondition : ITwinSerializable
    {
        public Int32 Bitfield;
        public UInt16 ConditionIndex;
        public UInt16 Parameter;
        public Boolean NotGate;
        public Single Modifier;
        public Single ReturnCheck;
        public Single ConditionPowerMultiplier;

        public Int32 GetLength()
        {
            return 16;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            Bitfield = reader.ReadInt32();
            ConditionIndex = (UInt16)(Bitfield & 0xFFFF);
            Parameter = (UInt16)((Bitfield & 0xFFFF0000) >> 17);
            NotGate = (Bitfield & 0x10000) != 0;
            Modifier = reader.ReadSingle();
            ReturnCheck = reader.ReadSingle();
            ConditionPowerMultiplier = reader.ReadSingle();
        }

        public void Write(BinaryWriter writer)
        {
            Int32 newBitfield = ConditionIndex;
            newBitfield = (newBitfield & 0x1FFFF) | (Int32)((Parameter << 17) & 0xFFFE0000);
            if (NotGate)
            {
                newBitfield |= 0x10000;
            }
            newBitfield |= (Int32)(Bitfield & 0xFFFE0000);
            writer.Write(newBitfield);
            writer.Write(Modifier);
            writer.Write(ReturnCheck);
            writer.Write(ConditionPowerMultiplier);
        }

        public void WriteText(StreamWriter writer)
        {
            writer.WriteLine($"            Condition ({ConditionIndex}, {Parameter}) {"{"}");
            writer.WriteLine($"                ({Modifier}, {ReturnCheck}, {ConditionPowerMultiplier}) == {(NotGate?"false":"true")}");
            writer.WriteLine($"            {"}"}");
        }

        public void ReadText(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
