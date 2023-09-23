﻿using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Layout
{
    public class PS2AnyTriggersSection : BaseTwinSection
    {
        public PS2AnyTriggersSection() : base()
        {
            defaultType = typeof(PS2AnyTrigger);
        }
    }
}
