using Newtonsoft.Json;
using System;
using TT_Lab.ViewModels.Instance.Scenery;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;

namespace TT_Lab.AssetData.Instance.Scenery
{
    public class SceneryRootData : SceneryNodeData
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkUInt { get; set; }

        public SceneryRootData() { }

        public SceneryRootData(String package, String subpackage, String? variant, SceneryBaseType baseType) : base(package, subpackage, variant, baseType)
        {
            var root = (SceneryRoot)baseType;
            UnkUInt = root.UnkUInt;
        }

        public SceneryRootData(SceneryRootViewModel vm) : base(vm)
        {
            UnkUInt = vm.UnkUInt;
        }
    }
}
