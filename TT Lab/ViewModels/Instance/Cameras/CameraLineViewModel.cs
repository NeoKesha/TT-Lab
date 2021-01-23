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

        public CameraLineViewModel(CameraLine baseCam) : base(baseCam)
        {
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
