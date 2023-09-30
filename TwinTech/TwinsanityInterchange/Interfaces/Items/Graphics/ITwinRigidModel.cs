using System;
using System.Collections.Generic;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items
{
    public interface ITwinRigidModel : ITwinItem
    {
        UInt32 Header { get; set; } // Unused by the game
        List<UInt32> Materials { get; set; }
        UInt32 Model { get; set; }
    }
}
