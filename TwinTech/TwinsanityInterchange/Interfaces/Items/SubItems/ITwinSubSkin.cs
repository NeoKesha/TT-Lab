using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems
{
    public interface ITwinSubSkin : ITwinSerializable
    {
        UInt32 Material { get; set; }
        List<Vector4> Vertexes { get; set; }
        List<Vector4> UVW { get; set; }
        List<Vector4> Colors { get; set; }
        List<VertexJointInfo> SkinJoints { get; set; }

        void CalculateData();
    }
}
