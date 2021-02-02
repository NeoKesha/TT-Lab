using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TT_Lab.ViewModels
{
    public abstract class SavebleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyChange([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
