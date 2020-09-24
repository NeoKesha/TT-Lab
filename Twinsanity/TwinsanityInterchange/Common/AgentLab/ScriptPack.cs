using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class ScriptPack : ITwinSerializable
    {
        public Int32 CommandsAmount;
        public ScriptCommand Command;

        public int GetLength()
        {
            return 4 + (CommandsAmount > 0 ? Command.GetLength() : 0);
        }

        public void Read(BinaryReader reader, int length)
        {
            CommandsAmount = reader.ReadInt32();
            if (CommandsAmount > 0)
            {
                Command = new ScriptCommand();
                Command.Read(reader, length);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(CommandsAmount);
            if (CommandsAmount > 0)
            {
                Command.Write(writer);
            }
        }
    }
}
