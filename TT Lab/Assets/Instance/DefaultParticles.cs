using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM;

namespace TT_Lab.Assets.Instance
{
    public class DefaultParticles : Particles
    {
        public DefaultParticles() : base() { }

        public DefaultParticles(LabURI package, UInt32 id, String name, String chunk, ITwinDefaultParticle particleData) : base(package, id, name, chunk, null)
        {
            assetData = new DefaultParticleData(particleData);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new DefaultParticleData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
