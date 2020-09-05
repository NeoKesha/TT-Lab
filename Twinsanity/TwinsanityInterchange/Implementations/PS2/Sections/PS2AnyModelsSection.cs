using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections
{
    public class PS2AnyModelsSection : BaseTwinSection
    {
        public PS2AnyModelsSection() : base()
        {
            defaultType = typeof(BaseTwinItem);
        }
}
}
