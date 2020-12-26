﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;
using Twinsanity.Libraries;

namespace TT_Lab.AssetData.Instance
{
    public class TriggerData : AbstractAssetData
    {
        public TriggerData()
        {
        }

        public TriggerData(PS2AnyTrigger trigger) : this()
        {
            Enabled = trigger.Trigger.Enabled > 0;
            Position = CloneUtils.DeepClone(trigger.Trigger.Position);
            Rotation = CloneUtils.DeepClone(trigger.Trigger.Rotation);
            Scale = CloneUtils.DeepClone(trigger.Trigger.Scale);
            Instances = new List<UInt16>(trigger.Trigger.Instances);
            Header1 = trigger.Trigger.Header1;
            HeaderT = trigger.Trigger.HeaderT;
            HeaderH = trigger.Trigger.HeaderH;
        }

        [JsonProperty(Required = Required.Always)]
        public Boolean Enabled { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Position { get; set; } = new Vector4();
        [JsonProperty(Required = Required.Always)]
        public Vector4 Rotation { get; set; } = new Vector4();
        [JsonProperty(Required = Required.Always)]
        public Vector4 Scale { get; set; } = new Vector4();
        [JsonProperty(Required = Required.Always)]
        public List<UInt16> Instances { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Header1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Single HeaderT { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 HeaderH { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
