using Caliburn.Micro;
using System;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Composite
{
    public class Vector2ViewModel : Screen, ISaveableViewModel<Vector2>
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

        public void Save(Vector2 o)
        {
            o.X = X;
            o.Y = Y;
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

        public override String ToString()
        {
            return $"({X}; {Y})";
        }
    }
}
