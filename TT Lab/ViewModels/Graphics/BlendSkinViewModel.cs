using System;

namespace TT_Lab.ViewModels.Graphics
{
    public class BlendSkinViewModel : ObservableObject
    {
        private Single[] shapeWeights;

        public BlendSkinViewModel()
        {
            shapeWeights = new Single[15];
        }

        public Single Weight1
        {
            get => shapeWeights[0];
            set
            {
                shapeWeights[0] = value;
                NotifyChange();
            }
        }

        public Single Weight2
        {
            get => shapeWeights[1];
            set
            {
                shapeWeights[1] = value;
                NotifyChange();
            }
        }

        public Single Weight3
        {
            get => shapeWeights[2];
            set
            {
                shapeWeights[2] = value;
                NotifyChange();
            }
        }

        public Single Weight4
        {
            get => shapeWeights[3];
            set
            {
                shapeWeights[3] = value;
                NotifyChange();
            }
        }

        public Single Weight5
        {
            get => shapeWeights[4];
            set
            {
                shapeWeights[4] = value;
                NotifyChange();
            }
        }

        public Single Weight6
        {
            get => shapeWeights[5];
            set
            {
                shapeWeights[5] = value;
                NotifyChange();
            }
        }

        public Single Weight7
        {
            get => shapeWeights[6];
            set
            {
                shapeWeights[6] = value;
                NotifyChange();
            }
        }

        public Single Weight8
        {
            get => shapeWeights[7];
            set
            {
                shapeWeights[7] = value;
                NotifyChange();
            }
        }

        public Single Weight9
        {
            get => shapeWeights[8];
            set
            {
                shapeWeights[8] = value;
                NotifyChange();
            }
        }

        public Single Weight10
        {
            get => shapeWeights[9];
            set
            {
                shapeWeights[9] = value;
                NotifyChange();
            }
        }

        public Single Weight11
        {
            get => shapeWeights[10];
            set
            {
                shapeWeights[10] = value;
                NotifyChange();
            }
        }

        public Single Weight12
        {
            get => shapeWeights[11];
            set
            {
                shapeWeights[11] = value;
                NotifyChange();
            }
        }

        public Single Weight13
        {
            get => shapeWeights[12];
            set
            {
                shapeWeights[12] = value;
                NotifyChange();
            }
        }

        public Single Weight14
        {
            get => shapeWeights[13];
            set
            {
                shapeWeights[13] = value;
                NotifyChange();
            }
        }

        public Single Weight15
        {
            get => shapeWeights[14];
            set
            {
                shapeWeights[14] = value;
                NotifyChange();
            }
        }
    }
}
