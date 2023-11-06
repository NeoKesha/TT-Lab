using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class ObjectInstanceData : AbstractAssetData
    {
        public ObjectInstanceData()
        {
            InstancesRelated = 10;
            PathsRelated = 10;
            PositionsRelated = 10;
        }

        public ObjectInstanceData(ITwinInstance instance) : this()
        {
            SetTwinItem(instance);
        }

        [JsonProperty(Required = Required.Always)]
        public Vector4 Position { get; set; }
        [JsonProperty(Required = Required.Always)]
        public TwinIntegerRotation RotationX { get; set; }
        [JsonProperty(Required = Required.Always)]
        public TwinIntegerRotation RotationY { get; set; }
        [JsonProperty(Required = Required.Always)]
        public TwinIntegerRotation RotationZ { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 InstancesRelated { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> Instances { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 PositionsRelated { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> Positions { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 PathsRelated { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> Paths { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI ObjectId { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int16 RefListIndex { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI OnSpawnScriptId { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 StateFlags { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> ParamList1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Single> ParamList2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> ParamList3 { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Instances.Clear();
            Positions.Clear();
            Paths.Clear();
            ParamList1.Clear();
            ParamList2.Clear();
            ParamList3.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinInstance instance = GetTwinItem<ITwinInstance>();
            Position = CloneUtils.Clone(instance.Position);
            RotationX = CloneUtils.Clone(instance.RotationX);
            RotationY = CloneUtils.Clone(instance.RotationY);
            RotationZ = CloneUtils.Clone(instance.RotationZ);
            InstancesRelated = instance.InstancesRelated;
            Instances = CloneUtils.CloneList(instance.Instances);
            PositionsRelated = instance.PositionsRelated;
            Positions = CloneUtils.CloneList(instance.Positions);
            PathsRelated = instance.PathsRelated;
            Paths = CloneUtils.CloneList(instance.Paths);
            ObjectId = AssetManager.Get().GetUri(package, typeof(GameObject).Name, null, instance.ObjectId);
            RefListIndex = instance.RefListIndex;
            OnSpawnScriptId = AssetManager.Get().GetUri(package, typeof(BehaviourStarter).Name, null, instance.OnSpawnHeaderScriptID);
            StateFlags = instance.StateFlags;
            ParamList1 = CloneUtils.CloneList(instance.ParamList1);
            ParamList2 = CloneUtils.CloneList(instance.ParamList2);
            ParamList3 = CloneUtils.CloneList(instance.ParamList3);
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            Position.Write(writer);
            RotationX.Write(writer);
            RotationY.Write(writer);
            RotationZ.Write(writer);

            writer.Write(Instances.Count);
            writer.Write(Instances.Count);
            writer.Write(InstancesRelated);
            foreach (var inst in Instances)
            {
                writer.Write(inst);
            }

            writer.Write(Positions.Count);
            writer.Write(Positions.Count);
            writer.Write(PositionsRelated);
            foreach (var pos in Positions)
            {
                writer.Write(pos);
            }

            writer.Write(Paths.Count);
            writer.Write(Paths.Count);
            writer.Write(PathsRelated);
            foreach (var path in Paths)
            {
                writer.Write(path);
            }

            writer.Write((UInt16)assetManager.GetAsset(ObjectId).ID);

            writer.Write(RefListIndex);

            writer.Write(OnSpawnScriptId == LabURI.Empty ? UInt16.MaxValue : (UInt16)assetManager.GetAsset(OnSpawnScriptId).ID);
            writer.Write((Byte)ParamList1.Count);
            writer.Write((Byte)ParamList2.Count);
            writer.Write((Byte)ParamList3.Count);
            writer.Write((Byte)0);
            writer.Write(StateFlags);

            writer.Write(ParamList1.Count);
            foreach (var flag in ParamList1)
            {
                writer.Write(flag);
            }

            writer.Write(ParamList2.Count);
            foreach (var @float in ParamList2)
            {
                writer.Write(@float);
            }

            writer.Write(ParamList3.Count);
            foreach (var param in ParamList3)
            {
                writer.Write(param);
            }

            ms.Position = 0;
            return factory.GenerateInstance(ms);
        }
    }
}
