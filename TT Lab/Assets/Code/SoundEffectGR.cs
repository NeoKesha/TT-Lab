using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectGR : SoundEffect
    {
        public SoundEffectGR() : base() { }
        public SoundEffectGR(String package, String subpackage, String? variant, UInt32 id, String name, PS2AnySound sound) : base(package, subpackage, variant, id, name, sound)
        {
        }
    }
}
