using System;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectIT : SoundEffect
    {
        public override UInt32 Section => Constants.CODE_LANG_ITA_SECTION;
        public SoundEffectIT() : base() { }
        public SoundEffectIT(LabURI package, Boolean needVariant, String variant, UInt32 id, String Name, ITwinSound sound) : base(package, needVariant, variant, id, Name, sound)
        {
        }
    }
}
