using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class ScriptStateBody : ITwinSerializable
    {
        public UInt32 Bitfield;
        public Int32 JumpToState;
        public ScriptCondition Condition;
        public ScriptCommand Command;
        public ScriptStateBody NextStateBody;

        public bool HasStateJump
        {
            get
            {
                return (Bitfield & 0x400) != 0;
            }
            set
            {
                if (value)
                {
                    Bitfield |= 0x400;
                    return;
                }
                Bitfield &= 0xFFFFFBFF;
            }
        }

        public bool HasCondition
        {
            get
            {
                return (Bitfield & 0x200) != 0;
            }
            set
            {
                if (value)
                {
                    Bitfield |= 0x200;
                    return;
                }
                Bitfield &= 0xFFFFFDFF;
            }
        }

        public bool HasCommand
        {
            get
            {
                return (Bitfield & 0xFF) != 0;
            }
        }

        public bool HasNextBody
        {
            get
            {
                return (Bitfield & 0x800) != 0;
            }
            set
            {
                if (value)
                {
                    Bitfield |= 0x800;
                    return;
                }
                Bitfield &= 0xFFFFF7FF;
            }
        }

        public int GetLength()
        {
            return 4 + (HasStateJump ? 4 : 0) + (HasCommand ? Command.GetLength() : 0) +
                (HasCondition ? Condition.GetLength() : 0) +
                (HasNextBody ? NextStateBody.GetLength() : 0);
        }

        public void Read(BinaryReader reader, int length)
        {
            Bitfield = reader.ReadUInt32();
            if (HasStateJump)
            {
                JumpToState = reader.ReadInt32();
            }
            if (HasCondition)
            {
                Condition = new ScriptCondition();
                Condition.Read(reader, length);
            }
            if (HasCommand)
            {
                Command = new ScriptCommand();
                Command.Read(reader, length);
            }
            if (HasNextBody)
            {
                NextStateBody = new ScriptStateBody();
                NextStateBody.Read(reader, length);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Bitfield);
            if (HasStateJump)
            {
                writer.Write(JumpToState);
            }
            if (HasCondition)
            {
                Condition.Write(writer);
            }
            if (HasCommand)
            {
                Command.Write(writer);
            }
            if (HasNextBody)
            {
                NextStateBody.Write(writer);
            }
        }
    }
}
