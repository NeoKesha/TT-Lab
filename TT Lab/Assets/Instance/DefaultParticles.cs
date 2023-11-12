using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
