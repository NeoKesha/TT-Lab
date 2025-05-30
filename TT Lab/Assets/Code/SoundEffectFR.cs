﻿using System;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectFR : SoundEffect
    {
        public override UInt32 Section => Constants.CODE_LANG_FRE_SECTION;
        public SoundEffectFR() : base() { }
        public SoundEffectFR(LabURI package, Boolean needVariant, String variant, UInt32 id, String name, ITwinSound sound) : base(package, needVariant, variant, id, name, sound)
        {
        }
    }
}
