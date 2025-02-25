using System.Threading;
using System.Threading.Tasks;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Editors.Instance.Cameras
{
    public class CameraLineViewModel : BaseCameraViewModel
    {
        private Vector4ViewModel lineStart;
        private Vector4ViewModel lineEnd;

        public CameraLineViewModel(CameraSubBase cam) : base(cam)
        {
            var baseCam = (CameraLine)cam;
            lineStart = new Vector4ViewModel(baseCam.LineStart);
            lineEnd = new Vector4ViewModel(baseCam.LineEnd);
            DirtyTracker.AddChild(lineStart);
            DirtyTracker.AddChild(lineEnd);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ActivateItemAsync(lineStart, cancellationToken);
            ActivateItemAsync(lineEnd, cancellationToken);

            return base.OnInitializeAsync(cancellationToken);
        }

        public override void Save(CameraSubBase? cam)
        {
            cam ??= new CameraLine();
            var lineCam = (CameraLine)cam;
            lineCam.LineStart = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = LineStart.X,
                Y = LineStart.Y,
                Z = LineStart.Z,
                W = LineStart.W,
            };
            lineCam.LineEnd = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = LineEnd.X,
                Y = LineEnd.Y,
                Z = LineEnd.Z,
                W = LineEnd.W,
            };
            base.Save(cam);
        }

        public Vector4ViewModel LineStart
        {
            get => lineStart;
        }

        public Vector4ViewModel LineEnd
        {
            get => lineEnd;
        }
    }
}
