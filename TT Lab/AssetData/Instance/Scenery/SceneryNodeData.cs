using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;

namespace TT_Lab.AssetData.Instance.Scenery
{
    public class SceneryNodeData : SceneryBaseData
    {
        [JsonProperty(Required = Required.Always)]
        public Int32[] SceneryTypes { get; set; }

        public SceneryNodeData() { }

        public SceneryNodeData(SceneryBaseType baseType) : base(baseType)
        {
            var node = (SceneryNode)baseType;
            SceneryTypes = CloneUtils.CloneArray(node.SceneryTypes);
        }
    }
}
