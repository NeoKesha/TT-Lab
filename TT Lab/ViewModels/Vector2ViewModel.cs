using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels
{
    public class Vector2ViewModel : SavebleViewModel
    {
        private float _x;
        private float _y;

        public Vector2ViewModel() { }
        public Vector2ViewModel(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Vector2ViewModel(Vector2 twinVec) : this(twinVec.X, twinVec.Y)
        {
        }

        public override void Save(Object? o = null)
        {
            var v = (Vector2)o!;
            v.X = X;
            v.Y = Y;
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

        public override String ToString()
        {
            return $"({X}; {Y})";
        }
    }
}
