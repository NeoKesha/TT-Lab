using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections;
using Twinsanity.TwinsanityInterchange.Implementations.XBOX.Sections;

namespace Twinsanity.TwinsanityInterchange.Implementations.XBOX
{
    public class XBOXAnyTwinsanitySMX : BaseTwinSection
    {
        public XBOXAnyTwinsanitySMX() : base()
        {
            idToClassDictionary.Add(Constants.SCENERY_GRAPHICS_SECTION, typeof(XBOXAnyGraphicsSection));
            idToClassDictionary.Add(Constants.SCENERY_SECENERY_ITEM, typeof(PS2AnyScenery));
            idToClassDictionary.Add(Constants.SCENERY_UNK_1_ITEM, typeof(BaseTwinItem));
            idToClassDictionary.Add(Constants.SCENERY_UNK_2_ITEM, typeof(BaseTwinItem));
            idToClassDictionary.Add(Constants.SCENERY_UNK_3_ITEM, typeof(BaseTwinItem));
            idToClassDictionary.Add(Constants.SCENERY_DYNAMIC_SECENERY_ITEM, typeof(PS2AnyDynamicScenery));
            idToClassDictionary.Add(Constants.SCENERY_LINK_ITEM, typeof(PS2AnyLink));
        }
    }
}
