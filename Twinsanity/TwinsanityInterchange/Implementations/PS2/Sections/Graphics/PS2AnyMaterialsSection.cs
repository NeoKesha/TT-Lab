using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections
{
    class PS2AnyMaterialsSection : BaseTwinSection
    {
        public PS2AnyMaterialsSection() : base()
        {
            defaultType = typeof(BaseTwinItem);
        }
}
}
