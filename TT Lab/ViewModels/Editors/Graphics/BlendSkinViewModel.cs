using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Project.Messages;
using TT_Lab.Rendering;

namespace TT_Lab.ViewModels.Editors.Graphics
{
    public class BlendSkinViewModel : ResourceEditorViewModel
    {
        private Single[] shapeWeights;
        private Rendering.Objects.BlendSkin blendSkin;
        private Int32 _selectedMaterial;
        private String _materialName;

        public BlendSkinViewModel()
        {
            shapeWeights = new Single[15];
            _materialName = "NO MATERIAL";
            Scenes.Add(IoC.Get<SceneEditorViewModel>());
            Scenes.Add(IoC.Get<SceneEditorViewModel>());

            InitMaterialViewer();
            InitSceneRenderer();
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
            var blendSkinData = AssetManager.Get().GetAssetData<BlendSkinData>(EditableResource);
            if (_selectedMaterial < 0)
            {
                _selectedMaterial = blendSkinData.Blends.Count - 1;
            }
            InitMaterialViewer();
        }

        public void NextMatButton()
        {
            _selectedMaterial++;
            var blendSkinData = AssetManager.Get().GetAssetData<BlendSkinData>(EditableResource);
            if (_selectedMaterial >= blendSkinData.Blends.Count)
            {
                _selectedMaterial = 0;
            }
            InitMaterialViewer();
        }

        private void InitSceneRenderer()
        {
            SceneRenderer.SceneCreator = (GLWindow glControl) =>
            {
                glControl.SetRendererLibraries(Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.Light);
                var scene = new Scene(glControl.RenderContext, glControl, (float)glControl.RenderControl.Width, (float)glControl.RenderControl.Height);
                scene.SetCameraSpeed(0.2f);

                var blendSkinData = AssetManager.Get().GetAssetData<BlendSkinData>(EditableResource);
                blendSkin = new(glControl.RenderContext, glControl, scene, blendSkinData);
                scene.AddChild(blendSkin);

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

                var blendSkinData = AssetManager.Get().GetAssetData<BlendSkinData>(EditableResource);
                var matData = AssetManager.Get().GetAsset(blendSkinData.Blends[_selectedMaterial].Material).GetData<MaterialData>();
                MaterialName = matData.Name;
                var texPlane = new Rendering.Objects.Plane(glControl.RenderContext, glControl, scene, matData);
                scene.AddChild(texPlane);

                return scene;
            };
        }

        private enum SceneIndex : int
        {
            BlendSkin,
            Material
        }

        public SceneEditorViewModel SceneRenderer
        {
            get => Scenes[(int)SceneIndex.BlendSkin];
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

        public Single Weight1
        {
            get => shapeWeights[0];
            set
            {
                shapeWeights[0] = value;
                blendSkin.SetBlendShapeValue(0, shapeWeights[0]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight2
        {
            get => shapeWeights[1];
            set
            {
                shapeWeights[1] = value;
                blendSkin.SetBlendShapeValue(1, shapeWeights[1]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight3
        {
            get => shapeWeights[2];
            set
            {
                shapeWeights[2] = value;
                blendSkin.SetBlendShapeValue(2, shapeWeights[2]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight4
        {
            get => shapeWeights[3];
            set
            {
                shapeWeights[3] = value;
                blendSkin.SetBlendShapeValue(3, shapeWeights[3]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight5
        {
            get => shapeWeights[4];
            set
            {
                shapeWeights[4] = value;
                blendSkin.SetBlendShapeValue(4, shapeWeights[4]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight6
        {
            get => shapeWeights[5];
            set
            {
                shapeWeights[5] = value;
                blendSkin.SetBlendShapeValue(5, shapeWeights[5]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight7
        {
            get => shapeWeights[6];
            set
            {
                shapeWeights[6] = value;
                blendSkin.SetBlendShapeValue(6, shapeWeights[6]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight8
        {
            get => shapeWeights[7];
            set
            {
                shapeWeights[7] = value;
                blendSkin.SetBlendShapeValue(7, shapeWeights[7]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight9
        {
            get => shapeWeights[8];
            set
            {
                shapeWeights[8] = value;
                blendSkin.SetBlendShapeValue(8, shapeWeights[8]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight10
        {
            get => shapeWeights[9];
            set
            {
                shapeWeights[9] = value;
                blendSkin.SetBlendShapeValue(9, shapeWeights[9]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight11
        {
            get => shapeWeights[10];
            set
            {
                shapeWeights[10] = value;
                blendSkin.SetBlendShapeValue(10, shapeWeights[10]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight12
        {
            get => shapeWeights[11];
            set
            {
                shapeWeights[11] = value;
                blendSkin.SetBlendShapeValue(11, shapeWeights[11]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight13
        {
            get => shapeWeights[12];
            set
            {
                shapeWeights[12] = value;
                blendSkin.SetBlendShapeValue(12, shapeWeights[12]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight14
        {
            get => shapeWeights[13];
            set
            {
                shapeWeights[13] = value;
                blendSkin.SetBlendShapeValue(13, shapeWeights[13]);
                NotifyOfPropertyChange();
            }
        }

        public Single Weight15
        {
            get => shapeWeights[14];
            set
            {
                shapeWeights[14] = value;
                blendSkin.SetBlendShapeValue(14, shapeWeights[14]);
                NotifyOfPropertyChange();
            }
        }
    }
}
