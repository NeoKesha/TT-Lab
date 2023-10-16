using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinPSF : ITwinSerializable
    {
        public List<ITwinPTC> FontPages { get; set; }
        public List<Vector4> UnkVecs { get; set; }
        public Int32 UnkInt { get; set; }
    }
}
