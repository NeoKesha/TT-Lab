using System;
using System.Windows;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Objects;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Graphics;

namespace TT_Lab.Editors.Graphics
{
    /// <summary>
    /// Interaction logic for BlendSkinModelEditor.xaml
    /// </summary>
    public partial class BlendSkinModelEditor : BaseEditor
    {

        private int selectedMaterial = 0;
        private BlendSkin blendSkin;

        public BlendSkinModelEditor()
        {
            InitializeComponent();
        }

        public BlendSkinModelEditor(AssetViewModel asset) : base(asset)
        {
            InitializeComponent();

            MaterialViewer_RendererInit();
            SceneRenderer_RendererInit();
        }

        public override void CloseEditor(Object? sender, EventArgs e)
        {
            SceneRenderer.CloseEditor();

            base.CloseEditor(sender, e);
        }

        private void MaterialViewer_RendererInit()
        {
            ResetMaterialViewer();
        }

        private void SceneRenderer_RendererInit()
        {
            SceneRenderer.Scene = new Rendering.Scene((float)SceneRenderer.Glcontrol.ActualWidth, (float)SceneRenderer.Glcontrol.ActualHeight,
                new Rendering.Shaders.ShaderProgram.LibShader { Type = OpenTK.Graphics.OpenGL.ShaderType.FragmentShader, Path = "Shaders\\TexturePass.frag" });
            SceneRenderer.Scene.SetCameraSpeed(0.2f);

            var blendSkinData = GetAssetData<BlendSkinData>();
            blendSkin = new(blendSkinData);
            SceneRenderer.Scene.AddRender(blendSkin, false);

            ((BlendSkinViewModel)MorphWeightsGrid.DataContext).PropertyChanged += BlendSkinModelEditor_PropertyChanged;
        }

        private void BlendSkinModelEditor_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == null) return;

            var context = (BlendSkinViewModel)MorphWeightsGrid.DataContext;
            switch(e.PropertyName)
            {
                case nameof(BlendSkinViewModel.Weight1):
                    blendSkin.BlendShapesValues[0] = context.Weight1;
                    break;
                case nameof(BlendSkinViewModel.Weight2):
                    blendSkin.BlendShapesValues[1] = context.Weight2;
                    break;
                case nameof(BlendSkinViewModel.Weight3):
                    blendSkin.BlendShapesValues[2] = context.Weight3;
                    break;
                case nameof(BlendSkinViewModel.Weight4):
                    blendSkin.BlendShapesValues[3] = context.Weight4;
                    break;
                case nameof(BlendSkinViewModel.Weight5):
                    blendSkin.BlendShapesValues[4] = context.Weight5;
                    break;
                case nameof(BlendSkinViewModel.Weight6):
                    blendSkin.BlendShapesValues[5] = context.Weight6;
                    break;
                case nameof(BlendSkinViewModel.Weight7):
                    blendSkin.BlendShapesValues[6] = context.Weight7;
                    break;
                case nameof(BlendSkinViewModel.Weight8):
                    blendSkin.BlendShapesValues[7] = context.Weight8;
                    break;
                case nameof(BlendSkinViewModel.Weight9):
                    blendSkin.BlendShapesValues[8] = context.Weight9;
                    break;
                case nameof(BlendSkinViewModel.Weight10):
                    blendSkin.BlendShapesValues[9] = context.Weight10;
                    break;
                case nameof(BlendSkinViewModel.Weight11):
                    blendSkin.BlendShapesValues[10] = context.Weight11;
                    break;
                case nameof(BlendSkinViewModel.Weight12):
                    blendSkin.BlendShapesValues[11] = context.Weight12;
                    break;
                case nameof(BlendSkinViewModel.Weight13):
                    blendSkin.BlendShapesValues[12] = context.Weight13;
                    break;
                case nameof(BlendSkinViewModel.Weight14):
                    blendSkin.BlendShapesValues[13] = context.Weight14;
                    break;
                case nameof(BlendSkinViewModel.Weight15):
                    blendSkin.BlendShapesValues[14] = context.Weight15;
                    break;
            };
        }

        private void ResetMaterialViewer()
        {
            MaterialViewer.Scene?.Delete();

            MaterialViewer.Scene = new Rendering.Scene((float)MaterialViewer.Glcontrol.ActualWidth, (float)MaterialViewer.Glcontrol.ActualHeight,
                new Rendering.Shaders.ShaderProgram.LibShader { Type = OpenTK.Graphics.OpenGL.ShaderType.FragmentShader, Path = "Shaders\\TexturePass.frag" });
            MaterialViewer.Scene.SetCameraSpeed(0);
            MaterialViewer.Scene.DisableCameraManipulation();

            var blendSkinData = GetAssetData<BlendSkinData>();
            var matData = AssetManager.Get().GetAsset(blendSkinData.Blends[selectedMaterial].Material).GetData<MaterialData>();
            MaterialName.Text = matData.Name;
            var texPlane = new Plane(matData);
            MaterialViewer.Scene.AddRender(texPlane);
        }

        private void PrevMatButton_Click(Object sender, RoutedEventArgs e)
        {
            selectedMaterial--;
            var blendSkinData = GetAssetData<BlendSkinData>();
            if (selectedMaterial < 0)
            {
                selectedMaterial = blendSkinData.Blends.Count - 1;
            }
            ResetMaterialViewer();
        }

        private void NextMatButton_Click(Object sender, RoutedEventArgs e)
        {
            selectedMaterial++;
            var blendSkinData = GetAssetData<BlendSkinData>();
            if (selectedMaterial >= blendSkinData.Blends.Count)
            {
                selectedMaterial = 0;
            }
            ResetMaterialViewer();
        }
    }
}
