using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class BehaviourCondition : ITwinSerializable
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

        public void WriteText(StreamWriter writer, Int32 tabs = 0)
        {
            AgentLabDefs defs = PS2BehaviourGraph.GetAgentLabDefs();
            StringUtils.WriteLineTabulated(writer, $"Condition {MapIndex(ConditionIndex, defs)}({Parameter}) {"{"}", tabs);
            StringUtils.WriteLineTabulated(writer, $"({Modifier.ToString(CultureInfo.InvariantCulture)}, " +
                $"{ReturnCheck.ToString(CultureInfo.InvariantCulture)}, " +
                $"{ConditionPowerMultiplier.ToString(CultureInfo.InvariantCulture)}) == {(NotGate ? "false" : "true")}", tabs + 1);
            StringUtils.WriteLineTabulated(writer, "}", tabs);
        }

        public void ReadText(StreamReader reader, String condName)
        {
            String line = "";
            if (condName.StartsWith("ById_"))
            {
                ConditionIndex = UInt16.Parse(StringUtils.GetStringAfter(condName, "ById_"));
            }
            else
            {
                AgentLabDefs defs = PS2BehaviourGraph.GetAgentLabDefs();
                ConditionIndex = UInt16.Parse((defs.condition_map.FirstOrDefault(x => x.Value == condName).Key));
            }
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.StartsWith("("))
                {
                    String right = StringUtils.GetStringAfter(line, "==").Trim();
                    if (right == "true")
                    {
                        NotGate = false;
                    }
                    else
                    {
                        NotGate = true;
                    }
                    String[] floats = StringUtils.GetStringInBetween(line, "(", ")").Split(',');
                    Modifier = Single.Parse(floats[0], CultureInfo.InvariantCulture);
                    ReturnCheck = Single.Parse(floats[1], CultureInfo.InvariantCulture);
                    ConditionPowerMultiplier = Single.Parse(floats[2], CultureInfo.InvariantCulture);
                }
            }
        }

        private string MapIndex(UInt32 index, AgentLabDefs defs)
        {
            string str_index = index.ToString();
            if (defs.condition_map.ContainsKey(str_index))
            {
                return defs.condition_map[str_index];
            }
            else
            {
                return $"ById_{str_index}";
            }
        }
    }
}
