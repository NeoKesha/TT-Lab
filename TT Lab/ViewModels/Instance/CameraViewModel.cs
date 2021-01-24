using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels.Instance.Cameras;

namespace TT_Lab.ViewModels.Instance
{
    public class CameraViewModel : AssetViewModel
    {
        private static readonly Dictionary<UInt32, Type> subIdToCamVM = new Dictionary<uint, Type>();

        private TriggerViewModel trigger;
        private UInt32 cameraHeader;
        private UInt16 unkShort;
        private Single unkFloat1;
        private Vector4ViewModel unkVector1;
        private Vector4ViewModel unkVector2;
        private Single unkFloat2;
        private Single unkFloat3;
        private UInt32 unkInt1;
        private UInt32 unkInt2;
        private UInt32 unkInt3;
        private UInt32 unkInt4;
        private UInt32 unkInt5;
        private UInt32 unkInt6;
        private Single unkFloat4;
        private Single unkFloat5;
        private Single unkFloat6;
        private Single unkFloat7;
        private UInt32 unkInt7;
        private UInt32 unkInt8;
        private Single unkFloat8;
        private Byte unkByte;
        private BaseCameraViewModel? mainCamera1;
        private BaseCameraViewModel? mainCamera2;

        public CameraViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var data = (CameraData)_asset.GetData();
            trigger = new TriggerViewModel(asset, parent, data.Trigger);
            trigger.PropertyChanged += Trigger_PropertyChanged;
            cameraHeader = data.CameraHeader;
            unkShort = data.UnkShort;
            unkFloat1 = data.UnkFloat1;
            unkVector1 = new Vector4ViewModel(data.UnkVector1);
            unkVector2 = new Vector4ViewModel(data.UnkVector2);
            unkVector1.PropertyChanged += Vector_PropertyChanged;
            unkVector2.PropertyChanged += Vector_PropertyChanged;
            unkFloat2 = data.UnkFloat2;
            unkFloat3 = data.UnkFloat3;
            unkInt1 = data.UnkInt1;
            unkInt2 = data.UnkInt2;
            unkInt3 = data.UnkInt3;
            unkInt4 = data.UnkInt4;
            unkInt5 = data.UnkInt5;
            unkInt6 = data.UnkInt6;
            unkFloat4 = data.UnkFloat4;
            unkFloat5 = data.UnkFloat5;
            unkFloat6 = data.UnkFloat6;
            unkFloat7 = data.UnkFloat7;
            unkInt7 = data.UnkInt7;
            unkInt8 = data.UnkInt8;
            unkFloat8 = data.UnkFloat8;
            unkByte = data.UnkByte;
            if (subIdToCamVM.ContainsKey(data.TypeIndex1))
            {
                mainCamera1 = (BaseCameraViewModel)Activator.CreateInstance(subIdToCamVM[data.TypeIndex1], data.MainCamera1)!;
                mainCamera1.PropertyChanged += MainCamera1_PropertyChanged;
            }
            if (subIdToCamVM.ContainsKey(data.TypeIndex2))
            {
                mainCamera2 = (BaseCameraViewModel)Activator.CreateInstance(subIdToCamVM[data.TypeIndex2], data.MainCamera2)!;
                mainCamera2.PropertyChanged += MainCamera2_PropertyChanged;
            }
        }

