using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    // Only overrides for XBOX command size mapper
    public class XBOXScriptStateBody : ScriptStateBody
    {
        public override void Read(BinaryReader reader, int length)
        {
            Bitfield = reader.ReadUInt32();
            var hasStateJump = (Bitfield & 0x400) != 0;
            var hasCondition = (Bitfield & 0x200) != 0;
            var commandsAmt = (Bitfield & 0xFF);
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
            for (var i = 0; i < commandsAmt; ++i)
            {
                var com = new XBOXScriptCommand();
                Commands.Add(com);
                com.Read(reader, length);
            }
        }

        public override void ReadText(StreamReader reader)
        {
            String line = "";
            Condition = null;
            Commands.Clear();
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.StartsWith("next_state "))
                {
                    JumpToState = Int32.Parse(StringUtils.GetStringAfter(line, "="));
                }
                else if (line.StartsWith("Condition"))
                {
                    Condition = new ScriptCondition();
                    String condName = StringUtils.GetStringInBetween(line, "Condition ", "(").Trim();
                    Condition.Parameter = UInt16.Parse(StringUtils.GetStringInBetween(line, "(", ")"));
                    while (!line.EndsWith("{"))
                    {
                        line = reader.ReadLine().Trim();
                    }
                    Condition.ReadText(reader, condName);
                }
                else if (!line.StartsWith("}"))
                {
                    XBOXScriptCommand cmd = new XBOXScriptCommand();
                    cmd.ReadText(line);
                    Commands.Add(cmd);
                }
            }
        }
    }
}
