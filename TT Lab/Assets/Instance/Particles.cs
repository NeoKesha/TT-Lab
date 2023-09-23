using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;

namespace TT_Lab.Assets.Instance
{
    public class Particles : SerializableInstance
    {
        public Particles()
        {
        }

        public Particles(String package, String subpackage, UInt32 id, String name, String chunk, PS2AnyParticleData particleData) : base(package, subpackage, id, name, chunk, null)
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

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override AssetViewModel GetViewModel(AssetViewModel? parent = null)
        {
            viewModel ??= new AssetViewModel(URI, parent);
            return viewModel;
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new ParticlesData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
