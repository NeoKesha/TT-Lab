using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;

namespace TT_Lab.AssetData.Instance
{
    public class SceneryData : AbstractAssetData
    {
        public SceneryData()
        {
        }

        public SceneryData(PS2AnyScenery scenery) : this()
        {
            Flags = scenery.Flags;
            SceneryName = scenery.Name.Substring(0);
            UnkUInt = scenery.UnkUInt;
            UnkByte = scenery.UnkByte;
            SkydomeID = scenery.SkydomeID;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Flags;
        [JsonProperty(Required = Required.Always)]
        public String SceneryName;
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkUInt;
        [JsonProperty(Required = Required.Always)]
        public Byte UnkByte;
        [JsonProperty(Required = Required.AllowNull)]
        public UInt32? SkydomeID;

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
