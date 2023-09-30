using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.Xbox.Items.Graphics;

namespace Twinsanity.TwinsanityInterchange.Implementations.Xbox.Sections.Graphics
{
    public class XboxAnyMaterialsSection : BaseTwinSection
    {
        public XboxAnyMaterialsSection() : base()
        {
            defaultType = typeof(XboxAnyMaterial);
        }
    }
}
