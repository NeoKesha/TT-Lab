using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Editors.Instance.Cameras
{
    public class CameraZoneViewModel : BaseCameraViewModel
    {
        private BindableCollection<Vector4ViewModel> unkVecs1;
        private BindableCollection<Vector4ViewModel> unkVecs2;

        public CameraZoneViewModel(CameraSubBase cam) : base(cam)
        {
            var baseCam = (CameraZone)cam;
            unkVecs1 = new BindableCollection<Vector4ViewModel>();
            unkVecs2 = new BindableCollection<Vector4ViewModel>();
            for (var i = 0; i < 5; ++i)
            {
                unkVecs1.Add(new Vector4ViewModel(baseCam.UnkData1[i]));
                unkVecs2.Add(new Vector4ViewModel(baseCam.UnkData2[i]));
            }
        }

        public override void Save(CameraSubBase? cam)
        {
            cam ??= new CameraZone();
            var zoneCam = (CameraZone)cam;
            for (var i = 0; i < 5; ++i)
            {
                zoneCam.UnkData1[i] = new Twinsanity.TwinsanityInterchange.Common.Vector4
                {
                    X = UnkVecs1[i].X,
                    Y = UnkVecs1[i].Y,
                    Z = UnkVecs1[i].Z,
                    W = UnkVecs1[i].W,
                };
                zoneCam.UnkData2[i] = new Twinsanity.TwinsanityInterchange.Common.Vector4
                {
                    X = UnkVecs2[i].X,
                    Y = UnkVecs2[i].Y,
                    Z = UnkVecs2[i].Z,
                    W = UnkVecs2[i].W,
                };
            }
            base.Save(cam);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            for (var i = 0; i < 5; ++i)
            {
                ActivateItemAsync(unkVecs1[i], cancellationToken);
                ActivateItemAsync(unkVecs2[i], cancellationToken);
            }

            return base.OnInitializeAsync(cancellationToken);
        }

        public BindableCollection<Vector4ViewModel> UnkVecs1
        {
            get => unkVecs1;
        }

        public BindableCollection<Vector4ViewModel> UnkVecs2
        {
            get => unkVecs2;
        }
    }
}
