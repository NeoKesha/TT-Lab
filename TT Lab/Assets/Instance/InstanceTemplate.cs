using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class InstanceTemplate : SerializableInstance
    {

        [JsonProperty(Required = Required.Always)]
        public String TemplateName { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 ObjectId { get; set; }

        public InstanceTemplate(UInt32 id, String name, String chunk, Int32 layId, PS2AnyTemplate template) : base(id, name, chunk, layId)
        {
            TemplateName = template.Name;
            ObjectId = template.ObjectId;
        }

        public override String Type => base.Type + "InstanceTemplate";

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
