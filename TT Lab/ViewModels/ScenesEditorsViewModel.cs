using Caliburn.Micro;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Project;
using TT_Lab.Project.Messages;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Editors;

namespace TT_Lab.ViewModels
{
    public class ScenesEditorsViewModel : EditorsViewerViewModel, IHandle<CreateEditorMessage<ChunkEditorViewModel>>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ProjectManager _projectManager;

        public ScenesEditorsViewModel(IEventAggregator eventAggregator, ProjectManager projectManager)
        {
            DisplayName = "Scenes Editors";
            _projectManager = projectManager;
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnUIThread(this);
        }

        public Task HandleAsync(CreateEditorMessage<ChunkEditorViewModel> message, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                if (_activeEditors.TryGetValue(message.ResourceURI, out TabbedEditorViewModel? tabViewModel))
                {
                    ActivateItemAsync(tabViewModel);
                    return;
                }

                var assetManager = _projectManager.OpenedProject!.AssetManager;
                var newEditor = new TabbedEditorViewModel(message.ResourceURI, message.EditorType)
                {
                    DisplayName = assetManager.GetAsset(message.ResourceURI).Name
                };

                _activeEditors[message.ResourceURI] = newEditor;

                ActivateItemAsync(newEditor);
            },
            cancellationToken);
        }
    }
}
