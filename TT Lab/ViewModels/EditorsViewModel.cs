using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;

namespace TT_Lab.ViewModels
{
    public class EditorsViewModel : Conductor<EditorsViewerViewModel>.Collection.OneActive
    {
        public void Save()
        {
            foreach (var item in Items)
            {
                item.Save();
            }
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ActivateItemAsync(IoC.Get<ScenesEditorsViewModel>(), cancellationToken);
            ActivateItemAsync(IoC.Get<ResourcesEditorsViewModel>(), cancellationToken);
            return base.OnInitializeAsync(cancellationToken);
        }
    }
}
