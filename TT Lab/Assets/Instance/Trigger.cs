using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class Trigger : SerializableInstance
    {
        [JsonProperty(Required = Required.Always)]
        public Boolean Enabled { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Position { get; set; } = new Vector4();

        public Trigger(UInt32 id, String name, String chunk, Int32 layId, PS2AnyTrigger trigger) : base(id, name, chunk, layId)
        {
            Enabled = trigger.Trigger.Enabled > 0;
            Position = trigger.Trigger.Position;
        }

        public Trigger()
        {
        }

        protected override String SavePath => base.SavePath + "Trigger";

        public override String Type => "Trigger";

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
