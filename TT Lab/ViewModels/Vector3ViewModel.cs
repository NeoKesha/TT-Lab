using System;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels
{
    public class Vector3ViewModel : SaveableViewModel
    {
        private float _x;
        private float _y;
        private float _z;

        public Vector3ViewModel() { }
        public Vector3ViewModel(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3ViewModel(Vector3 twinVec) : this(twinVec.X, twinVec.Y, twinVec.Z)
        {
        }

        public override void Save(Object? o = null)
        {
            var v = (Vector3)o!;
            v.X = X;
            v.Y = Y;
            v.Z = Z;
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
                NotifyChange();
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
                NotifyChange();
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
                NotifyChange();
            }
        }
    }
}
