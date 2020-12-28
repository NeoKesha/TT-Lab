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
        private UInt32 bitfield;

        public AnimationData()
        {
        }

        public AnimationData(PS2AnyAnimation animation) : this()
        {
            Bitfield = animation.Bitfield;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Bitfield
        {
            get => bitfield;

            set
            {
                if (bitfield != value)
                {
                    bitfield = value;
                    NotifyChange("Bitfield");
                }
            }
        }



        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
