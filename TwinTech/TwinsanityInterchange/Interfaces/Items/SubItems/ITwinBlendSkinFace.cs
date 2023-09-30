using System;
using System.Collections.Generic;
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
