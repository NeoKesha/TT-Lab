using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items
{
    public interface ITwinMaterial : ITwinItem
    {
        UInt64 Header { get; set; }
        UInt32 DmaChainIndex { get; set; }
        String Name { get; set; }
        List<TwinShader> Shaders { get; set; }
    }
}
