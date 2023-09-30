using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.Base;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Code
{
    public class PS2AnyBehaviourCommandsSequencesSection : BaseTwinSection
    {
        public PS2AnyBehaviourCommandsSequencesSection() : base()
        {
            defaultType = typeof(TwinBehaviourCommandsSequence);
        }
    }
}
