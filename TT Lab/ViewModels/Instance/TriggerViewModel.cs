using System;
using System.Collections.ObjectModel;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
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
        private ObservableCollection<LabURI> _instances;
        private UInt32 _header;
        private Single _unkFloat;
        private Enums.Layouts _layId;
        private LabURI _triggerScript1;
        private LabURI _triggerScript2;
        private LabURI _triggerScript3;
        private LabURI _triggerScript4;

        public TriggerViewModel(LabURI asset, AssetViewModel? parent) : base(asset, parent)
        {
            var data = _asset.GetData<TriggerData>();
            _objActivatorMask = MiscUtils.ConvertEnum<Enums.TriggerActivatorObjects>(data.ObjectActivatorMask);
            _instances = new ObservableCollection<LabURI>();
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
            _triggerScript1 = data.TriggerScript1;
            _triggerScript2 = data.TriggerScript2;
            _triggerScript3 = data.TriggerScript3;
            _triggerScript4 = data.TriggerScript4;
            _layId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);
            DeleteInstanceFromListCommand = new DeleteItemFromListCommand(_instances);
        }

        public TriggerViewModel(LabURI asset, AssetViewModel? parent, TriggerData data) : base(asset, parent)
        {
            _objActivatorMask = MiscUtils.ConvertEnum<Enums.TriggerActivatorObjects>(data.ObjectActivatorMask);
            _instances = new ObservableCollection<LabURI>();
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
            _triggerScript1 = data.TriggerScript1;
            _triggerScript2 = data.TriggerScript2;
            _triggerScript3 = data.TriggerScript3;
            _triggerScript4 = data.TriggerScript4;
            _layId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);
            DeleteInstanceFromListCommand = new DeleteItemFromListCommand(_instances);
        }

        public override void Save(object? o)
        {
            _asset.LayoutID = (int)LayoutID;
            var data = _asset.GetData<TriggerData>();
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
            data.TriggerScript1 = TriggerScript1;
            data.TriggerScript2 = TriggerScript2;
            data.TriggerScript3 = TriggerScript3;
            data.TriggerScript4 = TriggerScript4;
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
        public Boolean ActivateByPickups
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Pickups);
            set
            {
                if (value != ActivateByPickups)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Pickups, value);
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
        public Boolean ActivateByCreatures
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Creatures);
            set
            {
                if (value != ActivateByCreatures)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Creatures, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByGenericObjects
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.GenericObjects);
            set
            {
                if (value != ActivateByGenericObjects)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.GenericObjects, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByGrabbables
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Grabbables);
            set
            {
                if (value != ActivateByGrabbables)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Grabbables, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByPayGates
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.PayGates);
            set
            {
                if (value != ActivateByPayGates)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.PayGates, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByGraples
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Graples);
            set
            {
                if (value != ActivateByGraples)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Graples, value);
                    IsDirty = true;
                    NotifyChange();
                    NotifyChange(nameof(ObjectActivatorMask));
                }
            }
        }
        public Boolean ActivateByProjectiles
        {
            get => _objActivatorMask.HasFlag(Enums.TriggerActivatorObjects.Projectiles);
            set
            {
                if (value != ActivateByProjectiles)
                {
                    _objActivatorMask = _objActivatorMask.ChangeFlag(Enums.TriggerActivatorObjects.Projectiles, value);
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
        public ObservableCollection<LabURI> Instances
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
        public Boolean TriggerScript1Enabled
        {
            get => (_header >> 0xB & 0x1) != 0;
            set
            {
                if (value != TriggerScript1Enabled)
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
        public Boolean TriggerScript2Enabled
        {
            get => (_header >> 0x8 & 0x1) != 0;
            set
            {
                if (value != TriggerScript2Enabled)
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
        public Boolean TriggerScript3Enabled
        {
            get => (_header >> 0x9 & 0x1) != 0;
            set
            {
                if (value != TriggerScript3Enabled)
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
        public Boolean TriggerScript4Enabled
        {
            get => (_header >> 0xA & 0x1) != 0;
            set
            {
                if (value != TriggerScript4Enabled)
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
        public LabURI TriggerScript1
        {
            get => _triggerScript1;
            set
            {
                if (value != _triggerScript1)
                {
                    _triggerScript1 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public LabURI TriggerScript2
        {
            get => _triggerScript2;
            set
            {
                if (value != _triggerScript2)
                {
                    _triggerScript2 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public LabURI TriggerScript3
        {
            get => _triggerScript3;
            set
            {
                if (value != _triggerScript3)
                {
                    _triggerScript3 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public LabURI TriggerScript4
        {
            get => _triggerScript4;
            set
            {
                if (value != _triggerScript4)
                {
                    _triggerScript4 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
    }
}
