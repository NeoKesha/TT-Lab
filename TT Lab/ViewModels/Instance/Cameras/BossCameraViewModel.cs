using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Instance.Cameras
{
    public class BossCameraViewModel : BaseCameraViewModel
    {
        private Vector4ViewModel[] unkMatrix1;
        private Vector4ViewModel[] unkMatrix2;
        private Vector4ViewModel unkVec;
        private Byte unkByte1;
        private Single unkFloat3;
        private Single unkFloat4;
        private Single unkFloat5;
        private Single unkFloat6;
        private Byte unkByte2;

        public BossCameraViewModel(CameraSubBase cam) : base(cam)
        {
            var bossCam = (BossCamera)cam;
            unkMatrix1 = new Vector4ViewModel[4];
            unkMatrix2 = new Vector4ViewModel[4];
            for (var i = 0; i < 4; ++i)
            {
                unkMatrix1[i] = new Vector4ViewModel(bossCam.UnkMatrix1[i]);
                unkMatrix1[i].PropertyChanged += Vector_PropertyChanged;
                unkMatrix2[i] = new Vector4ViewModel(bossCam.UnkMatrix2[i]);
                unkMatrix2[i].PropertyChanged += Vector_PropertyChanged;
            }
            unkVec = new Vector4ViewModel(bossCam.UnkVector);
            unkVec.PropertyChanged += Vector_PropertyChanged;
            unkByte1 = bossCam.UnkByte1;
            unkByte2 = bossCam.UnkByte2;
            unkFloat3 = bossCam.UnkFloat3;
            unkFloat4 = bossCam.UnkFloat4;
            unkFloat5 = bossCam.UnkFloat5;
            unkFloat6 = bossCam.UnkFloat6;
        }

        public override void Save(CameraSubBase? cam)
        {
            if (cam == null)
            {
                cam = new BossCamera();
            }
            var bossCam = (BossCamera)cam;
            for (var i = 0; i < 4; ++i)
            {
                bossCam.UnkMatrix1[i] = new Twinsanity.TwinsanityInterchange.Common.Vector4
                {
                    X = UnkMatrix1[i].X,
                    Y = UnkMatrix1[i].Y,
                    Z = UnkMatrix1[i].Z,
                    W = UnkMatrix1[i].W,
                };
                bossCam.UnkMatrix2[i] = new Twinsanity.TwinsanityInterchange.Common.Vector4
                {
                    X = UnkMatrix2[i].X,
                    Y = UnkMatrix2[i].Y,
                    Z = UnkMatrix2[i].Z,
                    W = UnkMatrix2[i].W,
                };
            }
            bossCam.UnkVector = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = UnkVec.X,
                Y = UnkVec.Y,
                Z = UnkVec.Z,
                W = UnkVec.W,
            };
            bossCam.UnkByte1 = UnkByte1;
            bossCam.UnkByte2 = UnkByte2;
            bossCam.UnkFloat3 = UnkFloat3;
            bossCam.UnkFloat5 = UnkFloat4;
            bossCam.UnkFloat6 = UnkFloat5;
            bossCam.UnkFloat6 = UnkFloat6;
            base.Save(cam);
        }

        public override UInt32 GetIndex()
        {
            return 0xA19;
        }

        private void Vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange(nameof(UnkMatrix1));
            NotifyChange(nameof(UnkMatrix2));
            NotifyChange(nameof(UnkVec));
        }

        public Vector4ViewModel[] UnkMatrix1
        {
            get => unkMatrix1;
            set
            {
                if (unkMatrix1 != value)
                {
                    unkMatrix1 = value;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel[] UnkMatrix2
        {
            get => unkMatrix2;
            set
            {
                if (unkMatrix2 != value)
                {
                    unkMatrix2 = value;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel UnkVec
        {
            get => unkVec;
            set
            {
                if (unkVec != value)
                {
                    unkVec = value;
                    NotifyChange();
                }
            }
        }
        public Byte UnkByte1
        {
            get => unkByte1;
            set
            {
                if (unkByte1 != value)
                {
                    unkByte1 = value;
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
                    NotifyChange();
                }
            }
        }
        public Byte UnkByte2
        {
            get => unkByte2;
            set
            {
                if (unkByte2 != value)
                {
                    unkByte2 = value;
                    NotifyChange();
                }
            }
        }
    }
}
