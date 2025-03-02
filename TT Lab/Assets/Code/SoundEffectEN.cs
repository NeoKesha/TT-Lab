using System;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectEN : SoundEffect
    {
        public override UInt32 Section => Constants.CODE_LANG_ENG_SECTION;
        public SoundEffectEN() : base() { }
        public SoundEffectEN(LabURI package, Boolean needVariant, String variant, UInt32 id, String name, ITwinSound sound) : base(package, needVariant, variant, id, name, sound)
        {
        }
    }
}
