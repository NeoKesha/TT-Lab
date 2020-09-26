using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class ScriptState : ITwinSerializable
    {
        public UInt16 Bitfield;
        public Int16 ScriptIndexOrSlot;
        public ScriptType1 Type1;
        public ScriptStateBody Body;

        internal ScriptState next;

        public int GetLength()
        {
            return 4 + (Type1 != null ? Type1.GetLength() : 0) + (Body != null ? Body.GetLength() : 0);
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

        public void Read(BinaryReader reader, int length, IList<ScriptState> scriptStates)
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
            var newBitfield = 0;
            if (Type1 != null)
            {
                newBitfield |= 0x4000;
            }
            if (next != null)
            {
                newBitfield |= 0x8000;
            }
            if (Body != null)
            {
                newBitfield |= 0x1F; // The values are different for any state that has a body, needs proper research
            }
            newBitfield |= Bitfield;
            writer.Write(newBitfield);
            writer.Write(ScriptIndexOrSlot);
            if (Type1 != null)
            {
                Type1.Write(writer);
            }
            if (next != null)
            {
                next.Write(writer);
            }
        }
    }
}
