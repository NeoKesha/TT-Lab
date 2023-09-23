using System;
using TT_Lab.ViewModels.Instance.Scenery;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;

namespace TT_Lab.AssetData.Instance.Scenery
{
    public class SceneryLeafData : SceneryBaseData
    {
        public SceneryLeafData() { }
        public SceneryLeafData(String package, String subpackage, String? variant, SceneryBaseType baseType) : base(package, subpackage, variant, baseType)
        {
        }
        public SceneryLeafData(BaseSceneryViewModel vm) : base(vm) { }
    }
}
