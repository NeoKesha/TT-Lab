using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectIT : SoundEffect
    {
        public SoundEffectIT() : base() { }
        public SoundEffectIT(String package, String subpackage, String? variant, UInt32 id, String Name, PS2AnySound sound) : base(package, subpackage, variant, id, Name, sound)
        {
        }
    }
}
