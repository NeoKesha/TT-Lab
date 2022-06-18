using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2;
using Twinsanity.TwinsanityInterchange.Implementations.XBOX.Sections;
using Twinsanity.TwinsanityInterchange.Implementations.XBOX.Sections.RMX;

namespace Twinsanity.TwinsanityInterchange.Implementations.XBOX
{
    public class XBOXAnyTwinsanityRMX : BaseTwinSection
    {
        public XBOXAnyTwinsanityRMX() : base()
        {
            idToClassDictionary.Add(Constants.LEVEL_GRAPHICS_SECTION, typeof(XBOXAnyGraphicsSection));
            idToClassDictionary.Add(Constants.LEVEL_CODE_SECTION, typeof(XBOXAnyCodeSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_1_SECTION, typeof(PS2AnyLayoutSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_2_SECTION, typeof(PS2AnyLayoutSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_3_SECTION, typeof(PS2AnyLayoutSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_4_SECTION, typeof(PS2AnyLayoutSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_5_SECTION, typeof(PS2AnyLayoutSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_6_SECTION, typeof(PS2AnyLayoutSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_7_SECTION, typeof(PS2AnyLayoutSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_8_SECTION, typeof(PS2AnyLayoutSection));
            idToClassDictionary.Add(Constants.LEVEL_PARTICLES_ITEM, typeof(PS2AnyParticleData));
            idToClassDictionary.Add(Constants.LEVEL_COLLISION_ITEM, typeof(PS2AnyCollisionData));
        }
    }
}
