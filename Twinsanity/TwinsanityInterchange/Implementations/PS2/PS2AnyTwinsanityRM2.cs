using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2
{
    public class PS2AnyTwinsanityRM2 : BaseTwinSection
    {
        public PS2AnyTwinsanityRM2() : base()
        {
            idToClassDictionary.Add(Constants.LEVEL_GRAPHICS_SECTION, typeof(BaseTwinSection));
            idToClassDictionary.Add(Constants.LEVEL_CODE_SECTION, typeof(BaseTwinSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_1_SECTION, typeof(BaseTwinSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_2_SECTION, typeof(BaseTwinSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_3_SECTION, typeof(BaseTwinSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_4_SECTION, typeof(BaseTwinSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_5_SECTION, typeof(BaseTwinSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_6_SECTION, typeof(BaseTwinSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_7_SECTION, typeof(BaseTwinSection));
            idToClassDictionary.Add(Constants.LEVEL_LAYOUT_8_SECTION, typeof(BaseTwinSection));
            idToClassDictionary.Add(Constants.LEVEL_PARTICLES_ITEM, typeof(BaseTwinItem));
            idToClassDictionary.Add(Constants.LEVEL_COLLISION_ITEM, typeof(BaseTwinItem));
        }
    }
}
