using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectGR : SoundEffect
    {
        public SoundEffectGR() : base() { }
        public SoundEffectGR(LabURI package, String? variant, UInt32 id, String name, PS2AnySound sound) : base(package, variant, id, name, sound)
        {
        }
    }
}
