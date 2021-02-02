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

namespace TT_Lab.ViewModels.Instance
{
    public class ObjectInstanceViewModel : AssetViewModel
    {
        private Vector4ViewModel position;
        private Single rotX;
        private Single rotY;
        private Single rotZ;
        private ObservableCollection<UInt16> instances;
        private ObservableCollection<UInt16> paths;
        private ObservableCollection<UInt16> positions;
        private Guid objectId;
        private Int16 refListIndex;
        private Guid onSpawnScriptId;
        private UInt32 stateFlags;
        private ObservableCollection<UInt32> flagParams;
        private ObservableCollection<Single> floatParams;
        private ObservableCollection<UInt32> intParams;

        public ObjectInstanceViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var data = (ObjectInstanceData)_asset.GetData();
            position = new Vector4ViewModel(data.Position);
            rotX = data.RotationX.GetRotation();
            rotY = data.RotationY.GetRotation();
            rotZ = data.RotationZ.GetRotation();
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
            stateFlags = data.StateFlags;
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
        }

        public override void Save(Object? o)
        {
            var data = (ObjectInstanceData)_asset.GetData();
            Position.Save(data.Position);
            data.RotationX.SetRotation(RotationX);
            data.RotationY.SetRotation(RotationY);
            data.RotationZ.SetRotation(RotationZ);
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
            data.StateFlags = stateFlags;
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
        public Single RotationX
        {
            get
            {
                return rotX;
            }
            set
            {
                if (value != rotX)
                {
                    rotX = value;
                    NotifyChange();
                    IsDirty = true;
                }
            }
        }
        public Single RotationY
        {
            get
            {
                return rotY;
            }
            set
            {
                if (value != rotY)
                {
                    rotY = value;
                    NotifyChange();
                    IsDirty = true;
                }
            }
        }
        public Single RotationZ
        {
            get
            {
                return rotZ;
            }
            set
            {
                if (value != rotZ)
                {
                    rotZ = value;
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
                return RefListIndex;
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
                return stateFlags;
            }
            set
            {
                if (value != stateFlags)
                {
                    stateFlags = value;
                    NotifyChange();
                    IsDirty = true;
                }
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
