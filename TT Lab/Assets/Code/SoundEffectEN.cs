using System;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectEN : SoundEffect
    {
        public SoundEffectEN() : base() { }
        public SoundEffectEN(LabURI package, String? variant, UInt32 id, String Name, ITwinSound sound) : base(package, variant, id, Name, sound)
        {
        }
    }
}
