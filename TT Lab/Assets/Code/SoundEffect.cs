using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffect : SerializableAsset
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkFlag { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte FreqFac { get; set; }

        public override String Type => "SoundEffect";

        public SoundEffect(UInt32 id, String name, PS2AnySound sound) : base(id, name)
        {
            Header = sound.Header;
            UnkFlag = sound.UnkFlag;
            FreqFac = sound.FreqFac;
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
