using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            ParticleSystems = new();
            ParticleEmitters = new();
        }

        public ParticleData(ITwinParticle particleData) : this()
        {
            SetTwinItem(particleData);
        }

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
            ParticleSystems = CloneUtils.DeepClone(particleData.ParticleSystems);
            ParticleEmitters = CloneUtils.DeepClone(particleData.ParticleEmitters);
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(0x1E);
            writer.Write(ParticleSystems.Count);
            foreach (var system in ParticleSystems)
            {
                system.Write(writer);
            }

            writer.Write(ParticleEmitters.Count);
            foreach (var emitter in ParticleEmitters)
            {
                emitter.Write(writer);
            }

            return factory.GenerateParticle(ms);
        }
    }
}
