using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.AssetData.Code.Behaviour
{
    public abstract class BehaviourData : AbstractAssetData
    {
        protected BehaviourData()
        {
        }

        protected BehaviourData(ITwinBehaviour script) : this()
        {
            ID = script.GetID();
            Priority = script.Priority;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 ID { get; private set; }
        [JsonProperty(Required = Required.Always)]
        public Byte Priority { get; set; }
    }
}
