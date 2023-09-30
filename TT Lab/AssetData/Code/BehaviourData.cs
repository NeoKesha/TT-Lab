using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.AssetData.Code
{
    public abstract class BehaviourData : AbstractAssetData
    {
        protected BehaviourData()
        {
        }

        protected BehaviourData(ITwinBehaviour script) : this()
        {
            Mask = script.Mask;
        }

        [JsonProperty(Required = Required.Always)]
        public Byte Mask { get; set; }
    }
}
