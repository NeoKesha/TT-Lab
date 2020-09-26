using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Code;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.Code
{
    public class PS2AnyScriptsSection : BaseTwinSection
    {
        public PS2AnyScriptsSection() : base()
        {
            defaultType = typeof(PS2AnyScript);
            idToClassDictionary[0] = typeof(PS2HeaderScript);
            idToClassDictionary[1] = typeof(PS2MainScript);
        }
        protected override UInt32 ProcessId(UInt32 id)
        {
            return id % 2;
        }
    }
}
