using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.AssetData.Code
{
    public class OGIData : AbstractAssetData
    {
        public OGIData()
        {
        }

        public OGIData(ITwinOGI ogi) : this()
        {
            SetTwinItem(ogi);
        }

        [JsonProperty(Required = Required.Always)]
        public Vector4[] BoundingBox { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<TwinJoint> Joints { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<TwinExitPoint> ExitPoints { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Byte> JointIndices { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> RigidModelIds { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Matrix4> SkinInverseMatrices { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI Skin { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI BlendSkin { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<TwinBoundingBoxBuilder> BoundingBoxBuilders { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Byte> BuilderMatrixIndex { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Joints.Clear();
            ExitPoints.Clear();
            JointIndices.Clear();
            RigidModelIds.Clear();
            SkinInverseMatrices.Clear();
            BoundingBoxBuilders.Clear();
            BuilderMatrixIndex.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinOGI ogi = GetTwinItem<ITwinOGI>();
            BoundingBox = CloneUtils.CloneArray(ogi.BoundingBox);
            JointIndices = CloneUtils.CloneList(ogi.JointIndices);
            RigidModelIds = new List<LabURI>();
            foreach (var model in ogi.RigidModelIds)
            {
                RigidModelIds.Add(AssetManager.Get().GetUri(package, typeof(RigidModel).Name, null, model));
            }
            BuilderMatrixIndex = CloneUtils.CloneList(ogi.CollisionJointIndices);
            Joints = CloneUtils.DeepClone(ogi.Joints);
            ExitPoints = CloneUtils.DeepClone(ogi.ExitPoints);
            BoundingBoxBuilders = CloneUtils.DeepClone(ogi.Collisions);
            SkinInverseMatrices = CloneUtils.CloneListUnsafe(ogi.SkinInverseBindMatrices);
            Skin = ogi.SkinID != 0 ? AssetManager.Get().GetUri(package, typeof(Skin).Name, null, ogi.SkinID) : LabURI.Empty;
            BlendSkin = ogi.BlendSkinID != 0 ? AssetManager.Get().GetUri(package, typeof(BlendSkin).Name, null, ogi.BlendSkinID) : LabURI.Empty;
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
