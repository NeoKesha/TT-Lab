using System.Linq;
using Caliburn.Micro;
using TT_Lab.Util;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Composite
{
    public class BoundingBoxBuilderViewModel : Conductor<Vector4ViewModel>.Collection.OneActive, ISaveableViewModel<TwinBoundingBoxBuilder>, IHaveChildrenEditors
    {
        private BindableCollection<Vector4ViewModel> bbPoints;
        private readonly DirtyTracker dirtyTracker;

        public BoundingBoxBuilderViewModel()
        {
            dirtyTracker = new DirtyTracker(this);
            bbPoints = new BindableCollection<Vector4ViewModel>();
            dirtyTracker.AddBindableCollection(bbPoints);
            
            bbPoints.AddRange(new Vector4ViewModel[]
            {
                new(0, 0, 0, 1),
                new(0, 0, 1, 1),
                new(0, 1, 0, 1),
                new(0, 1, 1, 1),
                new(1, 0, 0, 1),
                new(1, 0, 1, 1),
                new(1, 1, 0, 1),
                new(1, 1, 1, 1),
            });
        }

        public BoundingBoxBuilderViewModel(TwinBoundingBoxBuilder bb)
        {
            dirtyTracker = new DirtyTracker(this);
            bbPoints = new BindableCollection<Vector4ViewModel>();
            dirtyTracker.AddBindableCollection(bbPoints);
            foreach (var p in bb.BoundingBoxPoints)
            {
                bbPoints.Add(new Vector4ViewModel(p));
            }
        }

        public void ResetDirty()
        {
            dirtyTracker.ResetDirty();
        }

        public bool IsDirty => dirtyTracker.IsDirty;

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
            
            dirtyTracker.ResetDirty();
        }

        public BindableCollection<Vector4ViewModel> BoundingBoxPoints => bbPoints;

        public DirtyTracker DirtyTracker => dirtyTracker;
    }
}
