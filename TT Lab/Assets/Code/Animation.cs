using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class Animation : SerializableAsset
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 Bitfield { get; set; }

        public override String Type => "Animation";

        public Animation(UInt32 id, String name, PS2AnyAnimation animation) : base(id, name)
        {
            Bitfield = animation.Bitfield;
        }

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
