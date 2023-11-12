using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Material = AssetManager.Get().GetUri(package, typeof(Material).Name, null, subSkin.Material);
            if (Material == LabURI.Empty)
            {
                var allMaterials = AssetManager.Get().GetAssets().FindAll(a => a is Material).ConvertAll(a => a.URI);
                var actuallyHasTheMaterial = allMaterials.FindAll(uri => uri.ToString().Contains(subSkin.Material.ToString()));
                throw new Exception($"Couldn't find requested material 0x{subSkin.Material:X}!");
            }

            Vertexes = new List<Vertex>();
            Faces = new List<IndexedFace>();

            Int32 refIndex = 0;
            subSkin.CalculateData();
            for (var i = 0; i < subSkin.Vertexes.Count; ++i)
            {
                if (i < subSkin.Vertexes.Count - 2)
                {
                    if (subSkin.SkinJoints[i + 2].Connection)
                    {
                        if (i % 2 == 0)
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
                Vertexes.Add(new Vertex(subSkin.Vertexes[i], subSkin.Colors[i], subSkin.UVW[i], subSkin.Colors[i])
                {
                    JointInfo = CloneUtils.Clone(subSkin.SkinJoints[i])
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
