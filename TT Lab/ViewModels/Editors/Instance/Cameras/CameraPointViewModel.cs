using System.Threading;
using System.Threading.Tasks;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.ViewModels.Editors.Instance.Cameras
{
    public class CameraPointViewModel : BaseCameraViewModel
    {
        private Vector4ViewModel point;

        public CameraPointViewModel()
        {
            CameraType = ITwinCamera.CameraType.CameraPoint;
            point = new Vector4ViewModel();
            DirtyTracker.AddChild(point);
        }

        public CameraPointViewModel(CameraSubBase cam) : base(cam)
        {
            var cameraPoint = (CameraPoint)cam;
            point = new Vector4ViewModel(cameraPoint.Point);
            DirtyTracker.AddChild(point);
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

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ActivateItemAsync(point, cancellationToken);

            return base.OnInitializeAsync(cancellationToken);
        }

        public Vector4ViewModel Point
        {
            get => point;
        }
    }
}
