using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Instance.Cameras
{
    public class CameraZoneViewModel : BaseCameraViewModel
    {
        private Vector4ViewModel[] unkVecs1;
        private Vector4ViewModel[] unkVecs2;

        public CameraZoneViewModel(CameraZone baseCam) : base(baseCam)
        {
            unkVecs1 = new Vector4ViewModel[5];
            unkVecs2 = new Vector4ViewModel[5];
            for (var i = 0; i < 5; ++i)
            {
                unkVecs1[i] = new Vector4ViewModel(baseCam.UnkData1[i]);
                unkVecs2[i] = new Vector4ViewModel(baseCam.UnkData2[i]);
                unkVecs1[i].PropertyChanged += Vector_PropertyChanged;
            }
        }

        private void Vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange(nameof(UnkVecs1));
            NotifyChange(nameof(UnkVecs2));
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
