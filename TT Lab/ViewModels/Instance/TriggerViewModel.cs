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
        private Enums.TriggerActivatorObjects _objActivatorMask;
        private Vector4ViewModel _position;
        private Vector4ViewModel _rotation;
        private Vector4ViewModel _scale;
        private ObservableCollection<UInt16> _instances;
        private UInt32 _header;
        private Single _unkFloat;
        private Enums.Layouts _layId;
        private UInt16 _arg1;
        private UInt16 _arg2;
        private UInt16 _arg3;
        private UInt16 _arg4;

        public TriggerViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var data = (TriggerData)_asset.GetData();
            _objActivatorMask = MiscUtils.ConvertEnum<Enums.TriggerActivatorObjects>(data.ObjectActivatorMask);
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
            _header = data.Header;
            _unkFloat = data.UnkFloat;
            _arg1 = data.Arg1;
            _arg2 = data.Arg2;
            _arg3 = data.Arg3;
            _arg4 = data.Arg4;
            _layId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);
            DeleteInstanceFromListCommand = new DeleteItemFromListCommand(_instances);
        }

        public TriggerViewModel(Guid asset, AssetViewModel parent, TriggerData data) : base(asset, parent)
        {
            _objActivatorMask = MiscUtils.ConvertEnum<Enums.TriggerActivatorObjects>(data.ObjectActivatorMask);
            _instances = new ObservableCollection<UInt16>();
            _instances.CollectionChanged += _instances_CollectionChanged;
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
            _header = data.Header;
            _unkFloat = data.UnkFloat;
            _arg1 = data.Arg1;
            _arg2 = data.Arg2;
            _arg3 = data.Arg3;
            _arg4 = data.Arg4;
            _layId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);
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
            data.ObjectActivatorMask = (UInt32)ObjectActivatorMask;
            data.Header = Header;
            data.UnkFloat = UnkFloat;
            Position.Save(data.Position);
            Scale.Save(data.Scale);
            Rotation.Save(data.Rotation);
            data.Instances.Clear();
            foreach (var inst in Instances)
            {
                data.Instances.Add(inst);
            }
            data.Arg1 = Arg1;
            data.Arg2 = Arg2;
            data.Arg3 = Arg3;
            data.Arg4 = Arg4;
        }

        private void _vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(Position));
            NotifyChange(nameof(Scale));
            NotifyChange(nameof(Rotation));
        }

        private void _instances_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(Instances));
        }

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
        public Enums.TriggerActivatorObjects ObjectActivatorMask
        {
            get => _objActivatorMask;
            set
            {
                if (value != _objActivatorMask)
                {
                    _objActivatorMask = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Boolean ActivateByPlayableCharacter
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.PlayableCharacter);
            set
            {
                if (value != ActivateByPlayableCharacter)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.PlayableCharacter, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByCollectibles
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Collectibles);
            set
            {
                if (value != ActivateByCollectibles)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Collectibles, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByCrates
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Crates);
            set
            {
                if (value != ActivateByCrates)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Crates, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByType3
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Type3Objects);
            set
            {
                if (value != ActivateByType3)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Type3Objects, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByType4
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Type4Objects);
            set
            {
                if (value != ActivateByType4)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Type4Objects, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByType5
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Type5Objects);
            set
            {
                if (value != ActivateByType5)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Type5Objects, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByType6
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Type6Objects);
            set
            {
                if (value != ActivateByType6)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Type6Objects, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByType7
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Type7Objects);
            set
            {
                if (value != ActivateByType7)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Type7Objects, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByPlayerProjectiles
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.PlayerProjectiles);
            set
            {
                if (value != ActivateByPlayerProjectiles)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.PlayerProjectiles, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
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
        public UInt32 Header
        {
            get => _header;
            set
            {
                if (value != _header)
                {
                    _header = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Boolean Arg1Enabled
        {
            get => (_header >> 0xB & 0x1) != 0;
            set
            {
                if (value != Arg1Enabled)
                {
                    if (value)
                    {
                        _header |= (1 << 0xB);
                    }
                    else
                    {
                        var mask = ~(1 << 0xB);
                        _header &= (UInt32)mask;
                    }
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(Header));
                }
            }
        }
        public Boolean Arg2Enabled
        {
            get => (_header >> 0x8 & 0x1) != 0;
            set
            {
                if (value != Arg2Enabled)
                {
                    if (value)
                    {
                        _header |= (1 << 0x8);
                    }
                    else
                    {
                        var mask = ~(1 << 0x8);
                        _header &= (UInt32)mask;
                    }
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(Header));
                }
            }
        }
        public Boolean Arg3Enabled
        {
            get => (_header >> 0x9 & 0x1) != 0;
            set
            {
                if (value != Arg3Enabled)
                {
                    if (value)
                    {
                        _header |= (1 << 0x9);
                    }
                    else
                    {
                        var mask = ~(1 << 0x9);
                        _header &= (UInt32)mask;
                    }
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(Header));
                }
            }
        }
        public Boolean Arg4Enabled
        {
            get => (_header >> 0xA & 0x1) != 0;
            set
            {
                if (value != Arg4Enabled)
                {
                    if (value)
                    {
                        _header |= (1 << 0xA);
                    }
                    else
                    {
                        var mask = ~(1 << 0xA);
                        _header &= (UInt32)mask;
                    }
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(Header));
                }
            }
        }
        public Single UnkFloat
        {
            get => _unkFloat;
            set
            {
                if (value != _unkFloat)
                {
                    _unkFloat = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt16 Arg1
        {
            get => _arg1;
            set
            {
                if (value != _arg1)
                {
                    _arg1 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt16 Arg2
        {
            get => _arg2;
            set
            {
                if (value != _arg2)
                {
                    _arg2 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt16 Arg3
        {
            get => _arg3;
            set
            {
                if (value != _arg3)
                {
                    _arg3 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt16 Arg4
        {
            get => _arg4;
            set
            {
                if (value != _arg4)
                {
                    _arg4 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
    }
}
