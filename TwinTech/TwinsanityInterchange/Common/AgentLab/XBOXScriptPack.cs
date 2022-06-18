using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    // Only overrides for XBOX command size mapper
    public class XBOXScriptPack : ScriptPack
    {
        public List<XBOXScriptCommand> XCommands;

        public XBOXScriptPack()
        {
            XCommands = new List<XBOXScriptCommand>();
        }

        public override int GetLength()
        {
            return 4 + XCommands.Sum(com => com.GetLength());
        }

        public override void Read(BinaryReader reader, int length)
        {
            var amt = reader.ReadInt32();
            XCommands.Clear();
            for (var i = 0; i < amt; ++i)
            {
                var com = new XBOXScriptCommand();
                XCommands.Add(com);
                com.Read(reader, length);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(XCommands.Count);
            foreach (var com in XCommands)
            {
                com.hasNext = !(XCommands.Last().Equals(com));
                com.Write(writer);
            }
        }
        public override void WriteText(StreamWriter writer, Int32 tabs = 0)
        {
            foreach (XBOXScriptCommand cmd in XCommands)
            {
                cmd.WriteText(writer, tabs);
            }
        }
        public override void ReadText(StreamReader reader)
        {
            String line = "";
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                XBOXScriptCommand cmd = new XBOXScriptCommand();
                cmd.ReadText(line);
                XCommands.Add(cmd);
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
