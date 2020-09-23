using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Instance;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections
{
    public class PS2AnyAIPathsSection : BaseTwinSection
    {
        public PS2AnyAIPathsSection() : base()
        {
            defaultType = typeof(PS2AnyAIPath);
        }
    }
}
