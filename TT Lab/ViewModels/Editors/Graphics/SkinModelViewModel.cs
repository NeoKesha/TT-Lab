﻿using Caliburn.Micro;
using org.ogre;
using System;
using System.Linq;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Buffers;
using TT_Lab.Rendering.Objects;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Vector4 = org.ogre.Vector4;

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
                var sceneManager = glControl.GetSceneManager();
                var pivot = sceneManager.getRootSceneNode().createChildSceneNode();
                pivot.setPosition(0, 0, 0);
                glControl.SetCameraTarget(pivot);
                glControl.SetCameraStyle(CameraStyle.CS_ORBIT);

                var plane = sceneManager.getRootSceneNode().createChildSceneNode();
                var entity = sceneManager.createEntity(BufferGeneration.GetPlaneBuffer());
                var asset = AssetManager.Get().GetAsset(EditableResource);
                var skinData = asset.GetData<SkinData>();
                var matData = AssetManager.Get().GetAssetData<MaterialData>(skinData.SubSkins[_selectedMaterial].Material);
                var material = TwinMaterialGenerator.GenerateMaterialFromTwinMaterial(matData);
                MaterialName = matData.Name;
                entity.setMaterial(material.Item2);
                entity.getSubEntity(0).setCustomParameter(0, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
                plane.attachObject(entity);
                plane.scale(0.05f, 0.05f, 1f);
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
