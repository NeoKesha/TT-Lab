using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class ScriptState : ITwinSerializable
    {
        public UInt16 Bitfield;
        public Int16 ScriptIndexOrSlot;
        public ScriptType1 Type1;
        public List<ScriptStateBody> Bodies;

        internal bool hasNext;

        public ScriptState()
        {
            Bodies = new List<ScriptStateBody>(0x1F);
        }

        public int GetLength()
        {
            return 4 + (Type1 != null ? Type1.GetLength() : 0) + Bodies.Sum(body => body.GetLength());
        }

        public void Read(BinaryReader reader, int length)
        {
            Bitfield = reader.ReadUInt16();
            ScriptIndexOrSlot = reader.ReadInt16();
            if ((Bitfield & 0x4000) != 0)
            {
                Type1 = new ScriptType1();
                Type1.Read(reader, length);
            }
        }

        public virtual void Read(BinaryReader reader, int length, IList<ScriptState> scriptStates)
        {
            Read(reader, length);
            var hasNext = (Bitfield & 0x8000) != 0;
            if (hasNext)
            {
                var state = new ScriptState();
                scriptStates.Add(state);
                state.Read(reader, length, scriptStates);
            }
        }

        public void Write(BinaryWriter writer)
        {
            UInt16 newBitfield = (UInt16)Bodies.Count;
            if (Type1 != null)
            {
                newBitfield |= 0x4000;
            }
            if (hasNext)
            {
                newBitfield |= 0x8000;
            }
            newBitfield |= Bitfield;
            writer.Write(newBitfield);
            writer.Write(ScriptIndexOrSlot);
            if (Type1 != null)
            {
                Type1.Write(writer);
            }
        }
        public void WriteText(StreamWriter writer, Int32 i, Int32 tabs = 0)
        {
            if (ScriptIndexOrSlot != -1)
            {
                StringUtils.WriteLineTabulated(writer, $"State_{i}({ScriptIndexOrSlot}) {"{"}", tabs);
                writer.WriteLine();
            } else
            {
                StringUtils.WriteLineTabulated(writer, $"State_{i}() {"{"}", tabs);
                writer.WriteLine();
            }
            if (Type1 != null)
            {
                Type1.WriteText(writer, tabs + 1);
            }
            foreach(var body in Bodies)
            {
                body.WriteText(writer, tabs + 1);
            }
            StringUtils.WriteLineTabulated(writer, "}", tabs);
            writer.WriteLine();
        }

        public virtual void ReadText(StreamReader reader)
        {
            String line = "";
            Type1 = null;
            Bodies.Clear();
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.StartsWith("Head"))
                {
                    Type1 = new ScriptType1();
                    while (!line.EndsWith("{"))
                    {
                        line = reader.ReadLine().Trim();
                    }
                    Type1.ReadText(reader);
                }
                if (line.StartsWith("Body"))
                {
                    ScriptStateBody body = new ScriptStateBody();
                    while (!line.EndsWith("{"))
                    {
                        line = reader.ReadLine().Trim();
                    }
                    body.ReadText(reader);
                    Bodies.Add(body);
                }
            }
        }
    }
}
