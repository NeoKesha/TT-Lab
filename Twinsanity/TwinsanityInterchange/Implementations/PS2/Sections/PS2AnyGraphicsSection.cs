using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections
{
    public class PS2AnyGraphicsSection : BaseTwinSection
    {
        public PS2AnyGraphicsSection() : base()
        {
            defaultType = typeof(BaseTwinItem);
        }
    }
}
