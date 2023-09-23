namespace TT_Lab.ViewModels
{
    public abstract class SaveableViewModel : ObservableObject
    {
        public abstract void Save(object? o = null);
    }
}
