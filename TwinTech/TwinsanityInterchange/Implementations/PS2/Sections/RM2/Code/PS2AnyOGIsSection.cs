﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Code
{
    public class PS2AnyOGIsSection : BaseTwinSection
    {
        public PS2AnyOGIsSection() : base()
        {
            defaultType = typeof(PS2AnyOGI);
        }
    }
}
