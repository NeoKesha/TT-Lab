using TT_Lab.Util;

namespace TT_Lab.ViewModels.Interfaces;

public interface IHaveChildrenEditors : IDirtyMarker
{
    DirtyTracker DirtyTracker { get; }
}