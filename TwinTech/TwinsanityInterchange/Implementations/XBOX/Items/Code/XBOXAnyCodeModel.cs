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
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.XBOX.Items.RMX.Code
{
    // Only overrides for XBOX command size mapper
    public class XBOXAnyCodeModel : PS2AnyCodeModel
    {
        public override void Read(BinaryReader reader, int length)
        {
            Header = reader.ReadInt32();
            ScriptPacks.Clear();
            var packs = (Byte)(Header >> 16 & 0xFF);
            for (var i = 0; i < packs; ++i)
            {
                var pack = new XBOXScriptPack();
                pack.Read(reader, length);
                var scriptId = reader.ReadUInt16();
                var pair = new KeyValuePair<UInt16, ScriptPack>(scriptId, pack);
                ScriptPacks.Add(pair);
            }
            Commands.Clear();
            var com = new XBOXScriptCommand();
            Commands.Add(com);
            com.Read(reader, length, Commands);
        }

        public override void ReadText(StreamReader reader)
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
                    XBOXScriptPack pack = new XBOXScriptPack();
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
                    XBOXScriptCommand cmd = new XBOXScriptCommand();
                    Commands.Add(cmd);
                    cmd.ReadText(line);
                }
            }
        }
    }
}
