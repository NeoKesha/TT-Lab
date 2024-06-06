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
    public class ObjectInstanceViewModel : InstanceSectionResourceEditorViewModel
    {
        private Enums.Layouts layoutId;
        private Vector4ViewModel position;
        private Vector3ViewModel rotation;
        private BindableCollection<LabURI> instances;
        private BindableCollection<LabURI> paths;
        private BindableCollection<LabURI> positions;
        private Boolean useOnSpawnScript;
        private LabURI objectId;
        private Int16 refListIndex;
        private LabURI onSpawnScriptId;
        private Enums.InstanceState stateFlags;
        private BindableCollection<UInt32> flagParams;
        private BindableCollection<Single> floatParams;
        private BindableCollection<UInt32> intParams;
        private Int32 _flagIndex;
        private Int32 _floatIndex;
        private Int32 _intIndex;

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<ObjectInstanceData>();
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
            data.OnSpawnScriptId = LabURI.Empty;
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
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<ObjectInstanceData>();
            position = new Vector4ViewModel(data.Position);
            var rotX = data.RotationX.GetRotation();
            var rotY = data.RotationY.GetRotation();
            var rotZ = data.RotationZ.GetRotation();
            rotation = new Vector3ViewModel(rotX, rotY, rotZ);
            instances = new BindableCollection<LabURI>();
            foreach (var i in data.Instances)
            {
                instances.Add(i);
            }
            paths = new BindableCollection<LabURI>();
            foreach (var p in data.Paths)
            {
                paths.Add(p);
            }
            positions = new BindableCollection<LabURI>();
            foreach (var p in data.Positions)
            {
                positions.Add(p);
            }
            objectId = data.ObjectId;
            refListIndex = data.RefListIndex;
            onSpawnScriptId = data.OnSpawnScriptId;
            useOnSpawnScript = onSpawnScriptId != LabURI.Empty;
            stateFlags = MiscUtils.ConvertEnum<Enums.InstanceState>(data.StateFlags);
            flagParams = new BindableCollection<UInt32>();
            foreach (var f in data.ParamList1)
            {
                flagParams.Add(f);
            }
            floatParams = new BindableCollection<Single>();
            foreach (var s in data.ParamList2)
            {
                floatParams.Add(s);
            }
            intParams = new BindableCollection<UInt32>();
            foreach (var i in data.ParamList3)
            {
                intParams.Add(i);
            }
            layoutId = MiscUtils.ConvertEnum<Enums.Layouts>(asset.LayoutID!.Value);

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
                var obj = AssetManager.Get().GetAsset(objectId);
                var asset = AssetManager.Get().GetAsset(EditableResource);
                return $"Instance {asset.ID} - {obj.Alias}";
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
                    NotifyOfPropertyChange();
                }
            }
        }
        public ResourceTreeElementViewModel InstanceObject
        {
            get => AssetManager.Get().GetAsset(objectId).GetResourceTreeElement();
            set
            {
                if (value.Asset.URI != objectId)
                {
                    objectId = value.Asset.URI;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }
        public ResourceTreeElementViewModel? OnSpawnScript
        {
            get
            {
                if (onSpawnScriptId != LabURI.Empty)
                {
                    return AssetManager.Get().GetAsset(onSpawnScriptId).GetResourceTreeElement();
                }
                return null;
            }
            set
            {
                if (value?.Asset.URI != onSpawnScriptId)
                {
                    onSpawnScriptId = value == null ? LabURI.Empty : value.Asset.URI;
                    IsDirty = true;
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
                    IsDirty = true;
                }
            }
        }
        public Vector3ViewModel Rotation
        {
            get => rotation;
        }
        public BindableCollection<LabURI> Instances
        {
            get => instances;
        }
        public BindableCollection<LabURI> Positions
        {
            get => positions;
        }
        public BindableCollection<LabURI> Paths
        {
            get => paths;
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(nameof(Deactivated));
                    NotifyOfPropertyChange(nameof(CollisionActive));
                    NotifyOfPropertyChange(nameof(Visible));
                    NotifyOfPropertyChange(nameof(ReceiveOnTriggerSignals));
                    NotifyOfPropertyChange(nameof(CanDamageCharacter));
                    NotifyOfPropertyChange(nameof(CanAlwaysDamageCharacter));
                    NotifyOfPropertyChange(nameof(Unknown1));
                    NotifyOfPropertyChange(nameof(Unknown2));
                    NotifyOfPropertyChange(nameof(Unknown3));
                    NotifyOfPropertyChange(nameof(Unknown4));
                    NotifyOfPropertyChange(nameof(Unknown5));
                    NotifyOfPropertyChange(nameof(Unknown6));
                    NotifyOfPropertyChange(nameof(Unknown7));
                    NotifyOfPropertyChange(nameof(Unknown8));
                    NotifyOfPropertyChange(nameof(Unknown9));
                    NotifyOfPropertyChange(nameof(Unknown10));
                    NotifyOfPropertyChange(nameof(Unknown11));
                    NotifyOfPropertyChange(nameof(Unknown12));
                    NotifyOfPropertyChange(nameof(Unknown13));
                    NotifyOfPropertyChange(nameof(Unknown14));
                    NotifyOfPropertyChange(nameof(Unknown15));
                    NotifyOfPropertyChange(nameof(Unknown16));
                    NotifyOfPropertyChange(nameof(Unknown17));
                    NotifyOfPropertyChange(nameof(Unknown18));
                    NotifyOfPropertyChange(nameof(Unknown19));
                    NotifyOfPropertyChange(nameof(Unknown20));
                    NotifyOfPropertyChange(nameof(Unknown21));
                    NotifyOfPropertyChange(nameof(Unknown22));
                    NotifyOfPropertyChange(nameof(Unknown23));
                    NotifyOfPropertyChange(nameof(Unknown24));
                    NotifyOfPropertyChange(nameof(Unknown25));
                    NotifyOfPropertyChange(nameof(Unknown26));
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
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean CollisionActive
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CollisionActive);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CollisionActive, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Visible
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Visible);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Visible, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean ReceiveOnTriggerSignals
        {
            get => stateFlags.HasFlag(Enums.InstanceState.ReceiveOnTriggerSignals);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.ReceiveOnTriggerSignals, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean CanDamageCharacter
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CanDamageCharacter);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CanDamageCharacter, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean CanAlwaysDamageCharacter
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CanAlwaysDamageCharacter);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CanAlwaysDamageCharacter, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown1
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown1);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown1, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown2
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown2);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown2, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown3
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown3);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown3, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown4
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown4);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown4, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown5
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown5);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown5, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown6
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown6);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown6, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown7
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown7);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown7, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown8
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown8);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown8, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown9
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown9);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown9, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown10
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown10);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown10, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown11
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown11);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown11, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown12
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown12);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown12, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown13
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown13);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown13, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown14
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown14);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown14, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown15
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown15);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown15, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown16
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown16);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown16, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown17
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown17);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown17, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown18
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown18);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown18, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown19
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown19);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown19, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown20
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown20);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown20, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown21
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown21);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown21, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown22
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown22);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown22, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown23
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown23);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown23, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown24
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown24);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown24, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown25
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown25);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown25, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown26
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown26);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown26, value);
                IsDirty = true;
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public BindableCollection<UInt32> FlagParams
        {
            get => flagParams;
        }
        public Int32 FlagIndex
        {
            get => _flagIndex;
            set
            {
                _flagIndex = value;
                NotifyOfPropertyChange(nameof(SelectedFlag));
            }
        }
        public UInt32 SelectedFlag
        {
            get
            {
                if (FlagParams.Count == 0) return 0;

                return FlagParams[_flagIndex];
            }
            set
            {
                if (_flagIndex == -1) return;
                FlagParams[_flagIndex] = value;
                IsDirty = true;
                NotifyOfPropertyChange(nameof(FlagParams));
                NotifyOfPropertyChange();
            }
        }
        public BindableCollection<Single> FloatParams
        {
            get => floatParams;
        }
        public Int32 FloatIndex
        {
            get => _floatIndex;
            set
            {
                _floatIndex = value;
                NotifyOfPropertyChange(nameof(SelectedFloat));
            }
        }
        public Single SelectedFloat
        {
            get
            {
                if (FloatParams.Count == 0) return 0;
                return FloatParams[_floatIndex];
            }
            set
            {
                if (_floatIndex == -1) return;
                FloatParams[_floatIndex] = value;
                IsDirty = true;
                NotifyOfPropertyChange(nameof(FloatParams));
                NotifyOfPropertyChange();
            }
        }
        public BindableCollection<UInt32> IntParams
        {
            get => intParams;
        }
        public Int32 IntIndex
        {
            get => _intIndex;
            set
            {
                _intIndex = value;
                NotifyOfPropertyChange(nameof(SelectedInt));
            }
        }
        public UInt32 SelectedInt
        {
            get
            {
                if (IntParams.Count == 0)
                    return 0;
                return IntParams[_intIndex];
            }
            set
            {
                if (_intIndex == -1) return;
                IntParams[_intIndex] = value;
                IsDirty = true;
                NotifyOfPropertyChange(nameof(IntParams));
                NotifyOfPropertyChange();
            }
        }
    }
}
