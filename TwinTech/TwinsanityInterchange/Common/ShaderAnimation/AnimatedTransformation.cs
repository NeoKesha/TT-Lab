using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.ShaderAnimation
{
    public class AnimatedTransformation : Animation.AnimatedTransformation
    { 
        public AnimatedTransformation(UInt16 amount) : base(amount) { }
    }
}
