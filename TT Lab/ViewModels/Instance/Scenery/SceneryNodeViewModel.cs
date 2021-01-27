using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance.Scenery;
using TT_Lab.Util;

namespace TT_Lab.ViewModels.Instance.Scenery
{
    public class SceneryNodeViewModel : BaseSceneryViewModel
    {
        private Int32[] sceneryTypes;

        public SceneryNodeViewModel(SceneryBaseData data) : base(data)
        {
            var nodeData = (SceneryNodeData)data;
            sceneryTypes = CloneUtils.CloneArray(nodeData.SceneryTypes);
        }

        public Int32[] SceneryTypes
        {
            get => sceneryTypes;
        }
    }
}
