using System;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Instance
{
    public class PositionViewModel : AssetViewModel
    {
        private Vector4ViewModel position;
        private Enums.Layouts layId;

        public PositionViewModel(LabURI asset) : base(asset)
        {
        }

        public PositionViewModel(LabURI asset, AssetViewModel? parent) : base(asset, parent)
        {
            var posData = _asset.GetData<PositionData>();
            position = new Vector4ViewModel(posData.Coords);
            position.PropertyChanged += Position_PropertyChanged;
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
            var data = _asset.GetData<PositionData>();
            data.Coords = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = Position.X,
                Y = Position.Y,
                Z = Position.Z,
                W = Position.W
            };
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
