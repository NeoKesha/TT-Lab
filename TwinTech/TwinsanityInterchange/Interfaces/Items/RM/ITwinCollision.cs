using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.Collision;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM
{
    public interface ITwinCollision : ITwinItem
    {
        UInt32 UnkInt { get; set; }
        List<TwinCollisionTrigger> Triggers { get; set; }
        List<TwinGroupInformation> Groups { get; set; }
        List<TwinCollisionTriangle> Triangles { get; set; }
        List<Vector4> Vectors { get; set; }
    }
}
