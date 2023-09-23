using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class BehaviourCommandPack : ITwinSerializable
    {
        public List<BehaviourCommand> Commands;

        public BehaviourCommandPack()
        {
            Commands = new List<BehaviourCommand>();
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
                var com = new BehaviourCommand();
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
        public void WriteText(StreamWriter writer, Int32 tabs = 0)
        {
            foreach (BehaviourCommand cmd in Commands)
            {
                cmd.WriteText(writer, tabs);
            }
        }
        public void ReadText(StreamReader reader)
        {
            String line = "";
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                BehaviourCommand cmd = new BehaviourCommand();
                cmd.ReadText(line);
                Commands.Add(cmd);
            }
        }
        public override String ToString()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                StreamReader reader = new StreamReader(stream);
                WriteText(writer);
                writer.Flush();
                stream.Position = 0;
                return reader.ReadToEnd();
            }
        }
    }
}
