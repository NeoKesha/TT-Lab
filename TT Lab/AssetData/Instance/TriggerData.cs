using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class TriggerData : AbstractAssetData
    {
        public TriggerData()
        {
        }

        public TriggerData(PS2AnyTrigger trigger) : this()
        {
            SetTwinItem(trigger);
        }

        public TriggerData(TwinTrigger trigger) : this()
        {
            ObjectActivatorMask = trigger.ObjectActivatorMask;
            Position = CloneUtils.Clone(trigger.Position);
            Rotation = CloneUtils.Clone(trigger.Rotation);
            Scale = CloneUtils.Clone(trigger.Scale);
            Instances = CloneUtils.CloneList(trigger.Instances);
            Header = trigger.Header;
            UnkFloat = trigger.UnkFloat;
            InstanceExtensionValue = trigger.InstanceExtensionValue;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 ObjectActivatorMask { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Position { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Rotation { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Scale { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> Instances { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 InstanceExtensionValue { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 Arg1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 Arg2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 Arg3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 Arg4 { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Instances.Clear();
        }

        public override void Import(String package, String subpackage, String? variant)
        {
            PS2AnyTrigger trigger = GetTwinItem<PS2AnyTrigger>();
            ObjectActivatorMask = trigger.Trigger.ObjectActivatorMask;
            Position = CloneUtils.Clone(trigger.Trigger.Position);
            Rotation = CloneUtils.Clone(trigger.Trigger.Rotation);
            Scale = CloneUtils.Clone(trigger.Trigger.Scale);
            Instances = CloneUtils.CloneList(trigger.Trigger.Instances);
            Header = trigger.Trigger.Header;
            UnkFloat = trigger.Trigger.UnkFloat;
            InstanceExtensionValue = trigger.Trigger.InstanceExtensionValue;
            Arg1 = trigger.Arguments[0];
            Arg2 = trigger.Arguments[1];
            Arg3 = trigger.Arguments[2];
            Arg4 = trigger.Arguments[3];
        }
    }
}
