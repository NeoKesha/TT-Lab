using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code.AgentLab
{
    public class PS2BehaviourState : ITwinBehaviourState
    {
        public UInt16 Bitfield { get; set; }
        public Int16 ScriptIndexOrSlot { get; set; }
        public TwinBehaviourControlPacket ControlPacket { get; set; }
        public List<ITwinBehaviourStateBody> Bodies { get; set; }

        bool ITwinBehaviourState.HasNext { get; set; }

        public PS2BehaviourState()
        {
            Bodies = new List<ITwinBehaviourStateBody>(0x1F);
        }

        public int GetLength()
        {
            return 4 + (ControlPacket != null ? ControlPacket.GetLength() : 0) + Bodies.Sum(body => body.GetLength());
        }

        public void Read(BinaryReader reader, int length)
        {
            Bitfield = reader.ReadUInt16();
            ScriptIndexOrSlot = reader.ReadInt16();
            if ((Bitfield & 0x4000) != 0)
            {
                ControlPacket = new TwinBehaviourControlPacket();
                ControlPacket.Read(reader, length);
            }
        }

        public void Read(BinaryReader reader, int length, IList<ITwinBehaviourState> scriptStates)
        {
            Read(reader, length);
            var hasNext = (Bitfield & 0x8000) != 0;
            if (hasNext)
            {
                var state = new PS2BehaviourState();
                scriptStates.Add(state);
                state.Read(reader, length, scriptStates);
            }
        }

        public void Write(BinaryWriter writer)
        {
            UInt16 newBitfield = (UInt16)Bodies.Count;
            if (ControlPacket != null)
            {
                newBitfield |= 0x4000;
            }
            ITwinBehaviourState downCast = this;
            if (downCast.HasNext)
            {
                newBitfield |= 0x8000;
            }
            newBitfield |= Bitfield;
            writer.Write(newBitfield);
            writer.Write(ScriptIndexOrSlot);
            ControlPacket?.Write(writer);
        }
        public void WriteText(StreamWriter writer, Int32 i, Int32 tabs = 0)
        {
            if (ScriptIndexOrSlot != -1)
            {
                StringUtils.WriteLineTabulated(writer, $"State_{i}({ScriptIndexOrSlot}) {"{"}", tabs);
                writer.WriteLine();
            }
            else
            {
                StringUtils.WriteLineTabulated(writer, $"State_{i}() {"{"}", tabs);
                writer.WriteLine();
            }
            if (ControlPacket != null)
            {
                ControlPacket.WriteText(writer, tabs + 1);
            }
            foreach (var body in Bodies)
            {
                body.WriteText(writer, tabs + 1);
            }
            StringUtils.WriteLineTabulated(writer, "}", tabs);
            writer.WriteLine();
        }

        public void ReadText(StreamReader reader)
        {
            String line = "";
            ControlPacket = null;
            Bodies.Clear();
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.StartsWith("Head"))
                {
                    ControlPacket = new TwinBehaviourControlPacket();
                    while (!line.EndsWith("{"))
                    {
                        line = reader.ReadLine().Trim();
                    }
                    ControlPacket.ReadText(reader);
                }
                if (line.StartsWith("Body"))
                {
                    PS2BehaviourStateBody body = new();
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
