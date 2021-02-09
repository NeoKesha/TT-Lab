using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.AssetData.Instance.Collision
{
    public class CollisionTrigger
    {
        [JsonProperty(Required = Required.Always)]
        Vector3 V1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        Vector3 V2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 MinTriggerIndex { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 MaxTriggerIndex { get; set; }

        public CollisionTrigger() { }

        public CollisionTrigger(Twinsanity.TwinsanityInterchange.Common.Collision.CollisionTrigger trigger)
        {
            V1 = CloneUtils.Clone(trigger.V1);
            V2 = CloneUtils.Clone(trigger.V2);
            MinTriggerIndex = trigger.MinTriggerIndex;
            MaxTriggerIndex = trigger.MaxTriggerIndex;
        }
    }
}
