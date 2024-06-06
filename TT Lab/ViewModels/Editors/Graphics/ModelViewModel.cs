using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.Project.Messages;

namespace TT_Lab.ViewModels.Editors.Graphics
{
    public class ModelViewModel : ResourceEditorViewModel, IHandle<RendererInitializedMessage>
    {
        private SceneEditorViewModel _sceneRenderer;

        public ModelViewModel(IEventAggregator eventAggregator)
        {
            _sceneRenderer = IoC.Get<SceneEditorViewModel>();
            eventAggregator.SubscribeOnUIThread(this);
        }

        public Task HandleAsync(RendererInitializedMessage message, CancellationToken cancellationToken)
        {
            SceneRenderer.Scene = new Rendering.Scene(SceneRenderer.GlControl.Context, (float)SceneRenderer.GlControl.ActualWidth, (float)SceneRenderer.GlControl.ActualHeight, Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.Light);
            SceneRenderer.Scene.SetCameraSpeed(0.2f);
            Rendering.Objects.Model m = new(SceneRenderer.Scene, AssetManager.Get().GetAssetData<AssetData.Graphics.ModelData>(EditableResource));
            SceneRenderer.Scene.AddRender(m);

            return Task.FromResult(true);
        }

        public SceneEditorViewModel SceneRenderer
        {
            get => _sceneRenderer;
        }

        public override void LoadData()
        {
            return;
        }

        protected override void Save()
        {
            return;
        }
    }
}
