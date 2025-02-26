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
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Vector4 = org.ogre.Vector4;

namespace TT_Lab.ViewModels.Editors.Graphics
{
    public class RigidModelViewModel : ResourceEditorViewModel
    {
        private Int32 _selectedMaterial;
        private String _materialName;
        private ModelBuffer? _rigidModel;

        private enum SceneIndex : int
        {
            Material,
            Model
        }

        public RigidModelViewModel()
        {
            Scenes.Add(IoC.Get<SceneEditorViewModel>());
            Scenes.Add(IoC.Get<SceneEditorViewModel>());
            _materialName = "NO MATERIAL";
            SceneRenderer.SceneHeaderModel = "Model viewer";
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
                glControl.EnableImgui(true);

                var assetManager = AssetManager.Get();
                var model = assetManager.GetAssetData<RigidModelData>(EditableResource);
                _rigidModel = new ModelBuffer(sceneManager, EditableResource, model);
                
                glControl.OnRender += (sender, args) =>
                {
                    ImGui.Begin("Rigid Model Data");
                    ImGui.SetWindowPos(new ImVec2(5, 5));
                    var linkedModelText = $"Links to model \"{assetManager.GetAsset(model.Model).Name}\"";
                    ImGui.Text(linkedModelText);
                    ImGui.Separator();
                    ImGui.BeginTable("Materials", 1);
                    ImGui.TableSetupColumn("Linked Materials");
                    ImGui.TableHeadersRow();
                    var maxNameLength = linkedModelText.Length;
                    foreach (var material in model.Materials)
                    {
                        ImGui.TableNextColumn();
                        var matAsset = assetManager.GetAsset(material);
                        ImGui.Text(matAsset.Name);
                        if (matAsset.Name.Length > maxNameLength)
                        {
                            maxNameLength = matAsset.Name.Length;
                        }
                        ImGui.TableNextRow();
                    }
                    ImGui.SetWindowSize(new ImVec2(8 * maxNameLength, 75 + model.Materials.Count * 20));
                    ImGui.EndTable();
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
                var rigidData = asset.GetData<RigidModelData>();
                var matData = AssetManager.Get().GetAssetData<MaterialData>(rigidData.Materials[_selectedMaterial]);
                MaterialName = matData.Name;
                var material = TwinMaterialGenerator.GenerateMaterialFromTwinMaterial(matData);
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
            var rm = AssetManager.Get().GetAssetData<RigidModelData>(EditableResource);
            if (_selectedMaterial < 0)
            {
                _selectedMaterial = rm.Materials.Count - 1;
            }
            MaterialViewer.ResetScene();
        }

        public void NextMatButton()
        {
            _selectedMaterial++;
            var rm = AssetManager.Get().GetAssetData<RigidModelData>(EditableResource);
            if (_selectedMaterial >= rm.Materials.Count)
            {
                _selectedMaterial = 0;
            }
            MaterialViewer.ResetScene();
        }

        public SceneEditorViewModel SceneRenderer => Scenes[(int)SceneIndex.Model];

        public SceneEditorViewModel MaterialViewer => Scenes[(int)SceneIndex.Material];

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
