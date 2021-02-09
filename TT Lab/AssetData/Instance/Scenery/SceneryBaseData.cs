using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using TT_Lab.ViewModels.Instance.Scenery;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;

namespace TT_Lab.AssetData.Instance.Scenery
{
    public class SceneryBaseData
    {
        [JsonProperty(Required = Required.Always)]
        public List<Guid> MeshIDs { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> LodIDs { get; set; }
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

        public SceneryBaseData(SceneryBaseType baseType)
        {
            MeshIDs = new List<Guid>();
            foreach (var m in baseType.MeshIDs)
            {
                MeshIDs.Add(GuidManager.GetGuidByTwinId(m, typeof(Mesh)));
            }
            LodIDs = new List<Guid>();
            foreach (var l in baseType.LodIDs)
            {
                LodIDs.Add(GuidManager.GetGuidByTwinId(l, typeof(LodModel)));
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
            MeshIDs = CloneUtils.CloneList(vm.Meshes.ToList());
            LodIDs = CloneUtils.CloneList(vm.Lods.ToList());
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
                    V1 = V1,
                    V2 = V2,
                    V3 = V3,
                    V4 = V4,
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
                    V1 = V1,
                    V2 = V2,
                    V3 = V3,
                    V4 = V4,
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
    }
}
