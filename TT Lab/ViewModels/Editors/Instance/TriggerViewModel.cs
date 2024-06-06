using Caliburn.Micro;
using System;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Editors.Instance
{
    public class TriggerViewModel : InstanceSectionResourceEditorViewModel
    {
        private Enums.TriggerActivatorObjects _objActivatorMask;
        private Vector4ViewModel _position;
        private Vector4ViewModel _rotation;
        private Vector4ViewModel _scale;
        private BindableCollection<LabURI> _instances;
        private UInt32 _header;
        private Single _unkFloat;
        private Enums.Layouts _layId;
        private UInt16 _triggerMessage1;
        private UInt16 _triggerMessage2;
        private UInt16 _triggerMessage3;
        private UInt16 _triggerMessage4;

        public TriggerViewModel(Enums.Layouts layoutId, TriggerData data)
        {
            _objActivatorMask = MiscUtils.ConvertEnum<Enums.TriggerActivatorObjects>(data.ObjectActivatorMask);
            _instances = new BindableCollection<LabURI>();
            foreach (var inst in data.Instances)
            {
                _instances.Add(inst);
            }
            _position = new Vector4ViewModel(data.Position);
            _rotation = new Vector4ViewModel(data.Rotation);
            _scale = new Vector4ViewModel(data.Scale);
            _header = data.Header;
            _unkFloat = data.UnkFloat;
            _triggerMessage1 = data.TriggerMessage1;
            _triggerMessage2 = data.TriggerMessage2;
            _triggerMessage3 = data.TriggerMessage3;
            _triggerMessage4 = data.TriggerMessage4;
            _layId = layoutId;
            DeleteInstanceFromListCommand = new DeleteItemFromListCommand(_instances);
        }

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            asset.LayoutID = (int)LayoutID;
            var data = asset.GetData<TriggerData>();
            Save(data);
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<TriggerData>();
            _objActivatorMask = MiscUtils.ConvertEnum<Enums.TriggerActivatorObjects>(data.ObjectActivatorMask);
            _instances = new BindableCollection<LabURI>();
            foreach (var inst in data.Instances)
            {
                _instances.Add(inst);
            }
            _position = new Vector4ViewModel(data.Position);
            _rotation = new Vector4ViewModel(data.Rotation);
            _scale = new Vector4ViewModel(data.Scale);
            _header = data.Header;
            _unkFloat = data.UnkFloat;
            _triggerMessage1 = data.TriggerMessage1;
            _triggerMessage2 = data.TriggerMessage2;
            _triggerMessage3 = data.TriggerMessage3;
            _triggerMessage4 = data.TriggerMessage4;
            _layId = MiscUtils.ConvertEnum<Enums.Layouts>(asset.LayoutID!.Value);
            DeleteInstanceFromListCommand = new DeleteItemFromListCommand(_instances);
        }

        public void Save(TriggerData data)
        {
            data.ObjectActivatorMask = ObjectActivatorMask;
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
            data.TriggerMessage1 = TriggerMessage1;
            data.TriggerMessage2 = TriggerMessage2;
            data.TriggerMessage3 = TriggerMessage3;
            data.TriggerMessage4 = TriggerMessage4;
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(ObjectActivatorMask));
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
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(ObjectActivatorMask));
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
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(ObjectActivatorMask));
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
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(ObjectActivatorMask));
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
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(ObjectActivatorMask));
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
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(ObjectActivatorMask));
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
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(ObjectActivatorMask));
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
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(ObjectActivatorMask));
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
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(ObjectActivatorMask));
                }
            }
        }

        public Vector4ViewModel Position
        {
            get => _position;
        }

        public Vector4ViewModel Rotation
        {
            get => _rotation;
        }

        public Vector4ViewModel Scale
        {
            get => _scale;
        }

        public BindableCollection<LabURI> Instances
        {
            get => _instances;
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
                    NotifyOfPropertyChange();
                }
            }
        }

        public Boolean TriggerArgument1Enabled
        {
            get => (_header >> 0xB & 0x1) != 0;
            set
            {
                if (value != TriggerArgument1Enabled)
                {
                    if (value)
                    {
                        _header |= 1 << 0xB;
                    }
                    else
                    {
                        var mask = ~(1 << 0xB);
                        _header &= (UInt32)mask;
                    }
                    IsDirty = true;
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(Header));
                }
            }
        }

        public Boolean TriggerArgument2Enabled
        {
            get => (_header >> 0x8 & 0x1) != 0;
            set
            {
                if (value != TriggerArgument2Enabled)
                {
                    if (value)
                    {
                        _header |= 1 << 0x8;
                    }
                    else
                    {
                        var mask = ~(1 << 0x8);
                        _header &= (UInt32)mask;
                    }
                    IsDirty = true;
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(Header));
                }
            }
        }

        public Boolean TriggerArgument3Enabled
        {
            get => (_header >> 0x9 & 0x1) != 0;
            set
            {
                if (value != TriggerArgument3Enabled)
                {
                    if (value)
                    {
                        _header |= 1 << 0x9;
                    }
                    else
                    {
                        var mask = ~(1 << 0x9);
                        _header &= (UInt32)mask;
                    }
                    IsDirty = true;
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(Header));
                }
            }
        }

        public Boolean TriggerArgument4Enabled
        {
            get => (_header >> 0xA & 0x1) != 0;
            set
            {
                if (value != TriggerArgument4Enabled)
                {
                    if (value)
                    {
                        _header |= 1 << 0xA;
                    }
                    else
                    {
                        var mask = ~(1 << 0xA);
                        _header &= (UInt32)mask;
                    }
                    IsDirty = true;
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(Header));
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
                    NotifyOfPropertyChange();
                }
            }
        }

        public UInt16 TriggerMessage1
        {
            get => _triggerMessage1;
            set
            {
                if (value != _triggerMessage1)
                {
                    _triggerMessage1 = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }

        public UInt16 TriggerMessage2
        {
            get => _triggerMessage2;
            set
            {
                if (value != _triggerMessage2)
                {
                    _triggerMessage2 = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }

        public UInt16 TriggerMessage3
        {
            get => _triggerMessage3;
            set
            {
                if (value != _triggerMessage3)
                {
                    _triggerMessage3 = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }

        public UInt16 TriggerMessage4
        {
            get => _triggerMessage4;
            set
            {
                if (value != _triggerMessage4)
                {
                    _triggerMessage4 = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }
    }
}
