using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Instance.Cameras
{
    public class CameraPointViewModel : BaseCameraViewModel
    {
        private Vector4ViewModel point;

        public CameraPointViewModel(CameraPoint cameraPoint) : base(cameraPoint)
        {
            point = new Vector4ViewModel(cameraPoint.Point);
            point.PropertyChanged += Point_PropertyChanged;
        }

        private void Point_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange(nameof(Point));
        }

        public override void Save(CameraSubBase? cam)
        {
            if (cam == null)
            {
                cam = new CameraPoint();
            }
            var pCam = (CameraPoint)cam;
            pCam.Point = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = point.X,
                Y = point.Y,
                Z = point.Z,
                W = point.W,
            };
            base.Save(cam);
        }

        public override UInt32 GetIndex()
        {
            return 0x1C02;
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
    }
}
