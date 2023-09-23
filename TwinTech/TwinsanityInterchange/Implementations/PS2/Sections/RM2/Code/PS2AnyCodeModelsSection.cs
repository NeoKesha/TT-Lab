using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Code
{
    public class PS2AnyCodeModelsSection : BaseTwinSection
    {
        public PS2AnyCodeModelsSection() : base()
        {
            defaultType = typeof(PS2AnyBehaviourCommandsSequence);
        }
    }
}
