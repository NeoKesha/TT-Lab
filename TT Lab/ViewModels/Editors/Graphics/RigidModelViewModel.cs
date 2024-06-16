using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Project.Messages;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Objects;

namespace TT_Lab.ViewModels.Editors.Graphics
{
    public class RigidModelViewModel : ResourceEditorViewModel
    {
        private Int32 _selectedMaterial;
        private String _materialName;
        private SceneEditorViewModel _sceneRenderer;
        private SceneEditorViewModel _materialViewer;

        public RigidModelViewModel()
        {
            _sceneRenderer = IoC.Get<SceneEditorViewModel>();
            _materialViewer = IoC.Get<SceneEditorViewModel>();
            _materialName = "NO MATERIAL";

            InitMaterialViewer();
            InitSceneRenderer();
        }

        private void InitSceneRenderer()
        {
            SceneRenderer.SceneCreator = (GLWindow glControl) =>
            {
                glControl.SetRendererLibraries(Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.TexturePass);

                var scene = new Scene(glControl.RenderContext, glControl, (float)glControl.RenderControl.Width, (float)glControl.RenderControl.Height);
                scene.SetCameraSpeed(0.2f);

                var rm = AssetManager.Get().GetAssetData<RigidModelData>(EditableResource);
                RigidModel model = new(glControl.RenderContext, glControl, scene, rm);
                scene.AddChild(model);

                return scene;
            };
        }

        private void InitMaterialViewer()
        {
            MaterialViewer.SceneCreator = (GLWindow glControl) =>
            {
                glControl.SetRendererLibraries(Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.TexturePass);

                var scene = new Scene(glControl.RenderContext, glControl, (float)glControl.RenderControl.Width, (float)glControl.RenderControl.Height);
                scene.SetCameraSpeed(0);
                scene.DisableCameraManipulation();

                var rm = AssetManager.Get().GetAssetData<RigidModelData>(EditableResource);
                var matData = AssetManager.Get().GetAsset(rm.Materials[_selectedMaterial]).GetData<MaterialData>();
                MaterialName = matData.Name;
                var texPlane = new Plane(glControl.RenderContext, glControl, scene, matData);
                scene.AddChild(texPlane);

                return scene;
            };
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
