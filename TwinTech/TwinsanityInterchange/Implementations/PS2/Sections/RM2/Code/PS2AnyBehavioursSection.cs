using System;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.Base;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Code
{
    public class PS2AnyBehavioursSection : BaseTwinSection
    {
        public PS2AnyBehavioursSection() : base()
        {
            defaultType = typeof(TwinBehaviourWrapper);
            idToClassDictionary[0] = typeof(TwinBehaviourStarter);
            idToClassDictionary[1] = typeof(TwinBehaviourGraph);
        }
        protected override UInt32 ProcessId(UInt32 id)
        {
            return id % 2;
        }
    }
}
