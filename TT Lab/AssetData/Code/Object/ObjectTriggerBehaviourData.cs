using Newtonsoft.Json;
using System;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.AssetData.Code.Object
{
    public class ObjectTriggerBehaviourData
    {
        [JsonProperty(Required = Required.Always)]
        public LabURI TriggerBehaviour { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 UnkTriggerValue { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte BehaviourCallerIndex { get; set; }

        public ObjectTriggerBehaviourData() { }

        public ObjectTriggerBehaviourData(LabURI package, String? variant, TwinObjectTriggerBehaviour triggerBehaviour)
        {
            TriggerBehaviour = AssetManager.Get().GetUri(package, typeof(BehaviourStarter).Name, variant, triggerBehaviour.TriggerBehaviour);
            UnkTriggerValue = triggerBehaviour.UnkTriggerValue;
            BehaviourCallerIndex = triggerBehaviour.BehaviourCallerIndex;
        }
    }
}
