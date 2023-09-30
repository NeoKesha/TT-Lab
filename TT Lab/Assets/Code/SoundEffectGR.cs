using System;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectGR : SoundEffect
    {
        public SoundEffectGR() : base() { }
        public SoundEffectGR(LabURI package, String? variant, UInt32 id, String name, ITwinSound sound) : base(package, variant, id, name, sound)
        {
        }
    }
}
