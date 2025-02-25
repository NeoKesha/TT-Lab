using Caliburn.Micro;
using org.ogre;
using System;
using System.Linq;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Objects;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Editors.Graphics
{
    public class SkinModelViewModel : ResourceEditorViewModel
    {
        private Int32 _selectedMaterial;
        private String _materialName;
        private ModelBuffer? _skin;

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
            SceneRenderer.SceneHeaderModel = "Skin viewer";
            MaterialViewer.SceneHeaderModel = "Material viewer";

            InitMaterialViewer();
            InitSceneRenderer();
        }

        private void InitSceneRenderer()
        {
            SceneRenderer.SceneCreator = glControl =>
            {
                var sceneManager = glControl.GetSceneManager();
                var pivot = sceneManager.getRootSceneNode().createChildSceneNode();
                pivot.setPosition(0, 0, 0);
                glControl.SetCameraTarget(pivot);

                var skin = AssetManager.Get().GetAssetData<SkinData>(EditableResource);
                _skin = new ModelBuffer(sceneManager, EditableResource, skin);
            };
        }

        private void InitMaterialViewer()
        {
            MaterialViewer.SceneCreator = glControl =>
            {
                // var sceneManager = glControl.GetSceneManager();
                // var pivot = sceneManager.getRootSceneNode().createChildSceneNode();
                // pivot.setPosition(0, 0, 0);
                // glControl.SetCameraTarget(pivot);
                // glControl.SetCameraStyle(org.ogre.CameraStyle.CS_ORBIT);
                //
                // var sphereNode = sceneManager.getRootSceneNode().createChildSceneNode();
                // var entity = sceneManager.createEntity(SceneManager.PT_SPHERE);
                // var rm = AssetManager.Get().GetAssetData<SkinData>(EditableResource);
                // var materialName = AssetManager.Get().GetAsset(rm.SubSkins[_selectedMaterial].Material).Name;
                // MaterialName = materialName;
                // var materialData = AssetManager.Get().GetAssetData<MaterialData>(rm.SubSkins[_selectedMaterial].Material);
                // var material = TwinMaterialGenerator.GenerateMaterialFromTwinMaterial(materialData);
                // entity.setMaterial(material.Item2);
                // sphereNode.attachObject(entity);
                // sphereNode.scale(0.1f, 0.1f, 0.1f);
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
            MaterialViewer.ResetScene();
        }

        public void NextMatButton()
        {
            _selectedMaterial++;
            var rm = AssetManager.Get().GetAssetData<SkinData>(EditableResource);
            if (_selectedMaterial >= rm.SubSkins.Count)
            {
                _selectedMaterial = 0;
            }
            MaterialViewer.ResetScene();
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
