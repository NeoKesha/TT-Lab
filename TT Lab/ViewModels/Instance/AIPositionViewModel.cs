using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Instance
{
    public class AIPositionViewModel : AssetViewModel
    {
        private Vector4ViewModel position;
        private UInt16 arg;
        private Enums.Layouts layId;

        public AIPositionViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var posData = (AiPositionData)_asset.GetData();
            position = new Vector4ViewModel(posData.Coords);
            position.PropertyChanged += Position_PropertyChanged;
            arg = posData.Arg;
            layId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);
        }

        private void Position_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(Position));
        }

        public override void Save(object? o)
        {
            _asset.LayoutID = (int)LayoutID;
            var data = (AiPositionData)_asset.GetData();
            Position.Save(data.Coords);
            data.Arg = Argument;
            base.Save(o);
        }

        public Enums.Layouts LayoutID
        {
            get => layId;
            set
            {
                if (layId != value)
                {
                    layId = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }

        public UInt16 Argument
        {
            get => arg;
            set
            {
                if (arg != value)
                {
                    arg = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }

        public Vector4ViewModel Position
        {
            get => position;
            set
            {
                if (position != value)
                {
                    position = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
    }
}
