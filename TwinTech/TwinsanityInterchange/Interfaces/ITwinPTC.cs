using System;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinPTC : ITwinSerializable
    {
        public UInt32 TexID { get; set; }
        public UInt32 MatID { get; set; }
        public ITwinTexture Texture { get; set; }
        public ITwinMaterial Material { get; set; }
    }
}
