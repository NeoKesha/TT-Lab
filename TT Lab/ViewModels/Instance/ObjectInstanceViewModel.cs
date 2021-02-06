using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Code;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets.Code;
using TT_Lab.Project;
using TT_Lab.Util;
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
        private Guid objectId;
        private Int16 refListIndex;
        private Guid onSpawnScriptId;
        private Enums.InstanceState stateFlags;
        private ObservableCollection<UInt32> flagParams;
        private ObservableCollection<Single> floatParams;
        private ObservableCollection<UInt32> intParams;

        public ObjectInstanceViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var data = (ObjectInstanceData)_asset.GetData();
            position = new Vector4ViewModel(data.Position);
            var rotX = data.RotationX.GetRotation();
            var rotY = data.RotationY.GetRotation();
            var rotZ = data.RotationZ.GetRotation();
            rotation = new Vector3ViewModel(rotX, rotY, rotZ);
            instances = new ObservableCollection<UInt16>();
            foreach (var i in data.Instances)
            {
                instances.Add(i);
            }
            paths = new ObservableCollection<UInt16>();
            foreach (var p in data.Paths)
            {
                paths.Add(p);
            }
            positions = new ObservableCollection<UInt16>();
            foreach (var p in data.Positions)
            {
                positions.Add(p);
            }
            objectId = data.ObjectId;
            refListIndex = data.RefListIndex;
            onSpawnScriptId = data.OnSpawnScriptId;
            stateFlags = MiscUtils.ConvertEnum<Enums.InstanceState>(data.StateFlags);
            flagParams = new ObservableCollection<UInt32>();
            foreach (var f in data.ParamList1)
            {
                flagParams.Add(f);
            }
            floatParams = new ObservableCollection<Single>();
            foreach (var s in data.ParamList2)
            {
                floatParams.Add(s);
            }
            intParams = new ObservableCollection<UInt32>();
            foreach (var i in data.ParamList3)
            {
                intParams.Add(i);
            }
            layoutId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);
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
            data.ObjectId = ObjectId;
            data.RefListIndex = RefListIndex;
            data.OnSpawnScriptId = OnSpawnScriptId;
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

        public string Name
        {
            get
            {
                var obj = (GameObject)ProjectManagerSingleton.PM.OpenedProject.GetAsset(ObjectId);
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
        public Guid ObjectId
        {
            get
            {
                return objectId;
            }
            set
            {
                if (value != objectId)
                {
                    objectId = value;
                    NotifyChange();
                    IsDirty = true;
                }
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
        public Guid OnSpawnScriptId
        {
            get
            {
                return onSpawnScriptId;
            }
            set
            {
                if (value != onSpawnScriptId)
                {
                    onSpawnScriptId = value;
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
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Deactivated, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean CollisionActive
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CollisionActive);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CollisionActive, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Visible
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Visible);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Visible, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean ReceiveOnTriggerSignals
        {
            get => stateFlags.HasFlag(Enums.InstanceState.ReceiveOnTriggerSignals);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.ReceiveOnTriggerSignals, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean CanDamageCharacter
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CanDamageCharacter);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CanDamageCharacter, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean CanAlwaysDamageCharacter
        {
            get => stateFlags.HasFlag(Enums.InstanceState.CanAlwaysDamageCharacter);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.CanAlwaysDamageCharacter, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown1
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown1);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown1, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown2
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown2);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown2, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown3
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown3);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown3, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown4
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown4);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown4, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown5
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown5);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown5, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown6
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown6);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown6, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown7
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown7);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown7, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown8
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown8);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown8, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown9
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown9);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown9, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown10
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown10);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown10, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown11
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown11);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown11, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown12
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown12);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown12, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown13
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown13);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown13, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown14
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown14);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown14, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown15
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown15);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown15, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown16
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown16);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown16, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown17
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown17);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown17, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown18
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown18);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown18, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown19
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown19);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown19, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown20
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown20);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown20, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown21
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown21);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown21, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown22
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown22);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown22, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown23
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown23);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown23, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown24
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown24);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown24, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown25
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown25);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown25, !value);
                NotifyChange(nameof(StateFlags));
            }
        }
        public Boolean Unknown26
        {
            get => stateFlags.HasFlag(Enums.InstanceState.Unknown26);
            set
            {
                stateFlags = stateFlags.ChangeFlag(Enums.InstanceState.Unknown26, !value);
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
        public ObservableCollection<Single> FloatParams
        {
            get
            {
                return floatParams;
            }
        }
        public ObservableCollection<UInt32> IntParams
        {
            get
            {
                return intParams;
            }
        }
    }
}
