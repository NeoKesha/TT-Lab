﻿using Caliburn.Micro;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Composite
{
    public class Vector3ViewModel : Screen, ISaveableViewModel<Vector3>
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

        public void Save(Vector3 o)
        {
            o.X = X;
            o.Y = Y;
            o.Z = Z;
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
    }
}
