using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
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
    }
}
