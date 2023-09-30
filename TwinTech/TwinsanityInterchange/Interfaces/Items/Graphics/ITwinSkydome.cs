using System;
using System.Collections.Generic;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items
{
    public interface ITwinSkydome : ITwinItem
    {
        Int32 Header { get; set; } // Unused by the game
        List<UInt32> Meshes { get; set; }
    }
}
