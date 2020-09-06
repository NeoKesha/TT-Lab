using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections
{
    public class PS2AnyBlendSkinsSection : BaseTwinSection
    {
        public PS2AnyBlendSkinsSection() : base()
        {
            defaultType = typeof(BaseTwinItem);
        }
}
}
