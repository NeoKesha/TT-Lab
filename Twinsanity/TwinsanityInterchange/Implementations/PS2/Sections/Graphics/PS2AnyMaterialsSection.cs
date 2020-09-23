using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections
{
    class PS2AnyMaterialsSection : BaseTwinSection
    {
        public PS2AnyMaterialsSection() : base()
        {
            defaultType = typeof(PS2AnyMaterial);
        }
}
}
