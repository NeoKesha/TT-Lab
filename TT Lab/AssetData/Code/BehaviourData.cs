using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public abstract class BehaviourData : AbstractAssetData
    {
        protected BehaviourData()
        {
        }

        protected BehaviourData(PS2BehaviourWrapper script) : this()
        {
            Mask = script.Mask;
        }

        [JsonProperty(Required = Required.Always)]
        public Byte Mask { get; set; }
    }
}
