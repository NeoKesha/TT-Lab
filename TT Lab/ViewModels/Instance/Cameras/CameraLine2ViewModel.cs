﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Instance.Cameras
{
    public class CameraLine2ViewModel : BaseCameraViewModel
    {
        private Vector4ViewModel lineStart;
        private Vector4ViewModel lineEnd;
        private Single unkFloat3;
        private Single unkFloat4;

        public CameraLine2ViewModel(CameraLine2 baseCam) : base(baseCam)
        {
            lineStart = new Vector4ViewModel(baseCam.LineStart);
            lineEnd = new Vector4ViewModel(baseCam.LineEnd);
            lineEnd.PropertyChanged += Vector_PropertyChanged;
            lineStart.PropertyChanged += Vector_PropertyChanged;
            unkFloat3 = baseCam.UnkFloat3;
            unkFloat4 = baseCam.UnkFloat4;
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
                cam = new CameraLine2();
            }
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

        public override UInt32 GetIndex()
        {
            return 0x1C0D;
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
        public Single UnkFloat3
        {
            get => unkFloat3;
            set
            {
                if (unkFloat3 != value)
                {
                    unkFloat3 = value;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat4
        {
            get => unkFloat4;
            set
            {
                if (unkFloat4 != value)
                {
                    unkFloat4 = value;
                    NotifyChange();
                }
            }
        }
    }
}
