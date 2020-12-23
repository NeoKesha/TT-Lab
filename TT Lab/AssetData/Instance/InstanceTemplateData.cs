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
            TemplateName = template.Name.Substring(0);
            ObjectId = template.ObjectId;
        }

        [JsonProperty(Required = Required.Always)]
        public String TemplateName { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 ObjectId { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
