using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class AnimationData : AbstractAssetData
    {
        public AnimationData()
        {
        }

        public AnimationData(PS2AnyAnimation animation) : this()
        {
            Bitfield = animation.Bitfield;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Bitfield { get; set; }

        

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
