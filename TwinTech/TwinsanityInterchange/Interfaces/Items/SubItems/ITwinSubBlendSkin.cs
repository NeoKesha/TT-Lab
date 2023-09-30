using System;
using System.Collections.Generic;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems
{
    public interface ITwinSubBlendSkin : ITwinSerializable
    {
        Int32 BlendsAmount { get; set; }
        UInt32 Material { get; set; }
        List<ITwinBlendSkinModel> Models { get; set; }
    }
}
