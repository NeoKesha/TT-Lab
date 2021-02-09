using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets.Code;
using TT_Lab.Command;
using TT_Lab.Project;
using TT_Lab.Util;
using TT_Lab.ViewModels.Code;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Instance
{
    public class ObjectInstanceViewModel : AssetViewModel
    {
        private Enums.Layouts layoutId;
        private Vector4ViewModel position;
        private Vector3ViewModel rotation;
        private ObservableCollection<UInt16> instances;
        private ObservableCollection<UInt16> paths;
        private ObservableCollection<UInt16> positions;
        private Boolean useOnSpawnScript;
        private Guid objectId;
        private Int16 refListIndex;
        private Guid onSpawnScriptId;
        private Enums.InstanceState stateFlags;
        private ObservableCollection<UInt32> flagParams;
        private ObservableCollection<Single> floatParams;
        private ObservableCollection<UInt32> intParams;
        private Int32 _flagIndex;
        private Int32 _floatIndex;
        private Int32 _intIndex;

        public ObjectInstanceViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var data = (ObjectInstanceData)_asset.GetData();
            position = new Vector4ViewModel(data.Position);
            position.PropertyChanged += Vector_PropertyChanged;
            var rotX = data.RotationX.GetRotation();
            var rotY = data.RotationY.GetRotation();
            var rotZ = data.RotationZ.GetRotation();
            rotation = new Vector3ViewModel(rotX, rotY, rotZ);
            rotation.PropertyChanged += Vector_PropertyChanged;
            instances = new ObservableCollection<UInt16>();
            foreach (var i in data.Instances)
            {
                instances.Add(i);
            }
            Instances.CollectionChanged += Instances_CollectionChanged;
            paths = new ObservableCollection<UInt16>();
            foreach (var p in data.Paths)
            {
                paths.Add(p);
            }
            Paths.CollectionChanged += Paths_CollectionChanged;
            positions = new ObservableCollection<UInt16>();
            foreach (var p in data.Positions)
            {
                positions.Add(p);
            }
            Positions.CollectionChanged += Positions_CollectionChanged;
            objectId = data.ObjectId;
            refListIndex = data.RefListIndex;
            onSpawnScriptId = data.OnSpawnScriptId;
            useOnSpawnScript = onSpawnScriptId != Guid.Empty;
            stateFlags = MiscUtils.ConvertEnum<Enums.InstanceState>(data.StateFlags);
            flagParams = new ObservableCollection<UInt32>();
            foreach (var f in data.ParamList1)
            {
                flagParams.Add(f);
            }
            FlagParams.CollectionChanged += FlagParams_CollectionChanged;
            floatParams = new ObservableCollection<Single>();
            foreach (var s in data.ParamList2)
            {
                floatParams.Add(s);
            }
            FloatParams.CollectionChanged += FloatParams_CollectionChanged;
            intParams = new ObservableCollection<UInt32>();
            foreach (var i in data.ParamList3)
            {
                intParams.Add(i);
            }
            IntParams.CollectionChanged += IntParams_CollectionChanged;
            layoutId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);

            AddIntParamCommand = new AddItemToListCommand<UInt32>(IntParams);
            AddFlagParamCommand = new AddItemToListCommand<UInt32>(FlagParams);
            AddFloatParamCommand = new AddItemToListCommand<Single>(FloatParams);
            DeleteIntParamCommand = new DeleteItemFromListCommand(IntParams);
            DeleteFlagParamCommand = new DeleteItemFromListCommand(FlagParams);
            DeleteFloatParamCommand = new DeleteItemFromListCommand(FloatParams);
            DeleteLinkedInstanceCommand = new DeleteItemFromListCommand(Instances);
            DeleteLinkedPathCommand = new DeleteItemFromListCommand(Paths);
            DeleteLinkedPositionCommand = new DeleteItemFromListCommand(Positions);
        }

        private void IntParams_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(IntParams));
        }

        private void FloatParams_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(FloatParams));
        }

        private void FlagParams_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(FlagParams));
        }

        private void Positions_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(Positions));
        }

        private void Paths_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(Paths));
        }

        private void Instances_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(Instances));
        }

        private void Vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(Position));
            NotifyChange(nameof(Rotation));
        }

        public override void Save(Object? o)
        {
            var data = (ObjectInstanceData)_asset.GetData();
            Position.Save(data.Position);
            data.RotationX.SetRotation(Rotation.X);
            data.RotationY.SetRotation(Rotation.Y);
            data.RotationZ.SetRotation(Rotation.Z);
            data.Instances.Clear();
            foreach (var i in Instances)
            {
                data.Instances.Add(i);
            }
            data.Positions.Clear();
            foreach (var p in Positions)
            {
                data.Positions.Add(p);
            }
            data.Paths.Clear();
            foreach (var p in Paths)
            {
                data.Paths.Add(p);
            }
            data.ObjectId = objectId;
            data.RefListIndex = RefListIndex;
            data.OnSpawnScriptId = Guid.Empty;
            if (UseOnSpawnScript)
            {
                data.OnSpawnScriptId = onSpawnScriptId;
            }
            data.StateFlags = (UInt32)stateFlags;
            data.ParamList1.Clear();
            foreach (var f in FlagParams)
            {
                data.ParamList1.Add(f);
            }
            data.ParamList2.Clear();
            foreach (var s in FloatParams)
            {
                data.ParamList2.Add(s);
            }
            data.ParamList3.Clear();
            foreach (var i in IntParams)
            {
                data.ParamList3.Add(i);
            }
            base.Save(o);
        }


        public AddItemToListCommand<UInt32> AddIntParamCommand { get; private set; }
        public DeleteItemFromListCommand DeleteIntParamCommand { get; private set; }
        public AddItemToListCommand<UInt32> AddFlagParamCommand { get; private set; }
        public DeleteItemFromListCommand DeleteFlagParamCommand { get; private set; }
        public AddItemToListCommand<Single> AddFloatParamCommand { get; private set; }
        public DeleteItemFromListCommand DeleteFloatParamCommand { get; private set; }
        public DeleteItemFromListCommand DeleteLinkedInstanceCommand { get; private set; }
        public DeleteItemFromListCommand DeleteLinkedPositionCommand { get; private set; }
        public DeleteItemFromListCommand DeleteLinkedPathCommand { get; private set; }

        public string Name
        {
            get
            {
                var obj = (GameObject)ProjectManagerSingleton.PM.OpenedProject.GetAsset(objectId);
                return $"Instance {_asset.ID} - {obj.Alias}";
            }
        }
        public Enums.Layouts LayoutID
        {
            get => layoutId;
            set
            {
                if (value != layoutId)
                {
                    layoutId = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public AssetViewModel InstanceObject
        {
            get => ProjectManagerSingleton.PM.OpenedProject.GetAsset(objectId).GetViewModel();
            set
            {
                if (value.Asset.UUID != objectId)
                {
                    objectId = value.Asset.UUID;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public HeaderScriptViewModel? OnSpawnScript
        {
            get
            {
                if (onSpawnScriptId != Guid.Empty)
                {
                    return (HeaderScriptViewModel)ProjectManagerSingleton.PM.OpenedProject.GetAsset(onSpawnScriptId).GetViewModel();
                }
                return null;
            }
            set
            {
                if (value?.Asset.UUID != onSpawnScriptId)
                {
                    onSpawnScriptId = value == null ? Guid.Empty : value.Asset.UUID;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Boolean UseOnSpawnScript
        {
            get => useOnSpawnScript;
            set
            {
                if (value != useOnSpawnScript)
                {
                    useOnSpawnScript = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel Position
        {
            get
            {
                return position;
            }
            set
            {
                if (value != position)
                {
                    position = value;
                    NotifyChange();
                    IsDirty = true;
                }
            }
        }
        public Vector3ViewModel Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                if (value != rotation)
                {
                    rotation = value;
                    NotifyChange();
                    IsDirty = true;
                }
            }
        }
        public ObservableCollection<UInt16> Instances
        {
            get
            {
                return instances;
            }
        }
        public ObservableCollection<UInt16> Positions
        {
            get
            {
                return positions;
            }
        }
        public ObservableCollection<UInt16> Paths
        {
            get
            {
                return paths;
            }
        }
        public Int16 RefListIndex
        {
            get
            {
                return refListIndex;
            }
            set
            {
                if (value != refListIndex)
                {
                    refListIndex = value;
                    NotifyChange();
                    IsDirty = true;
                }
            }
        }
        public UInt32 StateFlags
        {
            get
            {
                return (UInt32)stateFlags;
            }
            set
            {
                var flags = MiscUtils.ConvertEnum<Enums.InstanceState>(value);
                if (flags != stateFlags)
                {
                    stateFlags = flags;
                    NotifyChange();
                    NotifyChange(nameof(Deactivated));
                    NotifyChange(nameof(CollisionActive));
                    NotifyChange(nameof(Visible));
                    NotifyChange(nameof(ReceiveOnTriggerSignals));
                    NotifyChange(nameof(CanDamageCharacter));
                    NotifyChange(nameof(CanAlwaysDamageCharacter));
                    NotifyChange(nameof(Unknown1));
                    NotifyChange(nameof(Unknown2));
                    NotifyChange(nameof(Unknown3));
                    NotifyChange(nameof(Unknown4));
                    NotifyChange(nameof(Unknown5));
                    NotifyChange(nameof(Unknown6));
                    NotifyChange(nameof(Unknown7));
                    NotifyChange(nameof(Unknown8));
                    NotifyChange(nameof(Unknown9));
                    NotifyChange(nameof(Unknown10));
                    NotifyChange(nameof(Unknown11));
                    NotifyChange(nameof(Unknown12));
                    NotifyChange(nameof(Unknown13));
                    NotifyChange(nameof(Unknown14));
                    NotifyChange(nameof(Unknown15));
                    NotifyChange(nameof(Unknown16));
                    NotifyChange(nameof(Unknown17));
                    NotifyChange(nameof(Unknown18));
                    NotifyChange(nameof(Unknown19));
                    NotifyChange(nameof(Unknown20));
                    NotifyChange(nameof(Unknown21));
                    NotifyChange(nameof(Unknown22));
                    NotifyChange(nameof(Unknown23));
                    NotifyChange(nameof(Unknown24));
                    NotifyChange(nameof(Unknown25));
                    NotifyChange(nameof(Unknown26));
                    IsDirty = true;
                }
            }
        }
        public Boolean Deactivated
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Deactivated);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Deactivated, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean CollisionActive
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CollisionActive);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CollisionActive, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Visible
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Visible);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Visible, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean ReceiveOnTriggerSignals
        {
            get => stateFlags.HasFlag(Enums.InstanceState.ReceiveOnTriggerSignals);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.ReceiveOnTriggerSignals, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean CanDamageCharacter
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CanDamageCharacter);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CanDamageCharacter, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean CanAlwaysDamageCharacter
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CanAlwaysDamageCharacter);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CanAlwaysDamageCharacter, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown1
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown1);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown1, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown2
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown2);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown2, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown3
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown3);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown3, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown4
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown4);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown4, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown5
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown5);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown5, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown6
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown6);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown6, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown7
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown7);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown7, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown8
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown8);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown8, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown9
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown9);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown9, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown10
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown10);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown10, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown11
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown11);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown11, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown12
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown12);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown12, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown13
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown13);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown13, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown14
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown14);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown14, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown15
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown15);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown15, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown16
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown16);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown16, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown17
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown17);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown17, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown18
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown18);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown18, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown19
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown19);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown19, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown20
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown20);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown20, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown21
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown21);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown21, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown22
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown22);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown22, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown23
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown23);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown23, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown24
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown24);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown24, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown25
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown25);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown25, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown26
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown26);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown26, value);
                IsDirty = true;
                NotifyChange(nameof(StateFlags));
            }
        }
        public ObservableCollection<UInt32> FlagParams
        {
            get
            {
                return flagParams;
            }
        }
        public Int32 FlagIndex
        {
            get => _flagIndex;
            set
            {
                _flagIndex = value;
                NotifyChange(nameof(SelectedFlag));
            }
        }
        public UInt32 SelectedFlag
        {
            get => FlagParams[_flagIndex];
            set
            {
                if (_flagIndex == -1) return;
                FlagParams[_flagIndex] = value;
                IsDirty = true;
                NotifyChange(nameof(FlagParams));
                NotifyChange();
            }
        }
        public ObservableCollection<Single> FloatParams
        {
            get
            {
                return floatParams;
            }
        }
        public Int32 FloatIndex
        {
            get => _floatIndex;
            set
            {
                _floatIndex = value;
                NotifyChange(nameof(SelectedFloat));
            }
        }
        public Single SelectedFloat
        {
            get => FloatParams[_floatIndex];
            set
            {
                if (_floatIndex == -1) return;
                FloatParams[_floatIndex] = value;
                IsDirty = true;
                NotifyChange(nameof(FloatParams));
                NotifyChange();
            }
        }
        public ObservableCollection<UInt32> IntParams
        {
            get
            {
                return intParams;
            }
        }
        public Int32 IntIndex
        {
            get => _intIndex;
            set
            {
                _intIndex = value;
                NotifyChange(nameof(SelectedInt));
            }
        }
        public UInt32 SelectedInt
        {
            get => IntParams[_intIndex];
            set
            {
                if (_intIndex == -1) return;
                IntParams[_intIndex] = value;
                IsDirty = true;
                NotifyChange(nameof(IntParams));
                NotifyChange();
            }
        }
    }
}
