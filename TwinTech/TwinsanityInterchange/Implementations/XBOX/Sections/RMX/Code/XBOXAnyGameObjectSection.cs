using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.XBOX.Items.Code;
using Twinsanity.TwinsanityInterchange.Implementations.XBOX.Items.RMX.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.XBOX.Sections.RMX.Code
{
    public class XBOXAnyGameObjectSection : BaseTwinSection
    {
        public XBOXAnyGameObjectSection() : base()
        {
            defaultType = typeof(XBOXAnyObject);
        }
    }
}
