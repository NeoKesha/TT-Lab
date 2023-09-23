using System;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Code
{
    public class PS2AnyScriptsSection : BaseTwinSection
    {
        public PS2AnyScriptsSection() : base()
        {
            defaultType = typeof(PS2BehaviourWrapper);
            idToClassDictionary[0] = typeof(PS2BehaviourStarter);
            idToClassDictionary[1] = typeof(PS2BehaviourGraph);
        }
        protected override UInt32 ProcessId(UInt32 id)
        {
            return id % 2;
        }
    }
}
