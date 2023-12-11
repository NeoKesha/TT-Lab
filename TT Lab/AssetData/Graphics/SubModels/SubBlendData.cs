using SharpGLTF.Schema2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TT_Lab.Assets;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace TT_Lab.AssetData.Graphics.SubModels
{
    public class SubBlendData : IDisposable
    {
        public LabURI Material { get; set; } = LabURI.Empty;
        public List<SubBlendModelData> Models { get; set; } = new();

        public SubBlendData(LabURI package, String? variant, ITwinSubBlendSkin blend)
        {
            Material = AssetManager.Get().GetUri(package, typeof(Assets.Graphics.Material).Name, variant, blend.Material);
            if (Material == LabURI.Empty)
            {
                var allMaterials = AssetManager.Get().GetAssets().FindAll(a => a is Assets.Graphics.Material).ConvertAll(a => a.URI);
                var actuallyHasTheMaterial = allMaterials.FindAll(uri => uri.ToString().Contains(blend.Material.ToString()));
                throw new Exception($"Couldn't find requested material 0x{blend.Material:X}!");
            }

            foreach (var model in blend.Models)
            {
                Models.Add(new SubBlendModelData(model));
            }
        }

        public SubBlendData(LabURI material, IEnumerable<SharpGLTF.Schema2.Mesh> meshes, Int32 blendsAmount)
        {
            Material = material;

            foreach (var mesh in meshes)
            {
                var subblend = new List<Vertex>();
                var faces = new List<IndexedFace>();
                var blendShape = (Vector3)mesh.Extras.Deserialize(typeof(Vector3));
                foreach (var primitive in mesh.Primitives)
                {
                    var vertexes = primitive.GetVertexColumns();
                    for (var i = 0; i < vertexes.Positions.Count; i++)
                    {
                        var ver = new Vertex(
                                vertexes.Positions[i].ToTwin(),
                                vertexes.Colors0[i].ToTwin(),
                                vertexes.TexCoords0[i].ToTwin());
                        ver.JointInfo.JointIndex1 = (Int32)vertexes.Joints0[i].X;
                        ver.JointInfo.JointIndex2 = (Int32)vertexes.Joints0[i].Y;
                        ver.JointInfo.JointIndex3 = (Int32)vertexes.Joints0[i].Z;
                        ver.JointInfo.Weight1 = vertexes.Weights0[i].X;
                        ver.JointInfo.Weight2 = vertexes.Weights0[i].Y;
                        ver.JointInfo.Weight3 = vertexes.Weights0[i].Z;

                        subblend.Add(ver);
                    }

                    foreach (var (idx1, idx2, idx3) in primitive.GetTriangleIndices())
                    {
                        faces.Add(new IndexedFace(new int[] { idx1, idx2, idx3 }));
                    }

                    Debug.Assert(vertexes.MorphTargets.Count == blendsAmount);
                    Models.Add(new SubBlendModelData(blendShape, subblend, faces, vertexes.MorphTargets));
                }
            }
        }

        public void Dispose()
        {
            foreach (var model in Models)
            {
                model.Dispose();
            }
            Models.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
