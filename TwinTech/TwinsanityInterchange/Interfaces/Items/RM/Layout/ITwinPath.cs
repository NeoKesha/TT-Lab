using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout
{
    public interface ITwinPath : ITwinItem
    {
        List<Vector4> PointList { get; set; }
        List<Vector2> ParameterList { get; set; }
    }
}
