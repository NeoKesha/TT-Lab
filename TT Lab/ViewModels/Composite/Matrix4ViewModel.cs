using Caliburn.Micro;
using System;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Composite
{
    public class Matrix4ViewModel : Conductor<Vector4ViewModel>.Collection.AllActive, ISaveableViewModel<Matrix4>
    {
        private Vector4ViewModel V1;
        private Vector4ViewModel V2;
        private Vector4ViewModel V3;
        private Vector4ViewModel V4;

        public Matrix4ViewModel()
        {
            V1 = new Vector4ViewModel();
            V2 = new Vector4ViewModel();
            V3 = new Vector4ViewModel();
            V4 = new Vector4ViewModel();
        }

        public Matrix4ViewModel(Matrix4 m)
        {
            V1 = new Vector4ViewModel(m.Column1);
            V2 = new Vector4ViewModel(m.Column2);
            V3 = new Vector4ViewModel(m.Column3);
            V4 = new Vector4ViewModel(m.Column4);
        }

        public Vector4ViewModel this[int key]
        {
            get
            {
                return key switch
                {
                    0 => V1,
                    1 => V2,
                    2 => V3,
                    3 => V4,
                    _ => throw new IndexOutOfRangeException(),
                };
            }
            set
            {
                switch (key)
                {
                    case 0:
                        V1 = value;
                        break;
                    case 1:
                        V2 = value;
                        break;
                    case 2:
                        V3 = value;
                        break;
                    case 3:
                        V4 = value;
                        break;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public void Save(Matrix4 o)
        {
            var m = o;
            V1.Save(m.Column1);
            V2.Save(m.Column2);
            V3.Save(m.Column3);
            V4.Save(m.Column4);
        }
    }
}
