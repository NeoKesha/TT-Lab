using System;
using System.Collections.ObjectModel;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Instance.Particles;
using Twinsanity.TwinsanityInterchange.Common;

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
            foreach (var type in data.ParticleTypes)
            {
                ParticleTypes.Add(new ParticleTypeViewModel(type));
            }
            foreach (var inst in data.ParticleInstances)
            {
                ParticleInstances.Add(new ParticleInstanceViewModel(inst));
            }
        }

        public override void Save(Object? o)
        {
            var data = _asset.GetData<ParticleData>();
            data.Version = Version;
            data.ParticleTypes.Clear();
            foreach (var type in ParticleTypes)
            {
                var t = new TwinParticleType();
                type.Save(t);
                data.ParticleTypes.Add(t);
            }
            data.ParticleInstances.Clear();
            foreach (var inst in ParticleInstances)
            {
                var i = new TwinParticleInstance();
                inst.Save(i);
                data.ParticleInstances.Add(i);
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
