using System;
using System.Collections.ObjectModel;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Instance.Particles;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.Particles;

namespace TT_Lab.ViewModels.Instance
{
    public class ParticlesViewModel : AssetViewModel
    {
        private UInt32 version;

        public ParticlesViewModel(LabURI asset, AssetViewModel? parent) : base(asset, parent)
        {
            ParticleTypes = new ObservableCollection<ParticleTypeViewModel>();
            ParticleInstances = new ObservableCollection<ParticleInstanceViewModel>();
            var data = _asset.GetData<ParticleData>();
            foreach (var type in data.ParticleSystems)
            {
                ParticleTypes.Add(new ParticleTypeViewModel(type));
            }
            foreach (var inst in data.ParticleEmitters)
            {
                ParticleInstances.Add(new ParticleInstanceViewModel(inst));
            }
        }

        public override void Save(Object? o)
        {
            var data = _asset.GetData<ParticleData>();
            data.Version = Version;
            data.ParticleSystems.Clear();
            foreach (var type in ParticleTypes)
            {
                var t = new TwinParticleSystem();
                type.Save(t);
                data.ParticleSystems.Add(t);
            }
            data.ParticleEmitters.Clear();
            foreach (var inst in ParticleInstances)
            {
                var i = new TwinParticleEmitter();
                inst.Save(i);
                data.ParticleEmitters.Add(i);
            }
            base.Save(o);
        }

        public UInt32 Version
        {
            get
            {
                return version;
            }
            set
            {
                if (value != version)
                {
                    version = value;
                    NotifyChange();
                }
            }
        }
        public ObservableCollection<ParticleTypeViewModel> ParticleTypes
        {
            get;
            private set;
        }
        public ObservableCollection<ParticleInstanceViewModel> ParticleInstances
        {
            get;
            private set;
        }
    }
}
