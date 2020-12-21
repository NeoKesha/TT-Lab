using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;

namespace TT_Lab.Assets.Instance
{
    public class DynamicScenery : SerializableInstance
    {
        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt;

        public DynamicScenery()
        {
        }

        public DynamicScenery(UInt32 id, String name, String chunk, PS2AnyDynamicScenery dynamicScenery) : base(id, name, chunk, -1)
        {
            UnkInt = dynamicScenery.UnkInt;
        }

        public override String Type => "DynamicScenery";

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
