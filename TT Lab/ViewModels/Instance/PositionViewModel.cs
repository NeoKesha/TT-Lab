using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;

namespace TT_Lab.ViewModels.Instance
{
    public class PositionViewModel : AssetViewModel
    {
        private readonly PositionData posData;

        public PositionViewModel(Guid asset) : base(asset)
        {
        }

        public PositionViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            posData = (PositionData)_asset.GetData();
        }

        public float X
        {
            get => posData.Coords.X;
            set
            {
                posData.Coords.X = value;
                NotifyChange();
            }
        }
        public float Y
        {
            get => posData.Coords.Y;
            set
            {
                posData.Coords.Y = value;
                NotifyChange();
            }
        }
        public float Z
        {
            get => posData.Coords.Z;
            set
            {
                posData.Coords.Z = value;
                NotifyChange();
            }
        }
        public float W
        {
            get => posData.Coords.W;
            set
            {
                posData.Coords.W = value;
                NotifyChange();
            }
        }
    }
}
