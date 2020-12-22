using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.AssetData.Instance.Collision
{
    public class CollisionTrigger
    {
        [JsonProperty(Required = Required.Always)]
        Vector3 V1;
        [JsonProperty(Required = Required.Always)]
        Vector3 V2;
        [JsonProperty(Required = Required.Always)]
        public Int32 MinTriggerIndex;
        [JsonProperty(Required = Required.Always)]
        public Int32 MaxTriggerIndex;

        public CollisionTrigger() { }

        public CollisionTrigger(Twinsanity.TwinsanityInterchange.Common.Collision.CollisionTrigger trigger)
        {
            V1 = trigger.V1;
            V2 = trigger.V2;
            MinTriggerIndex = trigger.MinTriggerIndex;
            MaxTriggerIndex = trigger.MaxTriggerIndex;
        }
    }
}
