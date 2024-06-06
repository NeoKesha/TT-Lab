using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels.Editors.Instance;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM;

namespace TT_Lab.Assets.Instance
{
    public class Particles : SerializableInstance
    {
        public override UInt32 Section => Constants.LEVEL_PARTICLES_ITEM;

        public Particles()
        {
        }

        public Particles(LabURI package, UInt32 id, String name, String chunk, ITwinParticle particleData) : base(package, id, name, chunk, null)
        {
            assetData = new ParticleData(particleData);
        }

        public override Type GetEditorType()
        {
            return typeof(ParticlesViewModel);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new ParticleData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
