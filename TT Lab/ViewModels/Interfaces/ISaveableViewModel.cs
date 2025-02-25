using Caliburn.Micro;

namespace TT_Lab.ViewModels.Interfaces
{
    public interface ISaveableViewModel<in T> : IDirtyMarker where T : class?
    {
        void Save(T o);
    }
}
