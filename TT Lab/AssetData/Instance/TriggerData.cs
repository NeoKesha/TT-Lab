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

        public TriggerData(LabURI package, String? variant, TwinTrigger trigger) : this()
        {
            ObjectActivatorMask = trigger.ObjectActivatorMask;
            Position = CloneUtils.Clone(trigger.Position);
            Rotation = CloneUtils.Clone(trigger.Rotation);
            Scale = CloneUtils.Clone(trigger.Scale);
            Instances = new(trigger.Instances.Count);
            foreach (var inst in trigger.Instances)
            {
                Instances.Add(AssetManager.Get().GetUri(package, typeof(ObjectInstance).Name, variant, inst));
            }
            Header = trigger.Header;
            UnkFloat = trigger.UnkFloat;
            InstanceExtensionValue = trigger.InstanceExtensionValue;
            TriggerScript1 = LabURI.Empty;
            TriggerScript2 = LabURI.Empty;
            TriggerScript3 = LabURI.Empty;
            TriggerScript4 = LabURI.Empty;
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
        public LabURI TriggerScript1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI TriggerScript2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI TriggerScript3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI TriggerScript4 { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Instances.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinTrigger trigger = GetTwinItem<ITwinTrigger>();
            ObjectActivatorMask = trigger.Trigger.ObjectActivatorMask;
            Position = CloneUtils.Clone(trigger.Trigger.Position);
            Rotation = CloneUtils.Clone(trigger.Trigger.Rotation);
            Scale = CloneUtils.Clone(trigger.Trigger.Scale);
            Instances = new(trigger.Trigger.Instances.Count);
            foreach (var inst in trigger.Trigger.Instances)
            {
                Instances.Add(AssetManager.Get().GetUri(package, typeof(ObjectInstance).Name, variant, inst));
            }
            Header = trigger.Trigger.Header;
            UnkFloat = trigger.Trigger.UnkFloat;
            InstanceExtensionValue = trigger.Trigger.InstanceExtensionValue;
            TriggerScript1 = AssetManager.Get().GetUri(package, typeof(BehaviourGraph).Name, null, trigger.TriggerScripts[0]);
            TriggerScript2 = AssetManager.Get().GetUri(package, typeof(BehaviourGraph).Name, null, trigger.TriggerScripts[1]);
            TriggerScript3 = AssetManager.Get().GetUri(package, typeof(BehaviourGraph).Name, null, trigger.TriggerScripts[2]);
            TriggerScript4 = AssetManager.Get().GetUri(package, typeof(BehaviourGraph).Name, null, trigger.TriggerScripts[3]);
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
            writer.Write(TriggerScript1 == LabURI.Empty ? UInt16.MaxValue : (UInt16)assetManager.GetAsset(TriggerScript1).ID);
            writer.Write(TriggerScript2 == LabURI.Empty ? UInt16.MaxValue : (UInt16)assetManager.GetAsset(TriggerScript2).ID);
            writer.Write(TriggerScript3 == LabURI.Empty ? UInt16.MaxValue : (UInt16)assetManager.GetAsset(TriggerScript3).ID);
            writer.Write(TriggerScript4 == LabURI.Empty ? UInt16.MaxValue : (UInt16)assetManager.GetAsset(TriggerScript4).ID);

            ms.Position = 0;
            return factory.GenerateTrigger(ms);
        }
    }
}
