using Newtonsoft.Json;
using System;
using TT_Lab.Assets;
using TT_Lab.Util;
using TT_Lab.ViewModels.Instance.Scenery;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;

namespace TT_Lab.AssetData.Instance.Scenery
{
    public class SceneryNodeData : SceneryBaseData
    {
        [JsonProperty(Required = Required.Always)]
        public Int32[] SceneryTypes { get; set; }

        public SceneryNodeData() { }

        public SceneryNodeData(LabURI package, String? variant, SceneryBaseType baseType) : base(package, variant, baseType)
        {
            var node = (SceneryNode)baseType;
            SceneryTypes = CloneUtils.CloneArray(node.SceneryTypes);
        }

        public SceneryNodeData(SceneryNodeViewModel vm) : base(vm)
        {
            SceneryTypes = CloneUtils.CloneArray(vm.SceneryTypes);
        }
    }
}
