﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ObjectTriggerBehaviourData(LabURI package, String? variant, TwinObjectTriggerBehaviour triggerBehaviour)
        {
            TriggerBehaviour = AssetManager.Get().GetUri(package, typeof(BehaviourStarter).Name, null, triggerBehaviour.TriggerBehaviour);
            UnkTriggerValue = triggerBehaviour.UnkTriggerValue;
            BehaviourCallerIndex = triggerBehaviour.BehaviourCallerIndex;
        }
    }
}