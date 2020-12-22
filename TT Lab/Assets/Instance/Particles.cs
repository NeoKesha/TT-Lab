using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;

namespace TT_Lab.Assets.Instance
{
    public class Particles : SerializableInstance<ParticlesData>
    {
        public override String Type => "ParticleData";

        public Particles()
        {
        }

        public Particles(UInt32 id, String name, String chunk, PS2AnyParticleData particleData) : base(id, name, chunk, null)
        {
            assetData = new ParticlesData(particleData);
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
