using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
