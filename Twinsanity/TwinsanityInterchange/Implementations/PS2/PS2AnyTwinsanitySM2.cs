using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2
{
    public class PS2AnyTwinsanitySM2 : BaseTwinSection
    {
        public PS2AnyTwinsanitySM2() : base()
        {
            idToClassDictionary.Add(Constants.SCENERY_GRAPHICS_SECTION, typeof(BaseTwinSection));
            idToClassDictionary.Add(Constants.SCENERY_SECENERY_ITEM, typeof(BaseTwinItem));
            idToClassDictionary.Add(Constants.SCENERY_UNK_1_ITEM, typeof(BaseTwinItem));
            idToClassDictionary.Add(Constants.SCENERY_UNK_2_ITEM, typeof(BaseTwinItem));
            idToClassDictionary.Add(Constants.SCENERY_UNK_3_ITEM, typeof(BaseTwinItem));
            idToClassDictionary.Add(Constants.SCENERY_DYNAMIC_SECENERY_ITEM, typeof(BaseTwinItem));
            idToClassDictionary.Add(Constants.SCENERY_LINK_ITEM, typeof(BaseTwinItem));
        }
    }
}
