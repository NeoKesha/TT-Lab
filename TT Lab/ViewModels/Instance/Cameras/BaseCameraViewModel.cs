using System;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Instance.Cameras
{
    public class BaseCameraViewModel : ObservableObject
    {
        private UInt32 unkInt;
        private Single unkFloat1;
        private Single unkFloat2;

        public BaseCameraViewModel(CameraSubBase baseCam)
        {
            unkInt = baseCam.UnkInt;
            unkFloat1 = baseCam.UnkFloat1;
            unkFloat2 = baseCam.UnkFloat2;
        }

        public virtual void Save(CameraSubBase? cam)
        {
            cam.UnkInt = UnkInt;
            cam.UnkFloat1 = UnkFloat1;
            cam.UnkFloat2 = UnkFloat2;
        }

        public UInt32 UnkInt
        {
            get => unkInt;
            set
            {
                if (unkInt != value)
                {
                    unkInt = value;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat1
        {
            get => unkFloat1;
            set
            {
                if (unkFloat1 != value)
                {
                    unkFloat1 = value;
                    NotifyChange();
                }
            }
        }
        public Single UnkFloat2
        {
            get => unkFloat2;
            set
            {
                if (unkFloat2 != value)
                {
                    unkFloat2 = value;
                    NotifyChange();
                }
            }
        }
    }
}
