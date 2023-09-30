using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems
{
    public interface ITwinBlendSkinFace : ITwinSerializable
    {
        UInt32 VertexesAmount { get; set; }
        List<VertexBlendShape> Vertices { get; set; }

        void CalculateData();
    }
}
