using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
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
            twinRef = trigger;
        }

        public TriggerData(TwinTrigger trigger) : this()
        {
            Enabled = trigger.Enabled > 0;
            Position = CloneUtils.Clone(trigger.Position);
            Rotation = CloneUtils.Clone(trigger.Rotation);
            Scale = CloneUtils.Clone(trigger.Scale);
            Instances = CloneUtils.CloneList(trigger.Instances);
            Header1 = trigger.Header1;
            HeaderT = trigger.HeaderT;
            HeaderH = trigger.HeaderH;
        }

        [JsonProperty(Required = Required.Always)]
        public Boolean Enabled { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Position { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Rotation { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Scale { get; set; }
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
            Instances.Clear();
        }

        public override void Import()
        {
            PS2AnyTrigger trigger = (PS2AnyTrigger)twinRef;
            Enabled = trigger.Trigger.Enabled > 0;
            Position = CloneUtils.Clone(trigger.Trigger.Position);
            Rotation = CloneUtils.Clone(trigger.Trigger.Rotation);
            Scale = CloneUtils.Clone(trigger.Trigger.Scale);
            Instances = CloneUtils.CloneList(trigger.Trigger.Instances);
            Header1 = trigger.Trigger.Header1;
            HeaderT = trigger.Trigger.HeaderT;
            HeaderH = trigger.Trigger.HeaderH;
        }
    }
}
