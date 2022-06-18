using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Implementations.XBOX.Items.RMX.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.XBOX.Sections.RMX.Code
{
    public class XBOXAnyCodeModelsSection : BaseTwinSection
    {
        public XBOXAnyCodeModelsSection() : base()
        {
            defaultType = typeof(XBOXAnyCodeModel);
        }
    }
}
