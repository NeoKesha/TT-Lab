using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Project.Messages;
using TT_Lab.Rendering.Objects;

namespace TT_Lab.ViewModels.Editors.Graphics
{
    public class RigidModelViewModel : ResourceEditorViewModel, IHandle<RendererInitializedMessage>
    {
        private Int32 _selectedMaterial;
        private String _materialName;
        private SceneEditorViewModel _sceneRenderer;
        private SceneEditorViewModel _materialViewer;

        public RigidModelViewModel(IEventAggregator eventAggregator)
        {
            _sceneRenderer = IoC.Get<SceneEditorViewModel>();
            _materialViewer = IoC.Get<SceneEditorViewModel>();
            _materialName = "NO MATERIAL";

            eventAggregator.SubscribeOnUIThread(this);
        }

        public Task HandleAsync(RendererInitializedMessage message, CancellationToken cancellationToken)
        {
            if (message.Renderer == SceneRenderer)
            {
                InitSceneRenderer();
            }
            else if (message.Renderer == MaterialViewer)
            {
                InitMaterialViewer();
            }

            return Task.FromResult(true);
        }

        private void InitSceneRenderer()
        {
            SceneRenderer.Scene = new Rendering.Scene(SceneRenderer.GlControl.Context, (float)SceneRenderer.GlControl.ActualWidth, (float)SceneRenderer.GlControl.ActualHeight,
                Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.TexturePass);
            SceneRenderer.Scene.SetCameraSpeed(0.2f);

            var rm = AssetManager.Get().GetAssetData<RigidModelData>(EditableResource);
            RigidModel model = new(SceneRenderer.Scene, rm);
            SceneRenderer.Scene.AddRender(model);
        }

        private void InitMaterialViewer()
        {
            MaterialViewer.Scene?.Delete();

            MaterialViewer.Scene = new Rendering.Scene(MaterialViewer.GlControl.Context, (float)MaterialViewer.GlControl.ActualWidth, (float)MaterialViewer.GlControl.ActualHeight,
                Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.TexturePass);
            MaterialViewer.Scene.SetCameraSpeed(0);
            MaterialViewer.Scene.DisableCameraManipulation();

            var rm = AssetManager.Get().GetAssetData<RigidModelData>(EditableResource);
            var matData = AssetManager.Get().GetAsset(rm.Materials[_selectedMaterial]).GetData<MaterialData>();
            MaterialName = matData.Name;
            var texPlane = new Plane(MaterialViewer.Scene, matData);
            MaterialViewer.Scene.AddRender(texPlane);
        }

        public override void LoadData()
        {
            return;
        }

        protected override void Save()
        {
            return;
        }

        public void PrevMatButton()
        {
            _selectedMaterial--;
            var rm = AssetManager.Get().GetAssetData<RigidModelData>(EditableResource);
            if (_selectedMaterial < 0)
            {
                _selectedMaterial = rm.Materials.Count - 1;
            }
            InitMaterialViewer();
        }

        public void NextMatButton()
        {
            _selectedMaterial++;
            var rm = AssetManager.Get().GetAssetData<RigidModelData>(EditableResource);
            if (_selectedMaterial >= rm.Materials.Count)
            {
                _selectedMaterial = 0;
            }
            InitMaterialViewer();
        }

        public SceneEditorViewModel SceneRenderer
        {
            get => _sceneRenderer;
        }

        public SceneEditorViewModel MaterialViewer
        {
            get => _materialViewer;
        }

        public String MaterialName
        {
            get => _materialName;
            set
            {
                _materialName = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
