namespace TT_Lab.ViewModels.Interfaces
{
    public interface IHaveParentEditor<T> where T : class
    {
        public T ParentEditor { get; set; }
    }
}
