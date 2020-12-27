using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            TemplateName = new String(template.Name.ToCharArray());
            ObjectId = template.ObjectId;
            Bitfield = template.Bitfield;
            Header1 = template.Header1;
            Header2 = template.Header2;
            Header3 = template.Header3;
            UnkShort = template.UnkShort;
            UnkFlags = CloneUtils.CloneArray(template.UnkFlags);
            Properties = template.Properties;
            Flags = CloneUtils.CloneList(template.Flags);
            Floats = CloneUtils.CloneList(template.Floats);
            Ints = CloneUtils.CloneList(template.Ints);
        }

        [JsonProperty(Required = Required.Always)]
        public String TemplateName { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 ObjectId { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 Bitfield { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Header1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Header2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Header3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkShort { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte[] UnkFlags { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Properties { get; set; }
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
    }
}
