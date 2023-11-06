using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace TT_Lab.AssetData.Graphics.SubModels
{
    public class SubSkinData : IDisposable
    {
        public LabURI Material { get; set; }
        public List<Vertex> Vertexes { get; set; }
        public List<IndexedFace> Faces { get; set; }

        public SubSkinData(LabURI package, String? variant, ITwinSubSkin subSkin)
        {
            Material = AssetManager.Get().GetUri(package, typeof(Material).Name, variant, subSkin.Material);
            Vertexes = new List<Vertex>();
            Faces = new List<IndexedFace>();

            Int32 refIndex = 0;
            subSkin.CalculateData();
            for (var j = 0; j < subSkin.Vertexes.Count; ++j)
            {
                if (j < subSkin.Vertexes.Count - 2)
                {
                    if (subSkin.SkinJoints[j + 2].Connection)
                    {
                        if (j % 2 == 0)
                        {
                            Faces.Add(new IndexedFace(new int[] { refIndex, refIndex + 1, refIndex + 2 }));
                        }
                        else
                        {
                            Faces.Add(new IndexedFace(new int[] { refIndex + 1, refIndex, refIndex + 2 }));
                        }
                    }
                    ++refIndex;
                }
                Vertexes.Add(new Vertex(subSkin.Vertexes[j], subSkin.Colors[j], subSkin.UVW[j], subSkin.Colors[j])
                {
                    JointInfo = CloneUtils.Clone(subSkin.SkinJoints[j])
                });
            }
        }

        public SubSkinData(LabURI material, List<Vertex> vertexes, List<IndexedFace> faces)
        {
            Material = material;
            Vertexes = vertexes;
            Faces = faces;
        }

        public void Dispose()
        {
            Vertexes.Clear();
            Faces.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
