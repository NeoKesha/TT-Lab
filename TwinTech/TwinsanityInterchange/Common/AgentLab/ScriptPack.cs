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
        public List<ScriptCommand> Commands;

        public ScriptPack()
        {
            Commands = new List<ScriptCommand>();
        }

        public int GetLength()
        {
            return 4 + Commands.Sum(com => com.GetLength());
        }

        public void Read(BinaryReader reader, int length)
        {
            var amt = reader.ReadInt32();
            Commands.Clear();
            for (var i = 0; i < amt; ++i)
            {
                var com = new ScriptCommand();
                Commands.Add(com);
                com.Read(reader, length);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Commands.Count);
            foreach (var com in Commands)
            {
                com.hasNext = !(Commands.Last().Equals(com));
                com.Write(writer);
            }
        }
    }
}
