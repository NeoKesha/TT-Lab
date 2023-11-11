using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
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
        public List<Byte> BoundingBoxBuilderToJointIndex { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Joints.Clear();
            ExitPoints.Clear();
            JointIndices.Clear();
            RigidModelIds.Clear();
            SkinInverseMatrices.Clear();
            BoundingBoxBuilders.Clear();
            BoundingBoxBuilderToJointIndex.Clear();
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
            BoundingBoxBuilderToJointIndex = CloneUtils.CloneList(ogi.CollisionJointIndices);
            Joints = CloneUtils.DeepClone(ogi.Joints);
            ExitPoints = CloneUtils.DeepClone(ogi.ExitPoints);
            BoundingBoxBuilders = CloneUtils.DeepClone(ogi.Collisions);
            SkinInverseMatrices = CloneUtils.CloneListUnsafe(ogi.SkinInverseBindMatrices);
            Skin = ogi.SkinID != 0 ? AssetManager.Get().GetUri(package, typeof(Skin).Name, null, ogi.SkinID) : LabURI.Empty;
            BlendSkin = ogi.BlendSkinID != 0 ? AssetManager.Get().GetUri(package, typeof(BlendSkin).Name, null, ogi.BlendSkinID) : LabURI.Empty;
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            foreach (var vec in BoundingBox)
            {
                vec.Write(writer);
            }

            writer.Write(JointIndices.Count);
            foreach (var jointIndex in JointIndices)
            {
                writer.Write(jointIndex);
            }

            writer.Write(Joints.Count);
            foreach (var joint in Joints)
            {
                joint.Write(writer);
            }

            writer.Write(RigidModelIds.Count);
            foreach (var rigidModel in RigidModelIds)
            {
                writer.Write(assetManager.GetAsset(rigidModel).ID);
            }

            writer.Write(ExitPoints.Count);
            foreach (var exitPoint in ExitPoints)
            {
                exitPoint.Write(writer);
            }

            writer.Write(SkinInverseMatrices.Count);
            foreach (var matrix in SkinInverseMatrices)
            {
                matrix.Write(writer);
            }

            writer.Write(BoundingBoxBuilders.Count);
            foreach (var builder in BoundingBoxBuilders)
            {
                builder.Write(writer);
            }

            writer.Write(BoundingBoxBuilderToJointIndex.Count);
            foreach (var idx in BoundingBoxBuilderToJointIndex)
            {
                writer.Write(idx);
            }

            writer.Write(Skin == LabURI.Empty ? 0U : assetManager.GetAsset(Skin).ID);
            writer.Write(BlendSkin == LabURI.Empty ? 0U : assetManager.GetAsset(BlendSkin).ID);

            ms.Position = 0;
            return factory.GenerateOGI(ms);
        }
    }
}
