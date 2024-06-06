using Caliburn.Micro;

namespace TT_Lab.ViewModels.Interfaces
{
    public interface ISaveableViewModel<T> : INotifyPropertyChangedEx where T : class?
    {
        void Save(T o);
    }
}
