using System;
using System.Windows;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Objects;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors.Graphics
{
    /// <summary>
    /// Interaction logic for SkinModelEditor.xaml
    /// </summary>
    public partial class SkinModelEditor : BaseEditor
    {

        private int selectedMaterial = 0;

        public SkinModelEditor()
        {
            InitializeComponent();
        }

        public SkinModelEditor(AssetViewModel asset) : base(asset)
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
                Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.Light);
            SceneRenderer.Scene.SetCameraSpeed(0.2f);

            var skinData = GetAssetData<SkinData>();
            Skin model = new(SceneRenderer.Scene, skinData);
            SceneRenderer.Scene.AddRender(model);
        }

        private void ResetMaterialViewer()
        {
            MaterialViewer.Scene?.Delete();

            MaterialViewer.Scene = new Rendering.Scene((float)MaterialViewer.Glcontrol.ActualWidth, (float)MaterialViewer.Glcontrol.ActualHeight,
                Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.TexturePass);
            MaterialViewer.Scene.SetCameraSpeed(0);
            MaterialViewer.Scene.DisableCameraManipulation();

            var skinData = GetAssetData<SkinData>();
            var matData = AssetManager.Get().GetAsset(skinData.SubSkins[selectedMaterial].Material).GetData<MaterialData>();
            MaterialName.Text = matData.Name;
            var texPlane = new Plane(MaterialViewer.Scene, matData);
            MaterialViewer.Scene.AddRender(texPlane);
        }

        private void PrevMatButton_Click(Object sender, RoutedEventArgs e)
        {
            selectedMaterial--;
            var skinData = GetAssetData<SkinData>();
            if (selectedMaterial < 0)
            {
                selectedMaterial = skinData.SubSkins.Count - 1;
            }
            ResetMaterialViewer();
        }

        private void NextMatButton_Click(Object sender, RoutedEventArgs e)
        {
            selectedMaterial++;
            var skinData = GetAssetData<SkinData>();
            if (selectedMaterial >= skinData.SubSkins.Count)
            {
                selectedMaterial = 0;
            }
            ResetMaterialViewer();
        }
    }
}
