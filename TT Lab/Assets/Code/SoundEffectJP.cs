﻿using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffectJP : SoundEffect
    {
        public SoundEffectJP() : base() { }
        public SoundEffectJP(LabURI package, String? variant, UInt32 id, String Name, PS2AnySound sound) : base(package, variant, id, Name, sound)
        {
        }
    }
}
