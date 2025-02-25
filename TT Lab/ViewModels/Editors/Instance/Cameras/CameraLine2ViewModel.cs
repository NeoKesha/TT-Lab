using System;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Attributes;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Editors.Instance.Cameras
{
    public class CameraLine2ViewModel : BaseCameraViewModel
    {
        private Vector4ViewModel lineStart;
        private Vector4ViewModel lineEnd;
        private Single unkFloat3;
        private Single unkFloat4;

        public CameraLine2ViewModel(CameraSubBase cam) : base(cam)
        {
            var baseCam = (CameraLine2)cam;
            lineStart = new Vector4ViewModel(baseCam.LineStart);
            lineEnd = new Vector4ViewModel(baseCam.LineEnd);
            DirtyTracker.AddChild(lineStart);
            DirtyTracker.AddChild(lineEnd);
            unkFloat3 = baseCam.UnkFloat3;
            unkFloat4 = baseCam.UnkFloat4;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ActivateItemAsync(lineStart, cancellationToken);
            ActivateItemAsync(lineEnd, cancellationToken);

            return base.OnInitializeAsync(cancellationToken);
        }

        public override void Save(CameraSubBase? cam)
        {
            cam ??= new CameraLine2();
            var lineCam = (CameraLine2)cam;
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
            lineCam.UnkFloat3 = UnkFloat3;
            lineCam.UnkFloat4 = UnkFloat4;
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
        public Single UnkFloat4
        {
            get => unkFloat4;
            set
            {
                if (unkFloat4 != value)
                {
                    unkFloat4 = value;
                    NotifyOfPropertyChange();
                }
            }
        }
    }
}
