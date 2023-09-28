using System;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Instance.Scenery;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;

namespace TT_Lab.AssetData.Instance.Scenery
{
    public class SceneryLeafData : SceneryBaseData
    {
        public SceneryLeafData() { }
        public SceneryLeafData(LabURI package, String? variant, SceneryBaseType baseType) : base(package, variant, baseType)
        {
        }
        public SceneryLeafData(BaseSceneryViewModel vm) : base(vm) { }
    }
}
