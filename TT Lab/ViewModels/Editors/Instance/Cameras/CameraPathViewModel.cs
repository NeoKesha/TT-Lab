using Caliburn.Micro;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Editors.Instance.Cameras
{
    public class CameraPathViewModel : BaseCameraViewModel
    {
        private BindableCollection<Vector4ViewModel> pathPoints;
        private BindableCollection<PrimitiveWrapperViewModel<UInt64>> unkData;

        public CameraPathViewModel(CameraSubBase cam) : base(cam)
        {
            var baseCam = (CameraPath)cam;
            pathPoints = new BindableCollection<Vector4ViewModel>();
            DirtyTracker.AddBindableCollection(pathPoints);
            foreach (var v in baseCam.PathPoints)
            {
                pathPoints.Add(new Vector4ViewModel(v));
            }
            
            unkData = new BindableCollection<PrimitiveWrapperViewModel<UInt64>>();
            DirtyTracker.AddBindableCollection(unkData);
            foreach (var d in baseCam.UnkData)
            {
                unkData.Add(new PrimitiveWrapperViewModel<UInt64>(d));
            }
        }

        public override void Save(CameraSubBase? cam)
        {
            cam ??= new CameraPath();
            var pathCam = (CameraPath)cam;
            pathCam.PathPoints.Clear();
            foreach (var p in PathPoints)
            {
                pathCam.PathPoints.Add(new Twinsanity.TwinsanityInterchange.Common.Vector4
                {
                    X = p.X,
                    Y = p.Y,
                    Z = p.Z,
                    W = p.W
                });
            }
            pathCam.UnkData.Clear();
            foreach (var d in UnkData)
            {
                pathCam.UnkData.Add(d.Value);
            }
            
            base.Save(cam);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            foreach (var point in pathPoints)
            {
                ActivateItemAsync(point, cancellationToken);
            }

            return base.OnInitializeAsync(cancellationToken);
        }

        public BindableCollection<Vector4ViewModel> PathPoints
        {
            get => pathPoints;
        }

        public BindableCollection<PrimitiveWrapperViewModel<UInt64>> UnkData
        {
            get => unkData;
        }
    }
}
