using System;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectGR : SoundEffect
    {
        public override UInt32 Section => Constants.CODE_LANG_GER_SECTION;
        public SoundEffectGR() : base() { }
        public SoundEffectGR(LabURI package, Boolean needVariant, String variant, UInt32 id, String name, ITwinSound sound) : base(package, needVariant, variant, id, name, sound)
        {
        }
    }
}
