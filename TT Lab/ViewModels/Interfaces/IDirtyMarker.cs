using Caliburn.Micro;

namespace TT_Lab.ViewModels.Interfaces;

public interface IDirtyMarker : INotifyPropertyChangedEx
{
    void ResetDirty();
    
    bool IsDirty { get; }
}