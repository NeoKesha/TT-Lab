using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;

namespace TT_Lab.AssetData.Instance
{
    public class ParticlesData : AbstractAssetData
    {
        public ParticlesData()
        {
            Version = 0x1E;
        }

        public ParticlesData(PS2AnyParticleData particleData) : this()
        {
            SetTwinItem(particleData);
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Version { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<ParticleType> ParticleTypes { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<ParticleInstance> ParticleInstances { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            ParticleTypes.Clear();
            ParticleInstances.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            PS2AnyParticleData particleData = GetTwinItem<PS2AnyParticleData>();
            Version = particleData.Version;
            ParticleTypes = CloneUtils.DeepClone(particleData.ParticleTypes);
            ParticleInstances = CloneUtils.DeepClone(particleData.ParticleInstances);
        }
    }
}
