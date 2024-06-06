using Caliburn.Micro;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Editors.Instance.Particles;
using Twinsanity.TwinsanityInterchange.Common.Particles;

namespace TT_Lab.ViewModels.Editors.Instance
{
    public class ParticlesViewModel : InstanceSectionResourceEditorViewModel
    {
        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<ParticleData>();
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
        }

        public override void LoadData()
        {
            ParticleTypes = new BindableCollection<ParticleTypeViewModel>();
            ParticleInstances = new BindableCollection<ParticleInstanceViewModel>();
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<ParticleData>();
            foreach (var type in data.ParticleSystems)
            {
                ParticleTypes.Add(new ParticleTypeViewModel(type));
            }
            foreach (var inst in data.ParticleEmitters)
            {
                ParticleInstances.Add(new ParticleInstanceViewModel(inst));
            }
        }

        public BindableCollection<ParticleTypeViewModel> ParticleTypes
        {
            get;
            private set;
        }

        public BindableCollection<ParticleInstanceViewModel> ParticleInstances
        {
            get;
            private set;
        }
    }
}
