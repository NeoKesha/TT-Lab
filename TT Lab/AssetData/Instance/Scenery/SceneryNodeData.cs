using Newtonsoft.Json;
using System;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Util;
using TT_Lab.ViewModels.Instance.Scenery;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.AssetData.Instance.Scenery
{
    public class SceneryNodeData : SceneryBaseData
    {
        [JsonProperty(Required = Required.Always)]
        public ITwinScenery.SceneryType[] SceneryTypes { get; set; }

        public SceneryNodeData() { }

        public SceneryNodeData(LabURI package, String? variant, TwinSceneryBaseType baseType) : base(package, variant, baseType)
        {
            var node = (TwinSceneryNode)baseType;
            SceneryTypes = CloneUtils.DeepClone(node.SceneryTypes);
        }

        public SceneryNodeData(SceneryNodeViewModel vm) : base(vm)
        {
            SceneryTypes = CloneUtils.DeepClone(vm.SceneryTypes);
        }

        public override ITwinScenery.SceneryType GetSceneryType()
        {
            return ITwinScenery.SceneryType.Node;
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            foreach (var type in SceneryTypes)
            {
                writer.Write((Int32)type);
            }
        }
    }
}
