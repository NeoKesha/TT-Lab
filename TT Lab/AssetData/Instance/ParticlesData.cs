using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;

namespace TT_Lab.AssetData.Instance
{
    public class ParticlesData : AbstractAssetData
    {
        public ParticlesData()
        {
        }

        public ParticlesData(PS2AnyParticleData particleData) : this()
        {
            Version = particleData.Version;
            ParticleTypes = CloneUtils.DeepClone(particleData.ParticleTypes);
            ParticleInstances = CloneUtils.DeepClone(particleData.ParticleInstances);
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Version { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<ParticleType> ParticleTypes;
        [JsonProperty(Required = Required.Always)]
        public List<ParticleInstance> ParticleInstances;

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
