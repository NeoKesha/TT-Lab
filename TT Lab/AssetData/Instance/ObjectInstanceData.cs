using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class ObjectInstanceData : AbstractAssetData
    {
        public ObjectInstanceData()
        {
        }

        public ObjectInstanceData(PS2AnyInstance instance) : this()
        {
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
            ObjectId = instance.ObjectId;
            UnkInt1 = instance.UnkInt1;
            UnkInt2 = instance.UnkInt2;
            UnkInt3 = instance.UnkInt3;
            ParamList1 = CloneUtils.CloneList(instance.ParamList1);
            ParamList2 = CloneUtils.CloneList(instance.ParamList2);
            ParamList3 = CloneUtils.CloneList(instance.ParamList3);
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
        public UInt16 ObjectId { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> ParamList1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Single> ParamList2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> ParamList3 { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
