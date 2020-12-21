using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;

namespace TT_Lab.Assets.Instance
{
    public class ParticleData : SerializableInstance
    {

        [JsonProperty(Required = Required.Always)]
        public UInt32 Version { get; set; }

        public override String Type => "ParticleData";

        public ParticleData()
        {
        }

        public ParticleData(UInt32 id, String name, String chunk, PS2AnyParticleData particleData) : base(id, name, chunk, -1)
        {
            Version = particleData.Version;
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
