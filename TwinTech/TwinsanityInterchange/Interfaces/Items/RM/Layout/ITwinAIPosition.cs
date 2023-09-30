using System;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout
{
    public interface ITwinAIPosition : ITwinItem
    {
        Vector4 Position { get; set; }
        UInt16 UnkShort { get; set; }
    }
}
