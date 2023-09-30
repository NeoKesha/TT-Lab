using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;
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
        public List<TwinParticleType> ParticleTypes { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<TwinParticleInstance> ParticleInstances { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            ParticleTypes.Clear();
            ParticleInstances.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinParticle particleData = GetTwinItem<ITwinParticle>();
            Version = particleData.Version;
            ParticleTypes = CloneUtils.DeepClone(particleData.ParticleTypes);
            ParticleInstances = CloneUtils.DeepClone(particleData.ParticleInstances);
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
