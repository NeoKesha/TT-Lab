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
        public ScriptState NextState;
        public ScriptStateBody Body;

        public bool HasNextState
        {
            get
            {
                return (Bitfield & 0x8000) != 0;
            }
            set
            {
                if (value)
                {
                    Bitfield |= 0x8000;
                    return;
                }
                Bitfield &= 0x7FFF;
            }
        }

        public bool HasType1
        {
            get
            {
                return (Bitfield & 0x4000) != 0;
            }
            set
            {
                if (value)
                {
                    Bitfield |= 0x4000;
                    return;
                }
                Bitfield &= 0xBFFF;
            }
        }

        public bool HasBody
        {
            get
            {
                return (Bitfield & 0x1F) != 0;
            }
        }

        public int GetLength()
        {
            return 4 + (HasType1 ? Type1.GetLength() : 0) + (HasNextState ? NextState.GetLength() : 0);
        }

        public void Read(BinaryReader reader, int length)
        {
            Bitfield = reader.ReadUInt16();
            ScriptIndexOrSlot = reader.ReadInt16();
            if (HasType1)
            {
                Type1 = new ScriptType1();
                Type1.Read(reader, length);
            }
            if (HasNextState)
            {
                NextState = new ScriptState();
                NextState.Read(reader, length);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Bitfield);
            writer.Write(ScriptIndexOrSlot);
            if (HasType1)
            {
                Type1.Write(writer);
            }
            if (HasNextState)
            {
                NextState.Write(writer);
            }
        }
    }
}
