using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectEN : SoundEffect
    {
        public SoundEffectEN() : base() { }
        public SoundEffectEN(LabURI package, String? variant, UInt32 id, String Name, PS2AnySound sound) : base(package, variant, id, Name, sound)
        {
        }
    }
}
