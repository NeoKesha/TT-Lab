using System;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Instance.Cameras
{
    public class CameraZoneViewModel : BaseCameraViewModel
    {
        private Vector4ViewModel[] unkVecs1;
        private Vector4ViewModel[] unkVecs2;

        public CameraZoneViewModel(CameraSubBase cam) : base(cam)
        {
            var baseCam = (CameraZone)cam;
            unkVecs1 = new Vector4ViewModel[5];
            unkVecs2 = new Vector4ViewModel[5];
            for (var i = 0; i < 5; ++i)
            {
                unkVecs1[i] = new Vector4ViewModel(baseCam.UnkData1[i]);
                unkVecs2[i] = new Vector4ViewModel(baseCam.UnkData2[i]);
                unkVecs1[i].PropertyChanged += Vector_PropertyChanged;
                unkVecs2[i].PropertyChanged += Vector_PropertyChanged;
            }
        }

        private void Vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange(nameof(UnkVecs1));
            NotifyChange(nameof(UnkVecs2));
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

        public Vector4ViewModel[] UnkVecs1
        {
            get => unkVecs1;
        }

        public Vector4ViewModel[] UnkVecs2
        {
            get => unkVecs2;
        }
    }
}
