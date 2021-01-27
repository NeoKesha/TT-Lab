using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;

namespace TT_Lab.AssetData.Instance.Scenery
{
    public class SceneryRootData : SceneryBaseData
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkUInt { get; set; }

        public SceneryRootData() { }

        public SceneryRootData(SceneryBaseType baseType) : base(baseType)
        {
            var root = (SceneryRoot)baseType;
            UnkUInt = root.UnkUInt;
        }
    }
}
