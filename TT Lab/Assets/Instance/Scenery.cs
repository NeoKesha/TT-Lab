using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;

namespace TT_Lab.Assets.Instance
{
    public class Scenery : SerializableInstance
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 Flags;
        [JsonProperty(Required = Required.Always)]
        public String SceneryName;
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkUInt;
        [JsonProperty(Required = Required.Always)]
        public Byte UnkByte;
        [JsonProperty(Required = Required.AllowNull)]
        public UInt32 SkydomeID;

        public Scenery()
        {
        }

        public Scenery(UInt32 id, String name, String chunk, PS2AnyScenery scenery) : base(id, name, chunk, -1)
        {
            Flags = scenery.Flags;
            // Clone the name instead of reference copy
            SceneryName = scenery.Name.Substring(0);
            UnkUInt = scenery.UnkUInt;
            UnkByte = scenery.UnkByte;
            SkydomeID = scenery.SkydomeID;
        }

        public override String Type => "Scenery";

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
