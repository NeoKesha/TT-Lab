using System;
using System.Collections.Generic;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items
{
    public interface ITwinLOD : ITwinItem
    {
        Int32 Type { get; set; }
        Int32 MinDrawDistance { get; set; }
        Int32 MaxDrawDistance { get; set; }
        Int32[] ModelsDrawDistances { get; set; }
        List<UInt32> Meshes { get; set; }
    }
}
