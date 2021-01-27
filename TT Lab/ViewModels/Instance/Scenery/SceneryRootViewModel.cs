using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance.Scenery;

namespace TT_Lab.ViewModels.Instance.Scenery
{
    public class SceneryRootViewModel : BaseSceneryViewModel
    {
        private UInt32 unkUInt;

        public SceneryRootViewModel(SceneryBaseData data) : base(data)
        {
            var rootData = (SceneryRootData)data;
            unkUInt = rootData.UnkUInt;
        }

        public UInt32 UnkUInt
        {
            get => unkUInt;
            set
            {
                if (unkUInt != value)
                {
                    unkUInt = value;
                    NotifyChange();
                }
            }
        }
    }
}
