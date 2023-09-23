using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.Graphics
{
    public class PS2AnySkinsSection : BaseTwinSection
    {
        public PS2AnySkinsSection() : base()
        {
            defaultType = typeof(PS2AnySkin);
        }
    }
}
