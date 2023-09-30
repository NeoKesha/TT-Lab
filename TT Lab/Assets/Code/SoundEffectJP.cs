using System;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectJP : SoundEffect
    {
        public SoundEffectJP() : base() { }
        public SoundEffectJP(LabURI package, String? variant, UInt32 id, String Name, ITwinSound sound) : base(package, variant, id, Name, sound)
        {
        }
    }
}
