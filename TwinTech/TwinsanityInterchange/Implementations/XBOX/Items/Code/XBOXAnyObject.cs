using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.XBOX.Items.Code
{
    // Only overrides for XBOX command size mapper
    public class XBOXAnyObject : PS2AnyObject
    {
        public override void ReadScriptPack(BinaryReader reader, Int32 length)
        {
            ScriptPack = new XBOXScriptPack();
            ScriptPack.Read(reader, length);
        }
    }
}
