using Caliburn.Micro;
using System;
using TT_Lab.Util;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Composite
{
    public class Matrix4ViewModel : Conductor<Vector4ViewModel>.Collection.AllActive, ISaveableViewModel<Matrix4>, IHaveChildrenEditors
    {
        private Vector4ViewModel _v1;
        private Vector4ViewModel _v2;
        private Vector4ViewModel _v3;
        private Vector4ViewModel _v4;
        private bool isDirty;
        private readonly DirtyTracker dirtyTracker;

        public Matrix4ViewModel()
        {
            dirtyTracker = new DirtyTracker(this);
            _v1 = new Vector4ViewModel();
            _v2 = new Vector4ViewModel();
            _v3 = new Vector4ViewModel();
            _v4 = new Vector4ViewModel();
            for (var i = 0; i < 4; i++)
            {
                dirtyTracker.AddChild(this[i]);
            }
        }

        public Matrix4ViewModel(Matrix4 m)
        {
            dirtyTracker = new DirtyTracker(this);
            _v1 = new Vector4ViewModel(m.Column1);
            _v2 = new Vector4ViewModel(m.Column2);
            _v3 = new Vector4ViewModel(m.Column3);
            _v4 = new Vector4ViewModel(m.Column4);
            for (var i = 0; i < 4; i++)
            {
                dirtyTracker.AddChild(this[i]);
            }
        }

        public Vector4ViewModel this[int key]
        {
            get
            {
                return key switch
                {
                    0 => _v1,
                    1 => _v2,
                    2 => _v3,
                    3 => _v4,
                    _ => throw new IndexOutOfRangeException(),
                };
            }
            set
            {
                switch (key)
                {
                    case 0:
                        _v1 = value;
                        break;
                    case 1:
                        _v2 = value;
                        break;
                    case 2:
                        _v3 = value;
                        break;
                    case 3:
                        _v4 = value;
                        break;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public Vector4ViewModel V1 => _v1;
        public Vector4ViewModel V2 => _v2;
        public Vector4ViewModel V3 => _v3;
        public Vector4ViewModel V4 => _v4;

        public void ResetDirty()
        {
            dirtyTracker.ResetDirty();
        }

        public bool IsDirty => dirtyTracker.IsDirty;

        public void Save(Matrix4 o)
        {
            var m = o;
            _v1.Save(m.Column1);
            _v2.Save(m.Column2);
            _v3.Save(m.Column3);
            _v4.Save(m.Column4);
            
            ResetDirty();
        }

        public DirtyTracker DirtyTracker => dirtyTracker;
    }
}
