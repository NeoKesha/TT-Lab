using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    // Only overrides for XBOX command size mapper
    public class XBOXScriptState : ScriptState
    {
        public override void Read(BinaryReader reader, int length, IList<ScriptState> scriptStates)
        {
            Read(reader, length);
            var hasNext = (Bitfield & 0x8000) != 0;
            if (hasNext)
            {
                var state = new XBOXScriptState();
                scriptStates.Add(state);
                state.Read(reader, length, scriptStates);
            }
        }
        public override void ReadText(StreamReader reader)
        {
            String line = "";
            Type1 = null;
            Bodies.Clear();
            while (!line.EndsWith("}"))
            {
                line = reader.ReadLine().Trim();
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.StartsWith("Head"))
                {
                    Type1 = new ScriptType1();
                    while (!line.EndsWith("{"))
                    {
                        line = reader.ReadLine().Trim();
                    }
                    Type1.ReadText(reader);
                }
                if (line.StartsWith("Body"))
                {
                    XBOXScriptStateBody body = new XBOXScriptStateBody();
                    while (!line.EndsWith("{"))
                    {
                        line = reader.ReadLine().Trim();
                    }
                    body.ReadText(reader);
                    Bodies.Add(body);
                }
            }
        }
    }
}
