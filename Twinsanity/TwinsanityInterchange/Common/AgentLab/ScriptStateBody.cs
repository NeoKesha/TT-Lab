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
        public Boolean HasStateJump;
        public Int32 JumpToState;
        public ScriptCondition Condition;
        public List<ScriptCommand> Commands;

        internal bool hasNext;

        public ScriptStateBody()
        {
            Commands = new List<ScriptCommand>();
        }

        public int GetLength()
        {
            return 4 + (HasStateJump ? 4 : 0) + Commands.Sum(command => command.GetLength()) +
                (Condition != null ? Condition.GetLength() : 0);
        }

        public void Read(BinaryReader reader, int length)
        {
            Bitfield = reader.ReadUInt32();
            var hasStateJump = (Bitfield & 0x400) != 0;
            var hasCondition = (Bitfield & 0x200) != 0;
            var hasCommand = (Bitfield & 0xFF) != 0;
            HasStateJump = hasStateJump;
            if (HasStateJump)
            {
                JumpToState = reader.ReadInt32();
            }
            if (hasCondition)
            {
                Condition = new ScriptCondition();
                Condition.Read(reader, length);
            }
            Commands.Clear();
            if (hasCommand)
            {
                var com = new ScriptCommand();
                Commands.Add(com);
                com.Read(reader, length, Commands);
            }
        }

        public void Read(BinaryReader reader, int length, IList<ScriptStateBody> bodies)
        {
            Read(reader, length);
            var hasNext = (Bitfield & 0x800) != 0;
            if (hasNext)
            {
                var stateBody = new ScriptStateBody();
                bodies.Add(stateBody);
                stateBody.Read(reader, length, bodies);
            }
        }

        public void Write(BinaryWriter writer)
        {
            UInt32 newBitfield = (UInt32)Commands.Count;
            if (hasNext)
            {
                newBitfield |= 0x800;
            }
            if (HasStateJump)
            {
                newBitfield |= 0x400;
            }
            if (Condition != null)
            {
                newBitfield |= 0x200;
            }
            newBitfield |= (Bitfield & 0xFFFF0100);
            writer.Write(newBitfield);
            if (HasStateJump)
            {
                writer.Write(JumpToState);
            }
            if (Condition != null)
            {
                Condition.Write(writer);
            }
            if (Commands.Count > 0)
            {
                foreach (var com in Commands)
                {
                    com.hasNext = !(Commands.Last().Equals(com));
                    com.Write(writer);
                };
            }
        }
    }
}
