using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels
{
    public class Vector4ViewModel : SavebleViewModel
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

        public override void Save(Object? o = null)
        {
            var v = (Vector4)o!;
            v.X = X;
            v.Y = Y;
            v.Z = Z;
            v.W = W;
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

        public float W
        {
            get
            {
                return _w;
            }
            set
            {
                _w = value;
                NotifyChange();
            }
        }
    }
}
