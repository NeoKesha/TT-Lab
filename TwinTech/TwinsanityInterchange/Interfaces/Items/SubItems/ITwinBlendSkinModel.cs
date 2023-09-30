using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems
{
    public interface ITwinBlendSkinModel : ITwinSerializable
    {
        Int32 BlendsAmount { get; set; }
        Int32 VertexesAmount { get; set; }
        Vector3 BlendShape { get; set; }
        List<ITwinBlendSkinFace> Faces { get; set; }
        List<Vector4> Vertexes { get; set; }
        List<Vector4> UVW { get; set; }
        List<Vector4> Colors { get; set; }
        List<VertexJointInfo> SkinJoints { get; set; }

        void CalculateData();
    }
}
