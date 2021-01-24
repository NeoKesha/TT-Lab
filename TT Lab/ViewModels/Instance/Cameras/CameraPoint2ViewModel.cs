using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Instance.Cameras
{
    public class CameraPoint2ViewModel : BaseCameraViewModel
    {
        private Vector4ViewModel point;
        private Single unkFloat3;
        private Byte unkByte;

        public CameraPoint2ViewModel(CameraPoint2 baseCam) : base(baseCam)
        {
            point = new Vector4ViewModel(baseCam.Point);
            point.PropertyChanged += Point_PropertyChanged;
            unkFloat3 = baseCam.UnkFloat3;
            unkByte = baseCam.UnkByte;
        }

        private void Point_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange(nameof(Point));
        }

        public override void Save(CameraSubBase? cam)
        {
            if (cam == null)
            {
                cam = new CameraPoint2();
            }
            var pCam = (CameraPoint2)cam;
            pCam.Point = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = point.X,
                Y = point.Y,
                Z = point.Z,
                W = point.W,
            };
            pCam.UnkFloat3 = UnkFloat3;
            pCam.UnkByte = UnkByte;
            base.Save(cam);
        }

        public override UInt32 GetIndex()
        {
            return 0x1C0B;
        }

        public Vector4ViewModel Point
        {
            get => point;
            set
            {
                if (point != value)
                {
                    point = value;
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
        public Byte UnkByte
        {
            get => unkByte;
            set
            {
                if (unkByte != value)
                {
                    unkByte = value;
                    NotifyChange();
                }
            }
        }
    }
}
