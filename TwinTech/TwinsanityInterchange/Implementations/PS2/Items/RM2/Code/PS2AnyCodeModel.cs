using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code
{
    public class PS2AnyCodeModel : BaseTwinItem, ITwinCodeModel
    {
        public Int32 Header;
        public List<KeyValuePair<UInt16, ScriptPack>> ScriptPacks;
        public List<ScriptCommand> Commands;

        public PS2AnyCodeModel()
        {
            ScriptPacks = new List<KeyValuePair<UInt16, ScriptPack>>();
            Commands = new List<ScriptCommand>();
        }

        public override int GetLength()
        {
            return 4 + ScriptPacks.Sum(pair => pair.Value.GetLength()) + ScriptPacks.Count * Constants.SIZE_UINT16 + Commands.Sum(com => com.GetLength());
        }

        public override void Read(BinaryReader reader, int length)
        {
            Header = reader.ReadInt32();
            ScriptPacks.Clear();
            var packs = (Byte)(Header >> 16 & 0xFF);
            for (var i = 0; i < packs; ++i)
            {
                var pack = new ScriptPack();
                pack.Read(reader, length);
                var scriptId = reader.ReadUInt16();
                var pair = new KeyValuePair<UInt16, ScriptPack>(scriptId, pack);
                ScriptPacks.Add(pair);
            }
            Commands.Clear();
            var com = new ScriptCommand();
            Commands.Add(com);
            com.Read(reader, length, Commands);
        }

        public override void Write(BinaryWriter writer)
        {
            var newHeader = (Int32)(Header & 0xFF00FFFF) | (ScriptPacks.Count << 16);
            newHeader |= (Int32)(Header & 0xFF00FFFF);
            writer.Write(newHeader);
            foreach (var pair in ScriptPacks)
            {
                pair.Value.Write(writer);
                writer.Write(pair.Key);
            }
            foreach (var com in Commands)
            {
                com.hasNext = !(Commands.Last().Equals(com));
                com.Write(writer);
            }
        }

        public void WriteText(StreamWriter writer, Int32 tabs = 0)
        {
            StringUtils.WriteLineTabulated(writer, $"CodeModel({Header}) {"{"}", tabs);
            foreach (var packPair in ScriptPacks)
            {
                StringUtils.WriteLineTabulated(writer, $"Pack({packPair.Key}) {"{"}", tabs + 1);
                packPair.Value.WriteText(writer, tabs + 2);
                StringUtils.WriteLineTabulated(writer, "}", tabs + 1);
            }
            foreach (ScriptCommand cmd in Commands)
            {
                cmd.WriteText(writer, tabs + 1);
            }
            StringUtils.WriteLineTabulated(writer, "}", tabs);
        }
        public virtual void ReadText(StreamReader reader)
        {
            String line = "";
            ScriptPacks.Clear();
            Commands.Clear();
            while (!line.StartsWith("CodeModel"))
            {
                line = reader.ReadLine().Trim();
            }
            Header = Int32.Parse(StringUtils.GetStringInBetween(line, "(", ")"));
            while (!line.EndsWith("{"))
            {
                line = reader.ReadLine().Trim();
            }
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.StartsWith("Pack"))
                {
                    UInt16 arg = UInt16.Parse(StringUtils.GetStringInBetween(line, "(", ")"));
                    ScriptPack pack = new ScriptPack();
                    ScriptPacks.Add(new KeyValuePair<UInt16, ScriptPack>(arg, pack));
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
                    ScriptCommand cmd = new ScriptCommand();
                    Commands.Add(cmd);
                    cmd.ReadText(line);
                }
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

        public override String GetName()
        {
            return $"Code Model {id:X}";
        }
    }
}
