using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class InstanceTemplateData : AbstractAssetData
    {
        public InstanceTemplateData()
        {
        }

        public InstanceTemplateData(ITwinTemplate template) : this()
        {
            SetTwinItem(template);
        }

        [JsonProperty(Required = Required.Always)]
        public String TemplateName { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI ObjectId { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkByte1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkByte2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> UnkIds { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Header1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Header2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkByte3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkByte4 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 InstancePropsHeader { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> Flags { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Single> Floats { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> Ints { get; set; }
        protected override void Dispose(Boolean disposing)
        {
            UnkIds.Clear();
            Flags.Clear();
            Floats.Clear();
            Ints.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinTemplate template = GetTwinItem<ITwinTemplate>();
            TemplateName = new String(template.Name.ToCharArray());
            ObjectId = AssetManager.Get().GetUri(package, typeof(GameObject).Name, null, template.ObjectId);
            UnkByte1 = template.UnkByte1;
            UnkByte2 = template.UnkByte2;
            UnkByte3 = template.UnkByte3;
            UnkByte4 = template.UnkByte4;
            Header1 = template.Header1;
            Header2 = template.Header2;
            UnkIds = CloneUtils.CloneList(template.UnkIds);
            InstancePropsHeader = template.InstancePropsHeader;
            UnkInt1 = template.UnkInt1;
            Flags = CloneUtils.CloneList(template.Flags);
            Floats = CloneUtils.CloneList(template.Floats);
            Ints = CloneUtils.CloneList(template.Ints);
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(TemplateName.Length);
            writer.Write(TemplateName.ToCharArray());
            writer.Write((UInt16)assetManager.GetAsset(ObjectId).ID);
            writer.Write(UnkByte1);
            writer.Write(UnkByte2);
            writer.Write(UnkIds.Count);
            writer.Write(Header1);
            writer.Write(Header2);
            foreach (var s in UnkIds)
            {
                writer.Write(s);
            }
            writer.Write(UnkByte3);
            writer.Write(UnkByte4);
            writer.Write(InstancePropsHeader);
            writer.Write(UnkInt1);
            writer.Write(Flags.Count);
            foreach (var flag in Flags)
            {
                writer.Write(flag);
            }
            writer.Write(Floats.Count);
            foreach (var @float in Floats)
            {
                writer.Write(@float);
            }
            writer.Write(Ints.Count);
            foreach (var @int in Ints)
            {
                writer.Write(@int);
            }

            ms.Position = 0;
            return factory.GenerateTemplate(ms);
        }
    }
}
