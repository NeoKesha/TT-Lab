using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2
{
    public class PS2AnyTwinsanitySM2 : BaseTwinSection
    {
        public PS2AnyTwinsanitySM2() : base()
        {
            idToClassDictionary.Add(6, typeof(BaseTwinSection));
            idToClassDictionary.Add(0, typeof(BaseTwinItem));
            idToClassDictionary.Add(1, typeof(BaseTwinItem));
            idToClassDictionary.Add(2, typeof(BaseTwinItem));
            idToClassDictionary.Add(3, typeof(BaseTwinItem));
            idToClassDictionary.Add(4, typeof(BaseTwinItem));
            idToClassDictionary.Add(5, typeof(BaseTwinItem));
        }
    }
}
