using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2
{
    public class PS2AnyTwinsanityRM2 : BaseTwinSection
    {
        public PS2AnyTwinsanityRM2() : base()
        {
            idToClassDictionary.Add(11, typeof(BaseTwinSection));
            idToClassDictionary.Add(10, typeof(BaseTwinSection));
            idToClassDictionary.Add(0, typeof(BaseTwinSection));
            idToClassDictionary.Add(1, typeof(BaseTwinSection));
            idToClassDictionary.Add(2, typeof(BaseTwinSection));
            idToClassDictionary.Add(3, typeof(BaseTwinSection));
            idToClassDictionary.Add(4, typeof(BaseTwinSection));
            idToClassDictionary.Add(5, typeof(BaseTwinSection));
            idToClassDictionary.Add(6, typeof(BaseTwinSection));
            idToClassDictionary.Add(7, typeof(BaseTwinSection));
            idToClassDictionary.Add(8, typeof(BaseTwinItem));
            idToClassDictionary.Add(9, typeof(BaseTwinItem));
        }
    }
}
