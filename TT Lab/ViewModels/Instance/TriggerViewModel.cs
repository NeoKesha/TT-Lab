using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Command;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Instance
{
    public class TriggerViewModel : AssetViewModel
    {
        private Boolean _enabled;
        private Vector4ViewModel _position;
        private Vector4ViewModel _rotation;
        private Vector4ViewModel _scale;
        private ObservableCollection<UInt16> _instances;
        private UInt32 _header1;
        private Single _headerT;
        private UInt32 _headerH;
        private Enums.Layouts _layId;

        public TriggerViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var data = (TriggerData)_asset.GetData();
            _enabled = data.Enabled;
            _instances = new ObservableCollection<UInt16>();
            foreach (var inst in data.Instances)
            {
                _instances.Add(inst);
            }
            _position = new Vector4ViewModel(data.Position);
            _rotation = new Vector4ViewModel(data.Rotation);
            _scale = new Vector4ViewModel(data.Scale);
            _position.PropertyChanged += _vector_PropertyChanged;
            _rotation.PropertyChanged += _vector_PropertyChanged;
            _scale.PropertyChanged += _vector_PropertyChanged;
            _header1 = data.Header1;
            _headerT = data.HeaderT;
            _headerH = data.HeaderH;
            _layId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);
            AddInstanceToListCommand = new AddItemToListCommand<ushort>(_instances, typeof(UInt16));
            DeleteInstanceFromListCommand = new DeleteItemFromListCommand(_instances);
        }

        public TriggerViewModel(Guid asset, AssetViewModel parent, TriggerData data) : base(asset, parent)
        {
            _enabled = data.Enabled;
            _instances = new ObservableCollection<UInt16>();
            foreach (var inst in data.Instances)
            {
                _instances.Add(inst);
            }
            _position = new Vector4ViewModel(data.Position);
            _rotation = new Vector4ViewModel(data.Rotation);
            _scale = new Vector4ViewModel(data.Scale);
            _position.PropertyChanged += _vector_PropertyChanged;
            _rotation.PropertyChanged += _vector_PropertyChanged;
            _scale.PropertyChanged += _vector_PropertyChanged;
            _header1 = data.Header1;
            _headerT = data.HeaderT;
            _headerH = data.HeaderH;
            _layId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);
            AddInstanceToListCommand = new AddItemToListCommand<ushort>(_instances, typeof(UInt16));
            DeleteInstanceFromListCommand = new DeleteItemFromListCommand(_instances);
        }

        public override void Save(object? o)
        {
            _asset.LayoutID = (int)LayoutID;
            var data = (TriggerData)_asset.GetData();
            Save(data);
            base.Save(o);
        }
        public void Save(TriggerData data)
        {
            data.Enabled = Enabled;
            data.Header1 = Header1;
            data.HeaderH = HeaderH;
            data.HeaderT = HeaderT;
            data.Position = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = Position.X,
                Y = Position.Y,
                Z = Position.Z,
                W = Position.W
            };
            data.Scale = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = Scale.X,
                Y = Scale.Y,
                Z = Scale.Z,
                W = Scale.W
            };
            data.Rotation = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = Rotation.X,
                Y = Rotation.Y,
                Z = Rotation.Z,
                W = Rotation.W
            };
            data.Instances.Clear();
            foreach (var inst in Instances)
            {
                data.Instances.Add(inst);
            }
        }

        private void _vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(Position));
            NotifyChange(nameof(Scale));
            NotifyChange(nameof(Rotation));
        }

        public AddItemToListCommand<UInt16> AddInstanceToListCommand { get; private set; }
        public DeleteItemFromListCommand DeleteInstanceFromListCommand { get; private set; }
        public Enums.Layouts LayoutID
        {
            get => _layId;
            set
            {
                if (_layId != value)
                {
                    _layId = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Boolean Enabled
        {
            get => _enabled;
            set
            {
                if (value != _enabled)
                {
                    _enabled = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel Position
        {
            get => _position;
            set
            {
                if (value != _position)
                {
                    _position = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel Rotation
        {
            get => _rotation;
            set
            {
                if (value != _rotation)
                {
                    _rotation = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel Scale
        {
            get => _scale;
            set
            {
                if (value != _scale)
                {
                    _scale = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public ObservableCollection<UInt16> Instances
        {
            get => _instances;
            private set
            {
                _instances = value;
                NotifyChange();
            }
        }
        public UInt32 Header1
        {
            get => _header1;
            set
            {
                if (value != _header1)
                {
                    _header1 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Single HeaderT
        {
            get => _headerT;
            set
            {
                if (value != _headerT)
                {
                    _headerT = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt32 HeaderH
        {
            get => _headerH;
            set
            {
                if (value != _headerH)
                {
                    _headerH = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
    }
}
