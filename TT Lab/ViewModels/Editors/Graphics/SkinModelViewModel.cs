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
    public class SkinModelViewModel : ResourceEditorViewModel, IHandle<RendererInitializedMessage>
    {
        private Int32 _selectedMaterial;
        private String _materialName;
        private readonly IEventAggregator _eventAggregator;

        private enum SceneIndex : int
        {
            Skin,
            Material
        }

        public SkinModelViewModel(IEventAggregator eventAggregator)
        {
            Scenes.Add(IoC.Get<SceneEditorViewModel>());
            Scenes.Add(IoC.Get<SceneEditorViewModel>());
            _materialName = "NO MATERIAL";
            _eventAggregator = eventAggregator;
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _eventAggregator.SubscribeOnUIThread(this);

            return base.OnActivateAsync(cancellationToken);
        }

        protected override Task OnDeactivateAsync(Boolean close, CancellationToken cancellationToken)
        {
            _eventAggregator.Unsubscribe(this);

            return base.OnDeactivateAsync(close, cancellationToken);
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

            SceneRenderer.GlControl.Context.MakeCurrent();
            var rm = AssetManager.Get().GetAssetData<SkinData>(EditableResource);
            Skin model = new(SceneRenderer.Scene, rm);
            SceneRenderer.Scene.AddRender(model);
            SceneRenderer.GlControl.Context.MakeNoneCurrent();
        }

        private void InitMaterialViewer()
        {
            MaterialViewer.Scene?.Delete();

            MaterialViewer.Scene = new Rendering.Scene(MaterialViewer.GlControl.Context, (float)MaterialViewer.GlControl.ActualWidth, (float)MaterialViewer.GlControl.ActualHeight,
                Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.TexturePass);
            MaterialViewer.Scene.SetCameraSpeed(0);
            MaterialViewer.Scene.DisableCameraManipulation();

            MaterialViewer.GlControl.Context.MakeCurrent();
            var rm = AssetManager.Get().GetAssetData<SkinData>(EditableResource);
            var matData = AssetManager.Get().GetAsset(rm.SubSkins[_selectedMaterial].Material).GetData<MaterialData>();
            MaterialName = matData.Name;
            var texPlane = new Plane(MaterialViewer.Scene, matData);
            MaterialViewer.Scene.AddRender(texPlane);
            MaterialViewer.GlControl.Context.MakeNoneCurrent();
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
            var rm = AssetManager.Get().GetAssetData<SkinData>(EditableResource);
            if (_selectedMaterial < 0)
            {
                _selectedMaterial = rm.SubSkins.Count - 1;
            }
            InitMaterialViewer();
        }

        public void NextMatButton()
        {
            _selectedMaterial++;
            var rm = AssetManager.Get().GetAssetData<SkinData>(EditableResource);
            if (_selectedMaterial >= rm.SubSkins.Count)
            {
                _selectedMaterial = 0;
            }
            InitMaterialViewer();
        }

        public SceneEditorViewModel SceneRenderer
        {
            get => Scenes[(int)SceneIndex.Skin];
        }

        public SceneEditorViewModel MaterialViewer
        {
            get => Scenes[(int)SceneIndex.Material];
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
