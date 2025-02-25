using Caliburn.Micro;
using System.Threading.Tasks;
using TT_Lab.Models;

namespace TT_Lab.ViewModels
{
    public class AboutViewModel : Screen
    {
        private AboutModel _aboutData = new();

        public AboutModel AboutData
        {
            get => _aboutData;
        }

        public Task CloseForm()
        {
            return TryCloseAsync();
        }
    }
}
