using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems
{
    public interface ITwinSubBlendSkin : ITwinSerializable
    {
        Int32 BlendsAmount { get; set; }
        UInt32 Material { get; set; }
        List<ITwinBlendSkinModel> Models { get; set; }
    }
}
