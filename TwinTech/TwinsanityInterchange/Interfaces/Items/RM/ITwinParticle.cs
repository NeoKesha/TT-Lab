using System.Collections.Generic;
using System;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM
{
    public interface ITwinParticle : ITwinItem
    {
        UInt32 Version { get; set; }
        List<TwinParticleType> ParticleTypes { get; set; }
        List<TwinParticleInstance> ParticleInstances { get; set; }
    }
}
