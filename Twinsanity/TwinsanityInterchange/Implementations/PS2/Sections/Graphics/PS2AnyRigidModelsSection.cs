﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections
{
    public class PS2AnyRigidModelsSection : BaseTwinSection
    {
        public PS2AnyRigidModelsSection() : base()
        {
            defaultType = typeof(PS2AnyRigidModel);
        }
}
}
