using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class TwinBehaviourCommandsSequence : BaseTwinItem
    {
        public Int32 Header { get; set; }
        public List<KeyValuePair<UInt16, TwinBehaviourCommandPack>> BehaviourPacks { get; set; }
        public List<TwinBehaviourCommand> Commands { get; set; }

        public TwinBehaviourCommandsSequence()
        {
            BehaviourPacks = new List<KeyValuePair<UInt16, TwinBehaviourCommandPack>>();
            Commands = new List<TwinBehaviourCommand>();
        }

        public override int GetLength()
        {
            return 4 + BehaviourPacks.Sum(pair => pair.Value.GetLength()) + BehaviourPacks.Count * Constants.SIZE_UINT16 + Commands.Sum(com => com.GetLength());
        }

        public override void Read(BinaryReader reader, int length)
        {
            Header = reader.ReadInt32();
            BehaviourPacks.Clear();
            var packs = (Byte)(Header >> 16 & 0xFF);
            for (var i = 0; i < packs; ++i)
            {
                var pack = new TwinBehaviourCommandPack();
                pack.Read(reader, length);
                var scriptId = reader.ReadUInt16();
                var pair = new KeyValuePair<UInt16, TwinBehaviourCommandPack>(scriptId, pack);
                BehaviourPacks.Add(pair);
            }
            Commands.Clear();
            var com = new TwinBehaviourCommand();
            Commands.Add(com);
            com.Read(reader, length, Commands);
        }

        public override void Write(BinaryWriter writer)
        {
            var newHeader = (Int32)(Header & 0xFF00FFFF) | BehaviourPacks.Count << 16;
            newHeader |= (Int32)(Header & 0xFF00FFFF);
            writer.Write(newHeader);
            foreach (var pair in BehaviourPacks)
            {
                pair.Value.Write(writer);
                writer.Write(pair.Key);
            }
            foreach (var com in Commands)
            {
                com.hasNext = !Commands.Last().Equals(com);
                com.Write(writer);
            }
        }

        public void WriteText(StreamWriter writer, Int32 tabs = 0)
        {
            StringUtils.WriteLineTabulated(writer, $"BehaviourCommandsSequence({Header}) {"{"}", tabs);
            foreach (var packPair in BehaviourPacks)
            {
                StringUtils.WriteLineTabulated(writer, $"Pack({packPair.Key}) {"{"}", tabs + 1);
                packPair.Value.WriteText(writer, tabs + 2);
                StringUtils.WriteLineTabulated(writer, "}", tabs + 1);
            }
            foreach (TwinBehaviourCommand cmd in Commands)
            {
                cmd.WriteText(writer, tabs + 1);
            }
            StringUtils.WriteLineTabulated(writer, "}", tabs);
        }
        public void ReadText(StreamReader reader)
        {
            String line = "";
            BehaviourPacks.Clear();
            Commands.Clear();
            while (!line.StartsWith("BehaviourCommandsSequence"))
            {
                line = reader.ReadLine().Trim();
            }
            Header = int.Parse(StringUtils.GetStringInBetween(line, "(", ")"));
            while (!line.EndsWith("{"))
            {
                line = reader.ReadLine().Trim();
            }
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.StartsWith("Pack"))
                {
                    UInt16 arg = ushort.Parse(StringUtils.GetStringInBetween(line, "(", ")"));
                    TwinBehaviourCommandPack pack = new TwinBehaviourCommandPack();
                    BehaviourPacks.Add(new KeyValuePair<UInt16, TwinBehaviourCommandPack>(arg, pack));
                    while (!line.EndsWith("{"))
                    {
                        line = reader.ReadLine().Trim();
                    }
                    pack.ReadText(reader);
                    while (!line.EndsWith("}"))
                    {
                        line = reader.ReadLine().Trim();
                    }
                }
                else
                {
                    TwinBehaviourCommand cmd = new TwinBehaviourCommand();
                    Commands.Add(cmd);
                    cmd.ReadText(line);
                }
            }
        }
        public override String ToString()
        {
            using MemoryStream stream = new();
            StreamWriter writer = new(stream);
            StreamReader reader = new(stream);
            WriteText(writer);
            writer.Flush();
            stream.Position = 0;
            return reader.ReadToEnd();
        }

        public override String GetName()
        {
            return $"Behaviour Commands Sequence {id:X}";
        }
    }
}
