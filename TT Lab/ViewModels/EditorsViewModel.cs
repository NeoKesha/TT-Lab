using System;
using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;

namespace TT_Lab.ViewModels
{
    public class EditorsViewModel : Conductor<EditorsViewerViewModel>.Collection.OneActive
    {
        public override async Task<Boolean> CanCloseAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = true;
            foreach (var item in Items)
            {
                result = await item.CanCloseAsync(cancellationToken);
                if (!result)
                {
                    break;
                }
            }
            
            return result;
        }

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
