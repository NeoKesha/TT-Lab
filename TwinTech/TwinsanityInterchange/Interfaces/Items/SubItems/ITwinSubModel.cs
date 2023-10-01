using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems
{
    public interface ITwinSubModel : ITwinSerializable
    {
        List<Vector4> Vertexes { get; set; }
        List<Vector4> UVW { get; set; }
        List<Vector4> Colors { get; set; }
        List<Vector4> EmitColor { get; set; }
        List<Vector4> Normals { get; set; }
        List<bool> Connection { get; set; }

        void CalculateData();
    }
}
