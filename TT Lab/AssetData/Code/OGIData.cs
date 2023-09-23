using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class OGIData : AbstractAssetData
    {
        public OGIData()
        {
        }

        public OGIData(PS2AnyOGI ogi) : this()
        {
            SetTwinItem(ogi);
        }

        [JsonProperty(Required = Required.Always)]
        public Vector4[] BoundingBox { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Joint> Type1List { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<ExitPoint> Type2List { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Byte> RigidRelatedList { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> RigidModelIds { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Matrix4> Type1RelatedMatrix { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 SkinID { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 BlendSkinID { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<BoundingBoxBuilder> BoundingBoxBuilders { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Byte> BuilderMatrixIndex { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Type1List.Clear();
            Type2List.Clear();
            RigidRelatedList.Clear();
            RigidModelIds.Clear();
            Type1RelatedMatrix.Clear();
            BoundingBoxBuilders.Clear();
            BuilderMatrixIndex.Clear();
        }

        public override void Import(String package, String subpackage, String? variant)
        {
            PS2AnyOGI ogi = GetTwinItem<PS2AnyOGI>();
            BoundingBox = CloneUtils.CloneArray(ogi.BoundingBox);
            RigidRelatedList = CloneUtils.CloneList(ogi.JointIndices);
            RigidModelIds = new List<LabURI>();
            foreach (var model in ogi.RigidModelIds)
            {
                RigidModelIds.Add(AssetManager.Get().GetUri(package, subpackage, typeof(RigidModel).Name, variant, model));
            }
            BuilderMatrixIndex = CloneUtils.CloneList(ogi.CollisionJointIndices);
            Type1List = CloneUtils.DeepClone(ogi.Joints);
            Type2List = CloneUtils.DeepClone(ogi.ExitPoints);
            BoundingBoxBuilders = CloneUtils.DeepClone(ogi.Collisions);
            Type1RelatedMatrix = CloneUtils.CloneListUnsafe(ogi.SkinInverseBindMatrices);
        }
    }
}
