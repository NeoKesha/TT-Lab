﻿using Caliburn.Micro;
using System;
using System.Linq;
using org.ogre;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering;
using TT_Lab.Rendering.Objects;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Vector4 = org.ogre.Vector4;

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

            SceneRenderer.SceneHeaderModel = "Blend skin viewer";
            MaterialViewer.SceneHeaderModel = "Material viewer";

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
            MaterialViewer.ResetScene();
        }

        public void NextMatButton()
        {
            _selectedMaterial++;
            var blendSkinData = AssetManager.Get().GetAssetData<BlendSkinData>(EditableResource);
            if (_selectedMaterial >= blendSkinData.Blends.Count)
            {
                _selectedMaterial = 0;
            }
            MaterialViewer.ResetScene();
        }

        private void InitSceneRenderer()
        {
            SceneRenderer.SceneCreator = glControl =>
            {
                var sceneManager = glControl.GetSceneManager();
                var pivot = sceneManager.getRootSceneNode().createChildSceneNode();
                pivot.setPosition(0, 0, 0);
                glControl.SetCameraTarget(pivot);
                glControl.EnableImgui(true);
                
                var blendSkinData = AssetManager.Get().GetAssetData<BlendSkinData>(EditableResource);
                blendSkin = new BlendSkin(sceneManager, EditableResource, blendSkinData);

                glControl.OnRender += (sender, args) =>
                {
                    ImGui.Begin("Blend Skin");
                    ImGui.SetWindowPos(new ImVec2(5, 5));
                    ImGui.Text($"Vertexes: {blendSkinData.Blends.Sum(b => b.Models.Sum(sb => sb.Vertexes.Count))}");
                    ImGui.Text($"Faces: {blendSkinData.Blends.Sum(b => b.Models.Sum(sb => sb.Faces.Count))}");
                    ImGui.Text($"Morphs: {blendSkinData.BlendsAmount}");
                    ImGui.End();
                };
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
                var blendData = asset.GetData<BlendSkinData>();
                var matData = AssetManager.Get().GetAssetData<MaterialData>(blendData.Blends[_selectedMaterial].Material);
                MaterialName = matData.Name;
                var material = TwinMaterialGenerator.GenerateMaterialFromTwinMaterial(matData);
                entity.setMaterial(material.Item2);
                entity.getSubEntity(0).setCustomParameter(0, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
                plane.attachObject(entity);
                plane.scale(0.05f, 0.05f, 1f);
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
