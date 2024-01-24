using SharpGLTF.Schema2;
using System;
using System.Collections.Generic;
using System.Linq;
using TT_Lab.Assets;
using TT_Lab.Util;
using Twinsanity.PS2Hardware;
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
                var allVertexes = new List<Vertex>();
                var allIndices = new List<IndexedFace>();
                var indiceAccessor = 0;
                var blendShape = (Vector3)mesh.Extras.Deserialize(typeof(Vector3));
                var primitive = mesh.Primitives[0];
                var vertexes = primitive.GetVertexColumns();
                var indices = primitive.GetTriangleIndices();
                var amountOfSubmodels = (indices.Count() * 3) / TwinVIFCompiler.VertexStripCache;
                var leftovers = (indices.Count() * 3) % TwinVIFCompiler.VertexStripCache;
                if (leftovers > 0)
                {
                    amountOfSubmodels++;
                }

                for (var i = 0; i < vertexes.Positions.Count; i++)
                {
                    var ver = new Vertex(
                            vertexes.Positions[i].ToTwin(),
                            vertexes.Colors0[i].ToTwin(),
                            vertexes.TexCoords0[i].ToTwin());
                    ver.Color = new Vector4(ver.Color.X, ver.Color.Y, ver.Color.Z, ver.Color.W);
                    ver.JointInfo.JointIndex1 = (Int32)vertexes.Joints0[i].X;
                    ver.JointInfo.JointIndex2 = (Int32)vertexes.Joints0[i].Y;
                    ver.JointInfo.JointIndex3 = (Int32)vertexes.Joints0[i].Z;
                    ver.JointInfo.Weight1 = vertexes.Weights0[i].X;
                    ver.JointInfo.Weight2 = vertexes.Weights0[i].Y;
                    ver.JointInfo.Weight3 = vertexes.Weights0[i].Z;

                    allVertexes.Add(ver);
                }

                foreach (var (idx1, idx2, idx3) in indices)
                {
                    allIndices.Add(new IndexedFace(idx1, idx2, idx3));
                }

                if (vertexes.MorphTargets.Count == 0)
                {
                    var zeros = new List<List<System.Numerics.Vector3>>();
                    for (var j = 0; j < blendsAmount; j++)
                    {
                        zeros.Add(new());
                        for (var i = 0; i < vertexes.Positions.Count; i++)
                        {
                            zeros[^1].Add(System.Numerics.Vector3.Zero);
                        }
                    }
                    Models.Add(new SubBlendModelData(blendShape, allVertexes, allIndices, zeros));
                    continue;
                }

                Models.Add(new SubBlendModelData(blendShape, allVertexes, allIndices, vertexes.MorphTargets));

                //for (var submodelIdx = 0; submodelIdx < amountOfSubmodels; ++submodelIdx)
                //{
                //    var faces = new List<IndexedFace>();
                //    for (var i = indiceAccessor; i < indices.Count(); ++i)
                //    {
                //        var (idx1, idx2, idx3) = indices.ElementAt(i);
                //        faces.Add(new IndexedFace(new int[] { idx1, idx2, idx3 }));
                //        if (faces.Count * 3 >= TwinVIFCompiler.VertexStripCache)
                //        {
                //            break;
                //        }
                //    }

                //    var submodelVertexes = new List<Vertex>();
                //    var newIndexList = new List<IndexedFace>();
                //    var idxCount = 0;
                //    foreach (var face in faces)
                //    {
                //        submodelVertexes.Add(allVertexes[face.Indexes![0]]);
                //        submodelVertexes.Add(allVertexes[face.Indexes[1]]);
                //        submodelVertexes.Add(allVertexes[face.Indexes[2]]);
                //        newIndexList.Add(new IndexedFace(new int[] { idxCount, idxCount + 1, idxCount + 2 }));
                //        idxCount += 3;
                //    }

                //    if (vertexes.MorphTargets.Count == 0)
                //    {
                //        var zeros = new List<List<System.Numerics.Vector3>>();
                //        for (var j = 0; j < blendsAmount; j++)
                //        {
                //            zeros.Add(new());
                //            for (var i = 0; i < submodelVertexes.Count; i++)
                //            {
                //                zeros[^1].Add(System.Numerics.Vector3.Zero);
                //            }
                //        }
                //        Models.Add(new SubBlendModelData(blendShape, submodelVertexes, newIndexList, zeros));
                //        indiceAccessor += (TwinVIFCompiler.VertexStripCache / 3);
                //        continue;
                //    }

                //    var morphTargets = new List<List<System.Numerics.Vector3>>();
                //    var targetIndex = 0;
                //    foreach (var target in vertexes.MorphTargets)
                //    {
                //        morphTargets.Add(new());
                //        for (var i = indiceAccessor; i < indices.Count(); ++i)
                //        {
                //            var (idx1, idx2, idx3) = indices.ElementAt(i);
                //            morphTargets[^1].Add(vertexes.MorphTargets[targetIndex].Positions[idx1]);
                //            morphTargets[^1].Add(vertexes.MorphTargets[targetIndex].Positions[idx2]);
                //            morphTargets[^1].Add(vertexes.MorphTargets[targetIndex].Positions[idx3]);
                //            if (morphTargets[^1].Count >= TwinVIFCompiler.VertexStripCache)
                //            {
                //                break;
                //            }
                //        }

                //        targetIndex++;
                //    }

                //    Models.Add(new SubBlendModelData(blendShape, submodelVertexes, newIndexList, morphTargets));
                //    indiceAccessor += (TwinVIFCompiler.VertexStripCache / 3);
                //}
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
