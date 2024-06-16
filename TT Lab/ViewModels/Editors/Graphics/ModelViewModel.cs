using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.Project.Messages;
using TT_Lab.Rendering;

namespace TT_Lab.ViewModels.Editors.Graphics
{
    public class ModelViewModel : ResourceEditorViewModel
    {
        public ModelViewModel()
        {
            Scenes.Add(IoC.Get<SceneEditorViewModel>());

            InitSceneRenderer();
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            InitSceneRenderer();

            return base.OnActivateAsync(cancellationToken);
        }

        private void InitSceneRenderer()
        {
            SceneRenderer.SceneCreator = (GLWindow glControl) =>
            {
                glControl.SetRendererLibraries(Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.Light);

                var scene = new Scene(glControl.RenderContext, glControl, (float)glControl.RenderControl.Width, (float)glControl.RenderControl.Height);
                scene.SetCameraSpeed(0.2f);
                Rendering.Objects.Model m = new(glControl.RenderContext, glControl, scene, AssetManager.Get().GetAssetData<AssetData.Graphics.ModelData>(EditableResource));
                scene.AddChild(m);

                return scene;
            };
        }

        public SceneEditorViewModel SceneRenderer
        {
            get => Scenes[0];
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
