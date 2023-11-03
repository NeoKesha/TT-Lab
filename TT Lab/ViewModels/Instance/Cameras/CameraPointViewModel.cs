using System;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Instance.Cameras
{
    public class CameraPointViewModel : BaseCameraViewModel
    {
        private Vector4ViewModel point;

        public CameraPointViewModel(CameraSubBase cam) : base(cam)
        {
            var cameraPoint = (CameraPoint)cam;
            point = new Vector4ViewModel(cameraPoint.Point);
            point.PropertyChanged += Point_PropertyChanged;
        }

        private void Point_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange(nameof(Point));
        }

        public override void Save(CameraSubBase? cam)
        {
            cam ??= new CameraPoint();
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
