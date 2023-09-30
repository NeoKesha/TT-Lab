using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Common;

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