        private void MainCamera1_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(MainCamera1));
        }

        private void MainCamera2_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(MainCamera2));
        }

        static CameraViewModel()
        {
            subIdToCamVM.Add(0xA19, typeof(BossCameraViewModel));
            subIdToCamVM.Add(0x1C02, typeof(CameraPointViewModel));
            subIdToCamVM.Add(0x1C03, typeof(CameraLineViewModel));
            subIdToCamVM.Add(0x1C04, typeof(CameraPathViewModel));
            subIdToCamVM.Add(0x1C06, typeof(CameraSplineViewModel));
            subIdToCamVM.Add(0x1C0B, typeof(CameraPoint2ViewModel));
            subIdToCamVM.Add(0x1C0D, typeof(CameraLine2ViewModel));
            subIdToCamVM.Add(0x1C0F, typeof(CameraZoneViewModel));
        }

        private void Trigger_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(Trigger));
        }

        private void Vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(UnkVector1));
            NotifyChange(nameof(UnkVector2));
        }

        public override void Save()
        {
            _asset.LayoutID = (int)Trigger.LayoutID;
            var data = (CameraData)_asset.GetData();
            Trigger.Save(data.Trigger);
            data.CameraHeader = CameraHeader;
            data.UnkShort = UnkShort;
            data.UnkFloat1 = UnkFloat1;
            data.UnkFloat2 = UnkFloat2;
            data.UnkFloat3 = UnkFloat3;
            data.UnkFloat4 = UnkFloat4;
            data.UnkFloat5 = UnkFloat5;
            data.UnkFloat6 = UnkFloat6;
            data.UnkFloat7 = UnkFloat7;
            data.UnkFloat8 = UnkFloat8;
            data.UnkVector1 = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = UnkVector1.X,
                Y = UnkVector1.Y,
                Z = UnkVector1.Z,
                W = UnkVector1.W,
            };
            data.UnkVector2 = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = UnkVector2.X,
                Y = UnkVector2.Y,
                Z = UnkVector2.Z,
                W = UnkVector2.W,
            };
            data.UnkInt1 = UnkInt1;
            data.UnkInt2 = UnkInt2;
            data.UnkInt3 = UnkInt3;
            data.UnkInt4 = UnkInt4;
            data.UnkInt5 = UnkInt5;
            data.UnkInt6 = UnkInt6;
            data.UnkInt7 = UnkInt7;
            data.UnkInt8 = UnkInt8;
            data.UnkByte = UnkByte;
            if (MainCamera1 != null)
            {
                data.TypeIndex1 = MainCamera1.GetIndex();
                MainCamera1.Save(data.MainCamera1);
            }
            else
            {
                data.TypeIndex1 = 3;
                data.MainCamera1 = null;
            }
            if (MainCamera2 != null)
            {
                data.TypeIndex2 = MainCamera2.GetIndex();
                MainCamera2.Save(data.MainCamera2);
            }
            else
            {
                data.TypeIndex2 = 3;
                data.MainCamera2 = null;
            }
            base.Save();
        }

        public TriggerViewModel Trigger
        {
            get => trigger;
            set
            {
                if (trigger != value)
                {
                    trigger = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt32 CameraHeader
        {
            get => cameraHeader;
            set
            {
                if (cameraHeader != value)
                {
                    cameraHeader = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt16 UnkShort
        {
            get => unkShort;
            set
            {
                if (unkShort != value)
                {
                    unkShort = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat1
        {
            get => unkFloat1;
            set
            {
                if (unkFloat1 != value)
                {
                    unkFloat1 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel UnkVector1
        {
            get => unkVector1;
            set
            {
                if (unkVector1 != value)
                {
                    unkVector1 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel UnkVector2
        {
            get => unkVector2;
            set
            {
                if (unkVector2 != value)
                {
                    unkVector2 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat2
        {
            get => unkFloat2;
            set
            {
                if (unkFloat2 != value)
                {
                    unkFloat2 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat3
        {
            get => unkFloat3;
            set
            {
                if (unkFloat3 != value)
                {
                    unkFloat3 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt32 UnkInt1
        {
            get => unkInt1;
            set
            {
                if (unkInt1 != value)
                {
                    unkInt1 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt32 UnkInt2
        {
            get => unkInt2;
            set
            {
                if (unkInt2 != value)
                {
                    unkInt2 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt32 UnkInt3
        {
            get => unkInt3;
            set
            {
                if (unkInt3 != value)
                {
                    unkInt3 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt32 UnkInt4
        {
            get => unkInt4;
            set
            {
                if (unkInt4 != value)
                {
                    unkInt4 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt32 UnkInt5
        {
            get => unkInt5;
            set
            {
                if (unkInt5 != value)
                {
                    unkInt5 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt32 UnkInt6
        {
            get => unkInt6;
            set
            {
                if (unkInt6 != value)
                {
                    unkInt6 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat4
        {
            get => unkFloat4;
            set
            {
                if (unkFloat4 != value)
                {
                    unkFloat4 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat5
        {
            get => unkFloat5;
            set
            {
                if (unkFloat5 != value)
                {
                    unkFloat5 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat6
        {
            get => unkFloat6;
            set
            {
                if (unkFloat6 != value)
                {
                    unkFloat6 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat7
        {
            get => unkFloat7;
            set
            {
                if (unkFloat7 != value)
                {
                    unkFloat7 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt32 UnkInt7
        {
            get => unkInt7;
            set
            {
                if (unkInt7 != value)
                {
                    unkInt7 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt32 UnkInt8
        {
            get => unkInt8;
            set
            {
                if (unkInt8 != value)
                {
                    unkInt8 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat8
        {
            get => unkFloat8;
            set
            {
                if (unkFloat8 != value)
                {
                    unkFloat8 = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public Byte UnkByte
        {
            get => unkByte;
            set
            {
                if (unkByte != value)
                {
                    unkByte = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public BaseCameraViewModel? MainCamera1
        {
            get => mainCamera1;
            set
            {
                if (mainCamera1 != value)
                {
                    if (value == null)
                    {
                        mainCamera1!.PropertyChanged -= MainCamera1_PropertyChanged;
                    }
                    mainCamera1 = value;
                    NotifyChange();
                }
            }
        }
        public BaseCameraViewModel? MainCamera2
        {
            get => mainCamera2;
            set
            {
                if (mainCamera2 != value)
                {
                    if (value == null)
                    {
                        mainCamera2!.PropertyChanged -= MainCamera2_PropertyChanged;
                    }
                    mainCamera2 = value;
                    NotifyChange();
                }
            }
        }
    }
}
