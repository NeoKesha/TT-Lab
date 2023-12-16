using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Instance;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;
using static Twinsanity.TwinsanityInterchange.Enumerations.Enums;

namespace TT_Lab.AssetData.Instance
{
    public class TriggerData : AbstractAssetData
    {
        public TriggerData()
        {
        }

        public TriggerData(ITwinTrigger trigger) : this()
        {
            SetTwinItem(trigger);
        }

        public TriggerData(LabURI package, String? variant, TwinTrigger trigger, Int32? layoutId) : this()
        {
            ObjectActivatorMask = trigger.ObjectActivatorMask;
            Position = CloneUtils.Clone(trigger.Position);
            Rotation = CloneUtils.Clone(trigger.Rotation);
            Scale = CloneUtils.Clone(trigger.Scale);
            Instances = new(trigger.Instances.Count);
            foreach (var inst in trigger.Instances)
            {
                Instances.Add(AssetManager.Get().GetUri(package, typeof(ObjectInstance).Name, variant, layoutId, inst));
            }
            Header = trigger.Header;
            UnkFloat = trigger.UnkFloat;
            InstanceExtensionValue = trigger.InstanceExtensionValue;
            TriggerArgument1 = 0;
            TriggerArgument2 = 0;
            TriggerArgument3 = 0;
            TriggerArgument4 = 0;
        }

        [JsonProperty(Required = Required.Always)]
        public TriggerActivatorObjects ObjectActivatorMask { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Position { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Rotation { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Scale { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> Instances { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single UnkFloat { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 InstanceExtensionValue { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 TriggerArgument1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 TriggerArgument2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 TriggerArgument3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 TriggerArgument4 { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Instances.Clear();
        }

        public override void Import(LabURI package, String? variant, Int32? layoutId)
        {
            ITwinTrigger trigger = GetTwinItem<ITwinTrigger>();
            ObjectActivatorMask = trigger.Trigger.ObjectActivatorMask;
            Position = CloneUtils.Clone(trigger.Trigger.Position);
            Rotation = CloneUtils.Clone(trigger.Trigger.Rotation);
            Scale = CloneUtils.Clone(trigger.Trigger.Scale);
            Instances = new(trigger.Trigger.Instances.Count);
            foreach (var inst in trigger.Trigger.Instances)
            {
                Instances.Add(AssetManager.Get().GetUri(package, typeof(ObjectInstance).Name, variant, layoutId, inst));
            }
            Header = trigger.Trigger.Header;
            UnkFloat = trigger.Trigger.UnkFloat;
            InstanceExtensionValue = trigger.Trigger.InstanceExtensionValue;
            TriggerArgument1 = trigger.TriggerArguments[0];
            TriggerArgument2 = trigger.TriggerArguments[1];
            TriggerArgument3 = trigger.TriggerArguments[2];
            TriggerArgument4 = trigger.TriggerArguments[3];
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);

            var trigger = new TwinTrigger
            {
                Header = Header,
                ObjectActivatorMask = ObjectActivatorMask,
                UnkFloat = UnkFloat,
                Position = Position,
                Rotation = Rotation,
                Scale = Scale,
                InstanceExtensionValue = InstanceExtensionValue
            };
            foreach (var instance in Instances)
            {
                trigger.Instances.Add((UInt16)assetManager.GetAsset(instance).ID);
            }
            trigger.Write(writer);
            writer.Write(TriggerArgument1);
            writer.Write(TriggerArgument2);
            writer.Write(TriggerArgument3);
            writer.Write(TriggerArgument4);

            writer.Flush();
            ms.Position = 0;
            return factory.GenerateTrigger(ms);
        }
    }
}
