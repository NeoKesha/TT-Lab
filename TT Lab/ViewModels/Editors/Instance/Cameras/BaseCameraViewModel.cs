using Caliburn.Micro;
using System;
using TT_Lab.Attributes;
using TT_Lab.Util;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Editors.Instance.Cameras
{
    public class BaseCameraViewModel : Conductor<IScreen>.Collection.AllActive, ISaveableViewModel<CameraSubBase?>, IHaveChildrenEditors
    {
        private UInt32 unkInt;
        private Single unkFloat1;
        private Single unkFloat2;
        private bool isDirty;
        private DirtyTracker dirtyTracker;

        public BaseCameraViewModel(CameraSubBase baseCam)
        {
            dirtyTracker = new DirtyTracker(this);
            unkInt = baseCam.UnkInt;
            unkFloat1 = baseCam.UnkFloat1;
            unkFloat2 = baseCam.UnkFloat2;
        }

        public virtual void ResetDirty()
        {
            dirtyTracker.ResetDirty();
            IsDirty = false;
        }

        public bool IsDirty
        {
            get => isDirty;
            set
            {
                if (isDirty != value)
                {
                    isDirty = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public virtual void Save(CameraSubBase? cam)
        {
            cam.UnkInt = UnkInt;
            cam.UnkFloat1 = UnkFloat1;
            cam.UnkFloat2 = UnkFloat2;
            
            ResetDirty();
        }

        [MarkDirty]
        public UInt32 UnkInt
        {
            get => unkInt;
            set
            {
                if (unkInt != value)
                {
                    unkInt = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat1
        {
            get => unkFloat1;
            set
            {
                if (unkFloat1 != value)
                {
                    unkFloat1 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        [MarkDirty]
        public Single UnkFloat2
        {
            get => unkFloat2;
            set
            {
                if (unkFloat2 != value)
                {
                    unkFloat2 = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public DirtyTracker DirtyTracker => dirtyTracker;
    }
}
