using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.Particles;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM;

namespace TT_Lab.AssetData.Instance
{
    public class ParticleData : AbstractAssetData
    {
        public ParticleData()
        {
            Version = 0x1E;
        }

        public ParticleData(ITwinParticle particleData) : this()
        {
            SetTwinItem(particleData);
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Version { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<TwinParticleSystem> ParticleSystems { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<TwinParticleEmitter> ParticleEmitters { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            ParticleSystems.Clear();
            ParticleEmitters.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinParticle particleData = GetTwinItem<ITwinParticle>();
            Version = particleData.Version;
            ParticleSystems = CloneUtils.DeepClone(particleData.ParticleSystems);
            ParticleEmitters = CloneUtils.DeepClone(particleData.ParticleEmitters);
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
