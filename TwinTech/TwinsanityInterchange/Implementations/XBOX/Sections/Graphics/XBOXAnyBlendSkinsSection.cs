using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.XBOX.Items.Graphics;

namespace Twinsanity.TwinsanityInterchange.Implementations.XBOX.Sections.Graphics
{
    public class XBOXAnyBlendSkinsSection : BaseTwinSection
    {
        public XBOXAnyBlendSkinsSection() : base()
        {
            defaultType = typeof(XBOXAnyBlendSkin);
        }
    }
}
