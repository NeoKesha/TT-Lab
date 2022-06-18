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
    public class XBOXMainScript : PS2MainScript
    {
        public override void Read(BinaryReader reader, int length)
        {
            base.ReadBase(reader, length);
            var nameLen = reader.ReadInt32();
            Name = new String(reader.ReadChars(nameLen));
            var statesAmt = reader.ReadInt32();
            UnkInt = reader.ReadInt32();
            ScriptStates.Clear();
            for (int i = 0; i < statesAmt; ++i)
            {
                XBOXScriptState state = new XBOXScriptState();
                state.Read(reader, length);
                ScriptStates.Add(state);
            }
            foreach (var state in ScriptStates)
            {
                state.Bodies.Clear();
                var bodiesAmt = (state.Bitfield & 0x1F);
                for (var i = 0; i < bodiesAmt; ++i)
                {
                    var stateBody = new XBOXScriptStateBody();
                    state.Bodies.Add(stateBody);
                    stateBody.Read(reader, length);
                }
            }
        }

        public override void ReadText(StreamReader reader)
        {
            String line = "";
            ScriptStates.Clear();
            while (!line.StartsWith("Script"))
            {
                line = reader.ReadLine().Trim();
            }
            Name = StringUtils.GetStringInBetween(line, "(", ")");
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
                if (line.StartsWith("bitfield"))
                {
                    UnkInt = Int32.Parse(StringUtils.GetStringAfter(line, "="));
                }
                if (line.StartsWith("State_"))
                {
                    String arg = StringUtils.GetStringInBetween(line, "(", ")");
                    Int32 index = Int32.Parse(StringUtils.GetStringInBetween(line, "_", "("));
                    while (ScriptStates.Count <= index)
                    {
                        ScriptStates.Add(new XBOXScriptState());
                    }
                    ScriptState state = ScriptStates[index];
                    if (String.IsNullOrWhiteSpace(arg))
                    {
                        state.ScriptIndexOrSlot = -1;
                    }
                    else
                    {
                        state.ScriptIndexOrSlot = Int16.Parse(StringUtils.GetStringInBetween(line, "(", ")"));
                    }
                    while (!line.EndsWith("{"))
                    {
                        line = reader.ReadLine().Trim();
                    }
                    state.ReadText(reader);

                }
            }
        }
    }
}
