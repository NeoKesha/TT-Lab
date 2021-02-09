using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Instance.Cameras
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
            lineEnd.PropertyChanged += Vector_PropertyChanged;
            lineStart.PropertyChanged += Vector_PropertyChanged;
        }

        private void Vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange(nameof(LineStart));
            NotifyChange(nameof(LineEnd));
        }

        public override void Save(CameraSubBase? cam)
        {
            if (cam == null)
            {
                cam = new CameraLine();
            }
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

        public override UInt32 GetIndex()
        {
            return 0x1C03;
        }

        public Vector4ViewModel LineStart
        {
            get => lineStart;
            set
            {
                if (lineStart != value)
                {
                    lineStart = value;
                    NotifyChange();
                }
            }
        }
        public Vector4ViewModel LineEnd
        {
            get => lineEnd;
            set
            {
                if (lineEnd != value)
                {
                    lineEnd = value;
                    NotifyChange();
                }
            }
        }
    }
}
