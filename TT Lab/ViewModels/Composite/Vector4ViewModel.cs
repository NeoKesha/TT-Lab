using Caliburn.Micro;
using System;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Composite
{
    public class Vector4ViewModel : Screen, ISaveableViewModel<Vector4>
    {
        private float _x;
        private float _y;
        private float _z;
        private float _w;

        public Vector4ViewModel() { }

        public Vector4ViewModel(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4ViewModel(Vector4 twinVec) : this(twinVec.X, twinVec.Y, twinVec.Z, twinVec.W)
        {
        }

        public void Save(Vector4 o)
        {
            o.X = X;
            o.Y = Y;
            o.Z = Z;
            o.W = W;
        }

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                NotifyOfPropertyChange();
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                NotifyOfPropertyChange();
            }
        }

        public float Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
                NotifyOfPropertyChange();
            }
        }

        public float W
        {
            get
            {
                return _w;
            }
            set
            {
                _w = value;
                NotifyOfPropertyChange();
            }
        }

        public override String ToString()
        {
            return $"({X}; {Y}; {Z}; {W})";
        }
    }
}
