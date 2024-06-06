using Caliburn.Micro;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Composite
{
    public class BoundingBoxBuilderViewModel : Conductor<Vector4ViewModel>.Collection.OneActive, ISaveableViewModel<TwinBoundingBoxBuilder>
    {
        private BindableCollection<Vector4ViewModel> bbPoints;

        public BoundingBoxBuilderViewModel()
        {
            bbPoints = new BindableCollection<Vector4ViewModel>
            {
                new(0, 0, 0, 1),
                new(0, 0, 1, 1),
                new(0, 1, 0, 1),
                new(0, 1, 1, 1),
                new(1, 0, 0, 1),
                new(1, 0, 1, 1),
                new(1, 1, 0, 1),
                new(1, 1, 1, 1),
            };
        }

        public BoundingBoxBuilderViewModel(TwinBoundingBoxBuilder bb)
        {
            bbPoints = new BindableCollection<Vector4ViewModel>();
            foreach (var p in bb.BoundingBoxPoints)
            {
                bbPoints.Add(new Vector4ViewModel(p));
            }
        }

        public void Save(TwinBoundingBoxBuilder o)
        {
            var builder = o;
            builder.BoundingBoxPoints.Clear();
            foreach (var v in BoundingBoxPoints)
            {
                builder.BoundingBoxPoints.Add(new Vector4
                {
                    X = v.X,
                    Y = v.Y,
                    Z = v.Z,
                    W = v.W,
                });
            }
        }

        public BindableCollection<Vector4ViewModel> BoundingBoxPoints
        {
            get => bbPoints;
        }
    }
}
