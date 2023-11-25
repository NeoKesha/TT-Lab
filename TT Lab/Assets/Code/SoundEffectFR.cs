using System;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectFR : SoundEffect
    {
        public override UInt32 Section => Constants.CODE_LANG_FRE_SECTION;
        public SoundEffectFR() : base() { }
        public SoundEffectFR(LabURI package, String? variant, UInt32 id, String Name, ITwinSound sound) : base(package, variant, id, Name, sound)
        {
        }
    }
}
