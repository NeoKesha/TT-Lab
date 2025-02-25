using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Project;
using TT_Lab.Project.Messages;
using TT_Lab.ViewModels.Composite;

namespace TT_Lab.ViewModels
{
    public class ResourcesEditorsViewModel : EditorsViewerViewModel, IHandle<CreateEditorMessage<ResourceEditorViewModel>>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ProjectManager _projectManager;

        public ResourcesEditorsViewModel(IEventAggregator eventAggregator, ProjectManager projectManager)
        {
            DisplayName = "Resources Editors";
            _projectManager = projectManager;
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnUIThread(this);
        }

        public Task HandleAsync(CreateEditorMessage<ResourceEditorViewModel> message, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                if (ActiveEditors.TryGetValue(message.ResourceURI, out TabbedEditorViewModel? tabViewModel))
                {
                    ActivateItemAsync(tabViewModel, cancellationToken);
                    return;
                }

                var assetManager = _projectManager.OpenedProject!.AssetManager;
                var newEditor = new TabbedEditorViewModel(message.ResourceURI, message.EditorType)
                {
                    DisplayName = assetManager.GetAsset(message.ResourceURI).Name
                };

                ActiveEditors[message.ResourceURI] = newEditor;

                ActivateItemAsync(newEditor, cancellationToken);
            },
            cancellationToken);
        }
    }
}
