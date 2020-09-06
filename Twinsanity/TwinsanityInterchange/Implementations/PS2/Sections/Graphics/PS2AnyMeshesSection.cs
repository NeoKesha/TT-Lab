using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections
{
    public class PS2AnyMeshesSection : BaseTwinSection
    {
        public PS2AnyMeshesSection() : base()
        {
            defaultType = typeof(BaseTwinItem);
        }
}
}
