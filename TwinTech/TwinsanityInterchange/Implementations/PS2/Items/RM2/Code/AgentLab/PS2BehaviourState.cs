using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code.AgentLab
{
    public class PS2BehaviourState : ITwinBehaviourState
    {
        public UInt16 Bitfield { get; set; }
        public Int16 BehaviourIndexOrSlot { get; set; }
        public Boolean SkipsFirstStateBody { get; set; }
        public Boolean UsesObjectSlot { get; set; }
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
            SkipsFirstStateBody = (Bitfield & 0x400) == 1;
            UsesObjectSlot = (Bitfield & 0x1000) == 1;
            BehaviourIndexOrSlot = reader.ReadInt16();
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

        private const UInt16 KnownFlagsMask = 0x8000 | 0x4000 | 0x1000 | 0x400;
        public void Write(BinaryWriter writer)
        {
            Bitfield &= unchecked((UInt16)~KnownFlagsMask);
            UInt16 newBitfield = (UInt16)(Bodies.Count & 0x1F);
            if (SkipsFirstStateBody)
            {
                newBitfield |= 0x400;
            }
            if (UsesObjectSlot)
            {
                newBitfield |= 0x1000;
            }
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
            writer.Write(BehaviourIndexOrSlot);
            ControlPacket?.Write(writer);
        }
        public void WriteText(StreamWriter writer, Int32 i, Int32 tabs = 0)
        {
            if (BehaviourIndexOrSlot != -1)
            {
                StringUtils.WriteLineTabulated(writer, $"State_{i}({BehaviourIndexOrSlot}) {"{"}", tabs);
                writer.WriteLine();
            }
            else
            {
                StringUtils.WriteLineTabulated(writer, $"State_{i}() {"{"}", tabs);
                writer.WriteLine();
            }
            ControlPacket?.WriteText(writer, tabs + 1);
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
                if (line.StartsWith("ControlPacket"))
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
