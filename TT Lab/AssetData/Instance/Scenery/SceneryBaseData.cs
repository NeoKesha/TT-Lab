using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using TT_Lab.ViewModels.Instance.Scenery;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.AssetData.Instance.Scenery
{
    public class SceneryBaseData
    {
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> MeshIDs { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> LodIDs { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Vector4[]> BoundingBoxes { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Matrix4> MeshModelMatrices { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Matrix4> LodModelMatrices { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 UnkVec1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 UnkVec2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 UnkVec3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 UnkVec4 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 UnkVec5 { get; set; }

        public SceneryBaseData() { }

        public SceneryBaseData(LabURI package, String? variant, TwinSceneryBaseType baseType)
        {
            MeshIDs = new List<LabURI>();
            foreach (var m in baseType.MeshIDs)
            {
                MeshIDs.Add(AssetManager.Get().GetUri(package, typeof(Mesh).Name, variant, m));
            }
            LodIDs = new List<LabURI>();
            foreach (var l in baseType.LodIDs)
            {
                LodIDs.Add(AssetManager.Get().GetUri(package, typeof(LodModel).Name, variant, l));
            }
            BoundingBoxes = new List<Vector4[]>();
            foreach (var bb in baseType.BoundingBoxes)
            {
                BoundingBoxes.Add(new Vector4[] { CloneUtils.Clone(bb[0]), CloneUtils.Clone(bb[1]) });
            }
            MeshModelMatrices = new List<Matrix4>();
            foreach (var mat in baseType.MeshModelMatrices)
            {
                MeshModelMatrices.Add(CloneUtils.DeepClone(mat));
            }
            LodModelMatrices = new List<Matrix4>();
            foreach (var mat in baseType.LodModelMatrices)
            {
                LodModelMatrices.Add(CloneUtils.DeepClone(mat));
            }
            UnkVec1 = CloneUtils.Clone(baseType.UnkVec1);
            UnkVec2 = CloneUtils.Clone(baseType.UnkVec2);
            UnkVec3 = CloneUtils.Clone(baseType.UnkVec3);
            UnkVec4 = CloneUtils.Clone(baseType.UnkVec4);
            UnkVec5 = CloneUtils.Clone(baseType.UnkVec5);
        }

        public SceneryBaseData(BaseSceneryViewModel vm)
        {
            MeshIDs = CloneUtils.CloneList(vm.Meshes.ToList(), (e) => new(e.GetUri()));
            LodIDs = CloneUtils.CloneList(vm.Lods.ToList(), (e) => new(e.GetUri()));
            BoundingBoxes = new List<Vector4[]>();
            foreach (var bb in vm.Bbs)
            {
                BoundingBoxes.Add(new Vector4[]
                {
                    new Vector4
                    {
                        X = bb[0].X,
                        Y = bb[0].Y,
                        Z = bb[0].Z,
                        W = bb[0].W,
                    },
                    new Vector4
                    {
                        X = bb[1].X,
                        Y = bb[1].Y,
                        Z = bb[1].Z,
                        W = bb[1].W,
                    }
                });
            }
            MeshModelMatrices = new List<Matrix4>();
            foreach (var mat in vm.MeshModelMatrices)
            {
                var V1 = new Vector4
                {
                    X = mat[0].X,
                    Y = mat[0].Y,
                    Z = mat[0].Z,
                    W = mat[0].W,
                };
                var V2 = new Vector4
                {
                    X = mat[1].X,
                    Y = mat[1].Y,
                    Z = mat[1].Z,
                    W = mat[1].W,
                };
                var V3 = new Vector4
                {
                    X = mat[2].X,
                    Y = mat[2].Y,
                    Z = mat[2].Z,
                    W = mat[2].W,
                };
                var V4 = new Vector4
                {
                    X = mat[3].X,
                    Y = mat[3].Y,
                    Z = mat[3].Z,
                    W = mat[3].W,
                };
                MeshModelMatrices.Add(new Matrix4
                {
                    Column1 = V1,
                    Column2 = V2,
                    Column3 = V3,
                    Column4 = V4,
                });
            }
            LodModelMatrices = new List<Matrix4>();
            foreach (var mat in vm.LodModelMatrices)
            {
                var V1 = new Vector4
                {
                    X = mat[0].X,
                    Y = mat[0].Y,
                    Z = mat[0].Z,
                    W = mat[0].W,
                };
                var V2 = new Vector4
                {
                    X = mat[1].X,
                    Y = mat[1].Y,
                    Z = mat[1].Z,
                    W = mat[1].W,
                };
                var V3 = new Vector4
                {
                    X = mat[2].X,
                    Y = mat[2].Y,
                    Z = mat[2].Z,
                    W = mat[2].W,
                };
                var V4 = new Vector4
                {
                    X = mat[3].X,
                    Y = mat[3].Y,
                    Z = mat[3].Z,
                    W = mat[3].W,
                };
                LodModelMatrices.Add(new Matrix4
                {
                    Column1 = V1,
                    Column2 = V2,
                    Column3 = V3,
                    Column4 = V4,
                });
            }
            UnkVec1 = new Vector4
            {
                X = vm.UnkVec1.X,
                Y = vm.UnkVec1.Y,
                Z = vm.UnkVec1.Z,
                W = vm.UnkVec1.W,
            };
            UnkVec2 = new Vector4
            {
                X = vm.UnkVec2.X,
                Y = vm.UnkVec2.Y,
                Z = vm.UnkVec2.Z,
                W = vm.UnkVec2.W,
            };
            UnkVec3 = new Vector4
            {
                X = vm.UnkVec3.X,
                Y = vm.UnkVec3.Y,
                Z = vm.UnkVec3.Z,
                W = vm.UnkVec3.W,
            };
            UnkVec4 = new Vector4
            {
                X = vm.UnkVec4.X,
                Y = vm.UnkVec4.Y,
                Z = vm.UnkVec4.Z,
                W = vm.UnkVec4.W,
            };
            UnkVec5 = new Vector4
            {
                X = vm.UnkVec5.X,
                Y = vm.UnkVec5.Y,
                Z = vm.UnkVec5.Z,
                W = vm.UnkVec5.W,
            };
        }

        public virtual ITwinScenery.SceneryType GetSceneryType()
        {
            return ITwinScenery.SceneryType.Leaf;
        }

        public virtual void Write(BinaryWriter writer)
        {
            var assetManager = AssetManager.Get();
            writer.Write(MeshIDs.Count);
            foreach (var mesh in MeshIDs)
            {
                writer.Write(assetManager.GetAsset(mesh).ID);
            }

            writer.Write(LodIDs.Count);
            foreach (var lod in LodIDs)
            {
                writer.Write(assetManager.GetAsset(lod).ID);
            }

            writer.Write(BoundingBoxes.Count);
            foreach (var bb in BoundingBoxes)
            {
                bb[0].Write(writer);
                bb[1].Write(writer);
            }

            writer.Write(MeshModelMatrices.Count);
            foreach (var mat in MeshModelMatrices)
            {
                mat.Write(writer);
            }

            writer.Write(LodModelMatrices.Count);
            foreach (var mat in LodModelMatrices)
            {
                mat.Write(writer);
            }

            UnkVec1.Write(writer);
            UnkVec2.Write(writer);
            UnkVec3.Write(writer);
            UnkVec4.Write(writer);
            UnkVec5.Write(writer);
        }

        public void ResolveChunkResouces(ITwinItemFactory factory, ITwinSection section)
        {
            var assetManager = AssetManager.Get();
            var meshSection = section.GetItem<ITwinSection>(Constants.GRAPHICS_MESHES_SECTION);
            var lodSection = section.GetItem<ITwinSection>(Constants.GRAPHICS_LODS_SECTION);

            foreach (var mesh in MeshIDs)
            {
                assetManager.GetAsset(mesh).ResolveChunkResources(factory, meshSection);
            }

            foreach (var lod in LodIDs)
            {
                assetManager.GetAsset(lod).ResolveChunkResources(factory, lodSection);
            }
        }
    }
}
