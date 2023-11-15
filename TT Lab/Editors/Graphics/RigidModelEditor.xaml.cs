using System;
using System.Windows;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Rendering.Objects;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors.Graphics
{
    /// <summary>
    /// Interaction logic for RigidModelEditor.xaml
    /// </summary>
    public partial class RigidModelEditor : BaseEditor
    {

        private int selectedMaterial = 0;

        public RigidModelEditor()
        {
            InitializeComponent();
        }

        public RigidModelEditor(AssetViewModel asset) : base(asset)
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

            var rm = GetAssetData<RigidModelData>();
            RigidModel model = new RigidModel(rm);
            SceneRenderer.Scene.AddRender(model, false);
        }

        private void ResetMaterialViewer()
        {
            MaterialViewer.Scene?.Delete();

            MaterialViewer.Scene = new Rendering.Scene((float)MaterialViewer.Glcontrol.ActualWidth, (float)MaterialViewer.Glcontrol.ActualHeight,
                new Rendering.Shaders.ShaderProgram.LibShader { Type = OpenTK.Graphics.OpenGL.ShaderType.FragmentShader, Path = "Shaders\\TexturePass.frag" });
            MaterialViewer.Scene.SetCameraSpeed(0);
            MaterialViewer.Scene.DisableCameraManipulation();

            var rm = GetAssetData<RigidModelData>();
            var matData = AssetManager.Get().GetAsset(rm.Materials[selectedMaterial]).GetData<MaterialData>();
            MaterialName.Text = matData.Name;
            var texPlane = new Plane(matData);
            MaterialViewer.Scene.AddRender(texPlane);
        }

        private void PrevMatButton_Click(Object sender, RoutedEventArgs e)
        {
            selectedMaterial--;
            var rm = GetAssetData<RigidModelData>();
            if (selectedMaterial < 0)
            {
                selectedMaterial = rm.Materials.Count - 1;
            }
            ResetMaterialViewer();
        }

        private void NextMatButton_Click(Object sender, RoutedEventArgs e)
        {
            selectedMaterial++;
            var rm = GetAssetData<RigidModelData>();
            if (selectedMaterial >= rm.Materials.Count)
            {
                selectedMaterial = 0;
            }
            ResetMaterialViewer();
        }
    }
}
