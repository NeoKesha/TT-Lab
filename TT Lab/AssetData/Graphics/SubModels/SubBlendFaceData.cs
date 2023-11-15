using System;
using System.Collections.Generic;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace TT_Lab.AssetData.Graphics.SubModels
{
    public class SubBlendFaceData : IDisposable
    {
        public UInt32 VertexesAmount { get; set; }
        public List<BlendShapeData> BlendShapes { get; set; } = new();

        public SubBlendFaceData(ITwinBlendSkinFace face)
        {
            VertexesAmount = face.VertexesAmount;

            face.CalculateData();
            foreach (var shape in face.Vertices)
            {
                var shapeData = new BlendShapeData()
                {
                    Offset = CloneUtils.Clone(shape.Offset)
                };
                BlendShapes.Add(shapeData);
            }
        }

        public SubBlendFaceData(List<Assimp.VectorKey> positions)
        {
            VertexesAmount = (UInt32)positions.Count;
            foreach (var pos in positions)
            {
                var shapeData = new BlendShapeData()
                {
                    Offset = new Twinsanity.TwinsanityInterchange.Common.Vector4(pos.Value.X, pos.Value.Y, pos.Value.Z, 1.0f)
                };
                BlendShapes.Add(shapeData);
            }
        }

        public void Dispose()
        {
            BlendShapes.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
