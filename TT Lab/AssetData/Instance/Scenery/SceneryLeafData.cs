using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.ViewModels.Instance.Scenery;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;

namespace TT_Lab.AssetData.Instance.Scenery
{
    public class SceneryLeafData : SceneryBaseData
    {
        public SceneryLeafData() { }
        public SceneryLeafData(SceneryBaseType baseType) : base(baseType)
        {
        }
        public SceneryLeafData(BaseSceneryViewModel vm) : base(vm) { }
    }
}
