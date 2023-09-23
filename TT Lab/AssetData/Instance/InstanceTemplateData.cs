﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class InstanceTemplateData : AbstractAssetData
    {
        public InstanceTemplateData()
        {
        }

        public InstanceTemplateData(PS2AnyTemplate template) : this()
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
            return;
        }

        public override void Import(String package, String subpackage, String? variant)
        {
            PS2AnyTemplate template = GetTwinItem<PS2AnyTemplate>();
            TemplateName = new String(template.Name.ToCharArray());
            ObjectId = AssetManager.Get().GetUri(package, subpackage, typeof(GameObject).Name, variant, template.ObjectId);
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
    }
}
