using Caliburn.Micro;
using System;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Attributes;
using TT_Lab.Command;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Editors.Instance
{
    public class ObjectInstanceViewModel : InstanceSectionResourceEditorViewModel
    {
        private Enums.Layouts layoutId;
        private Vector4ViewModel position = new();
        private Vector3ViewModel rotation = new();
        private BindableCollection<PrimitiveWrapperViewModel<LabURI>> instances = new();
        private BindableCollection<PrimitiveWrapperViewModel<LabURI>> paths = new();
        private BindableCollection<PrimitiveWrapperViewModel<LabURI>> positions = new();
        private Boolean useOnSpawnScript;
        private LabURI objectId = LabURI.Empty;
        private Int16 refListIndex;
        private LabURI onSpawnScriptId = LabURI.Empty;
        private Enums.InstanceState stateFlags;
        private BindableCollection<PrimitiveWrapperViewModel<UInt32>> flagParams = new();
        private BindableCollection<PrimitiveWrapperViewModel<Single>> floatParams = new();
        private BindableCollection<PrimitiveWrapperViewModel<UInt32>> intParams = new();
        private Int32 _flagIndex;
        private Int32 _floatIndex;
        private Int32 _intIndex;

        public ObjectInstanceViewModel()
        {
            DirtyTracker.AddBindableCollection(instances);
            DirtyTracker.AddBindableCollection(paths);
            DirtyTracker.AddBindableCollection(positions);
            DirtyTracker.AddBindableCollection(flagParams);
            DirtyTracker.AddBindableCollection(floatParams);
            DirtyTracker.AddBindableCollection(intParams);
            DirtyTracker.AddChild(position);
            DirtyTracker.AddChild(rotation);
            
            AddIntParamCommand = new AddItemToListCommand<PrimitiveWrapperViewModel<UInt32>>(IntParams);
            AddFlagParamCommand = new AddItemToListCommand<PrimitiveWrapperViewModel<UInt32>>(FlagParams);
            AddFloatParamCommand = new AddItemToListCommand<PrimitiveWrapperViewModel<Single>>(FloatParams);
            DeleteIntParamCommand = new DeleteItemFromListCommand(IntParams);
            DeleteFlagParamCommand = new DeleteItemFromListCommand(FlagParams);
            DeleteFloatParamCommand = new DeleteItemFromListCommand(FloatParams);
            DeleteLinkedInstanceCommand = new DeleteItemFromListCommand(Instances);
            DeleteLinkedPathCommand = new DeleteItemFromListCommand(Paths);
            DeleteLinkedPositionCommand = new DeleteItemFromListCommand(Positions);
        }

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
                data.Instances.Add(i.Value);
            }
            data.Positions.Clear();
            foreach (var p in Positions)
            {
                data.Positions.Add(p.Value);
            }
            data.Paths.Clear();
            foreach (var p in Paths)
            {
                data.Paths.Add(p.Value);
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
                data.ParamList1.Add(f.Value);
            }
            data.ParamList2.Clear();
            foreach (var s in FloatParams)
            {
                data.ParamList2.Add(s.Value);
            }
            data.ParamList3.Clear();
            foreach (var i in IntParams)
            {
                data.ParamList3.Add(i.Value);
            }
            
            base.Save();
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<ObjectInstanceData>();
            DirtyTracker.RemoveChild(position);
            position = new Vector4ViewModel(data.Position);
            DirtyTracker.AddChild(position);
            var rotX = data.RotationX.GetRotation();
            var rotY = data.RotationY.GetRotation();
            var rotZ = data.RotationZ.GetRotation();
            DirtyTracker.RemoveChild(rotation);
            rotation = new Vector3ViewModel(rotX, rotY, rotZ);
            DirtyTracker.AddChild(rotation);
            foreach (var i in data.Instances)
            {
                instances.Add(new PrimitiveWrapperViewModel<LabURI>(i));
            }
            foreach (var p in data.Paths)
            {
                paths.Add(new PrimitiveWrapperViewModel<LabURI>(p));
            }
            foreach (var p in data.Positions)
            {
                positions.Add(new PrimitiveWrapperViewModel<LabURI>(p));
            }
            objectId = data.ObjectId;
            refListIndex = data.RefListIndex;
            onSpawnScriptId = data.OnSpawnScriptId;
            useOnSpawnScript = onSpawnScriptId != LabURI.Empty;
            stateFlags = MiscUtils.ConvertEnum<Enums.InstanceState>(data.StateFlags);
            foreach (var f in data.ParamList1)
            {
                flagParams.Add(new PrimitiveWrapperViewModel<UInt32>(f));
            }
            foreach (var s in data.ParamList2)
            {
                floatParams.Add(new PrimitiveWrapperViewModel<Single>(s));
            }
            foreach (var i in data.ParamList3)
            {
                intParams.Add(new PrimitiveWrapperViewModel<UInt32>(i));
            }
            layoutId = MiscUtils.ConvertEnum<Enums.Layouts>(asset.LayoutID!.Value);
        }


        public AddItemToListCommand<PrimitiveWrapperViewModel<UInt32>> AddIntParamCommand { get; private set; }
        public DeleteItemFromListCommand DeleteIntParamCommand { get; private set; }
        public AddItemToListCommand<PrimitiveWrapperViewModel<UInt32>> AddFlagParamCommand { get; private set; }
        public DeleteItemFromListCommand DeleteFlagParamCommand { get; private set; }
        public AddItemToListCommand<PrimitiveWrapperViewModel<Single>> AddFloatParamCommand { get; private set; }
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
        [MarkDirty]
        public Enums.Layouts LayoutID
        {
            get => layoutId;
            set
            {
                if (value != layoutId)
                {
                    layoutId = value;
                    
                    NotifyOfPropertyChange();
                }
            }
        }
        [MarkDirty]
        public ResourceTreeElementViewModel InstanceObject
        {
            get => AssetManager.Get().GetAsset(objectId).GetResourceTreeElement();
            set
            {
                if (value.Asset.URI != objectId)
                {
                    objectId = value.Asset.URI;
                    
                    NotifyOfPropertyChange();
                }
            }
        }
        [MarkDirty]
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
                    
                    NotifyOfPropertyChange();
                }
            }
        }
        [MarkDirty]
        public Boolean UseOnSpawnScript
        {
            get => useOnSpawnScript;
            set
            {
                if (value != useOnSpawnScript)
                {
                    useOnSpawnScript = value;
                    
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
                    
                }
            }
        }
        public Vector3ViewModel Rotation
        {
            get => rotation;
        }
        public BindableCollection<PrimitiveWrapperViewModel<LabURI>> Instances
        {
            get => instances;
        }
        public BindableCollection<PrimitiveWrapperViewModel<LabURI>> Positions
        {
            get => positions;
        }
        public BindableCollection<PrimitiveWrapperViewModel<LabURI>> Paths
        {
            get => paths;
        }
        [MarkDirty]
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
                    
                }
            }
        }
        [MarkDirty]
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
                    NotifyOfPropertyChange(nameof(ShadowActive));
                    NotifyOfPropertyChange(nameof(PlayableCharacterCanMoveAlong));
                    NotifyOfPropertyChange(nameof(Unknown3));
                    NotifyOfPropertyChange(nameof(SyncCrossChunkState));
                    NotifyOfPropertyChange(nameof(Unknown5));
                    NotifyOfPropertyChange(nameof(SolidToBodySlam));
                    NotifyOfPropertyChange(nameof(SolidToSlide));
                    NotifyOfPropertyChange(nameof(SolidToSpin));
                    NotifyOfPropertyChange(nameof(SolidToTwinSlam));
                    NotifyOfPropertyChange(nameof(SolidToThrownCortex));
                    NotifyOfPropertyChange(nameof(Targettable));
                    NotifyOfPropertyChange(nameof(BulletsWillBounceBack));
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
                    
                }
            }
        }
        [MarkDirty]
        public Boolean Deactivated
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Deactivated);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Deactivated, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean CollisionActive
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CollisionActive);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CollisionActive, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Visible
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Visible);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Visible, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean ReceiveOnTriggerSignals
        {
            get => stateFlags.HasFlag(Enums.InstanceState.ReceiveOnTriggerSignals);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.ReceiveOnTriggerSignals, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean CanDamageCharacter
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CanDamageCharacter);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CanDamageCharacter, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean CanAlwaysDamageCharacter
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CanAlwaysDamageCharacter);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CanAlwaysDamageCharacter, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean ShadowActive
        {
            get => stateFlags.HasFlag(Enums.InstanceState.ShadowActive);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.ShadowActive, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean PlayableCharacterCanMoveAlong
        {
            get => stateFlags.HasFlag(Enums.InstanceState.PlayableCharacterCanMoveAlong);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.PlayableCharacterCanMoveAlong, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown3
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown1);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown1, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean SyncCrossChunkState
        {
            get => stateFlags.HasFlag(Enums.InstanceState.SyncCrossChunkState);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.SyncCrossChunkState, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown5
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown2);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown2, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean SolidToBodySlam
        {
            get => stateFlags.HasFlag(Enums.InstanceState.SolidToBodySlam);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.SolidToBodySlam, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean SolidToSlide
        {
            get => stateFlags.HasFlag(Enums.InstanceState.SolidToSlide);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.SolidToSlide, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean SolidToSpin
        {
            get => stateFlags.HasFlag(Enums.InstanceState.SolidToSpin);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.SolidToSpin, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean SolidToTwinSlam
        {
            get => stateFlags.HasFlag(Enums.InstanceState.SolidToTwinSlam);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.SolidToTwinSlam, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean SolidToThrownCortex
        {
            get => stateFlags.HasFlag(Enums.InstanceState.SolidToThrownCortex);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.SolidToThrownCortex, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Targettable
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Targettable);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Targettable, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean BulletsWillBounceBack
        {
            get => stateFlags.HasFlag(Enums.InstanceState.BulletsWillBounceBack);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.BulletsWillBounceBack, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown13
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown3);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown3, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown14
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown4);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown4, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown15
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown5);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown5, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown16
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown6);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown6, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown17
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown7);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown7, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown18
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown8);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown8, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown19
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown9);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown9, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown20
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown10);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown10, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown21
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown11);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown11, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown22
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown12);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown12, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown23
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown13);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown13, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown24
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown14);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown14, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown25
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown15);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown15, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        [MarkDirty]
        public Boolean Unknown26
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown16);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown16, value);
                
                NotifyOfPropertyChange(nameof(StateFlags));
            }
        }
        public BindableCollection<PrimitiveWrapperViewModel<UInt32>> FlagParams
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

                return FlagParams[_flagIndex].Value;
            }
            set
            {
                if (_flagIndex == -1) return;
                FlagParams[_flagIndex].Value = value;
                
                NotifyOfPropertyChange(nameof(FlagParams));
                NotifyOfPropertyChange();
            }
        }
        public BindableCollection<PrimitiveWrapperViewModel<Single>> FloatParams
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
                return FloatParams[_floatIndex].Value;
            }
            set
            {
                if (_floatIndex == -1) return;
                FloatParams[_floatIndex].Value = value;
                
                NotifyOfPropertyChange(nameof(FloatParams));
                NotifyOfPropertyChange();
            }
        }
        public BindableCollection<PrimitiveWrapperViewModel<UInt32>> IntParams
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
                return IntParams[_intIndex].Value;
            }
            set
            {
                if (_intIndex == -1) return;
                IntParams[_intIndex].Value = value;
                
                NotifyOfPropertyChange(nameof(IntParams));
                NotifyOfPropertyChange();
            }
        }
    }
}
