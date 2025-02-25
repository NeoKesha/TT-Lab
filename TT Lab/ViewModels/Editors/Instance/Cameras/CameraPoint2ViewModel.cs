using System;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Attributes;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Editors.Instance.Cameras
{
    public class CameraPoint2ViewModel : BaseCameraViewModel
    {
        private Vector4ViewModel point;
        private Single unkFloat3;
        private Byte unkByte;

        public CameraPoint2ViewModel(CameraSubBase cam) : base(cam)
        {
            var baseCam = (CameraPoint2)cam;
            point = new Vector4ViewModel(baseCam.Point);
            DirtyTracker.AddChild(point);
            unkFloat3 = baseCam.UnkFloat3;
            unkByte = baseCam.UnkByte;
        }

        public override void Save(CameraSubBase? cam)
        {
            cam ??= new CameraPoint2();
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

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ActivateItemAsync(point, cancellationToken);

            return base.OnInitializeAsync(cancellationToken);
        }

        public Vector4ViewModel Point
        {
            get => point;
        }

        [MarkDirty]
        public Single UnkFloat3
        {
            get => unkFloat3;
            set
            {
                if (unkFloat3 != value)
                {
                    unkFloat3 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Byte UnkByte
        {
            get => unkByte;
            set
            {
                if (unkByte != value)
                {
                    unkByte = value;
                    NotifyOfPropertyChange();
                }
            }
        }
    }
}
