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
            twinRef = animation;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Bitfield;

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
            PS2AnyAnimation animation = (PS2AnyAnimation)twinRef;
            Bitfield = animation.Bitfield;
        }
    }
}
