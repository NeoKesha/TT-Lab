using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.Base;

namespace Twinsanity.TwinsanityInterchange.Implementations.Xbox.Sections.RMX.Code
{
    public class XboxAnyBehaviourCommandsSequencesSection : BaseTwinSection
    {
        public XboxAnyBehaviourCommandsSequencesSection() : base()
        {
            defaultType = typeof(TwinBehaviourCommandsSequence);
        }
    }
}
