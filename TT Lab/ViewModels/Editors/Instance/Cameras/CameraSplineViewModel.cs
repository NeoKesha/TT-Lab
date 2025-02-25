using Caliburn.Micro;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Attributes;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Editors.Instance.Cameras
{
    public class CameraSplineViewModel : BaseCameraViewModel
    {
        private Single unkFloat3;
        private BindableCollection<Vector4ViewModel> pathPoints;
        private BindableCollection<Vector4ViewModel> interpolationPoints;
        private BindableCollection<Vector2ViewModel> unkData;
        private UInt16 unkShort;

        public CameraSplineViewModel(CameraSubBase cam) : base(cam)
        {
            var baseCam = (CameraSpline)cam;
            unkFloat3 = baseCam.UnkFloat3;
            pathPoints = new BindableCollection<Vector4ViewModel>();
            DirtyTracker.AddBindableCollection(pathPoints);
            foreach (var v in baseCam.PathPoints)
            {
                pathPoints.Add(new Vector4ViewModel(v));
            }
            interpolationPoints = new BindableCollection<Vector4ViewModel>();
            DirtyTracker.AddBindableCollection(interpolationPoints);
            foreach (var v in baseCam.InterpolationPoints)
            {
                interpolationPoints.Add(new Vector4ViewModel(v));
            }
            unkData = new BindableCollection<Vector2ViewModel>();
            DirtyTracker.AddBindableCollection(unkData);
            foreach (var d in baseCam.UnkData)
            {
                unkData.Add(new Vector2ViewModel(d));
            }
            unkShort = baseCam.UnkShort;
        }

        public override void Save(CameraSubBase? cam)
        {
            cam ??= new CameraSpline();
            var splineCam = (CameraSpline)cam;
            splineCam.UnkFloat3 = UnkFloat3;
            splineCam.UnkShort = UnkShort;
            splineCam.PathPoints.Clear();
            foreach (var p in PathPoints)
            {
                splineCam.PathPoints.Add(new Twinsanity.TwinsanityInterchange.Common.Vector4
                {
                    X = p.X,
                    Y = p.Y,
                    Z = p.Z,
                    W = p.W,
                });
            }
            splineCam.InterpolationPoints.Clear();
            foreach (var ip in InterpolationPoints)
            {
                splineCam.InterpolationPoints.Add(new Twinsanity.TwinsanityInterchange.Common.Vector4
                {
                    X = ip.X,
                    Y = ip.Y,
                    Z = ip.Z,
                    W = ip.W,
                });
            }
            splineCam.UnkData.Clear();
            foreach (var d in UnkData)
            {
                splineCam.UnkData.Add(new Twinsanity.TwinsanityInterchange.Common.Vector2
                {
                    X = d.X,
                    Y = d.Y
                });
            }
            base.Save(cam);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            foreach (var point in pathPoints)
            {
                ActivateItemAsync(point, cancellationToken);
            }

            foreach (var interPoint in interpolationPoints)
            {
                ActivateItemAsync(interPoint, cancellationToken);
            }

            foreach (var data in unkData)
            {
                ActivateItemAsync(data, cancellationToken);
            }

            return base.OnInitializeAsync(cancellationToken);
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

        public BindableCollection<Vector4ViewModel> PathPoints
        {
            get => pathPoints;
        }

        public BindableCollection<Vector4ViewModel> InterpolationPoints
        {
            get => interpolationPoints;
        }

        public BindableCollection<Vector2ViewModel> UnkData
        {
            get => unkData;
        }

        [MarkDirty]
        public UInt16 UnkShort
        {
            get => unkShort;
            set
            {
                if (unkShort != value)
                {
                    unkShort = value;
                    NotifyOfPropertyChange();
                }
            }
        }
    }
}
