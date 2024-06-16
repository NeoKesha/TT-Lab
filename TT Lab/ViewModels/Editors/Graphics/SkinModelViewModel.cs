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
    public class SkinModelViewModel : ResourceEditorViewModel
    {
        private Int32 _selectedMaterial;
        private String _materialName;

        private enum SceneIndex : int
        {
            Skin,
            Material
        }

        public SkinModelViewModel()
        {
            Scenes.Add(IoC.Get<SceneEditorViewModel>());
            Scenes.Add(IoC.Get<SceneEditorViewModel>());
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

                var rm = AssetManager.Get().GetAssetData<SkinData>(EditableResource);
                Skin model = new(glControl.RenderContext, glControl, scene, rm);
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

                var rm = AssetManager.Get().GetAssetData<SkinData>(EditableResource);
                var matData = AssetManager.Get().GetAsset(rm.SubSkins[_selectedMaterial].Material).GetData<MaterialData>();
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
