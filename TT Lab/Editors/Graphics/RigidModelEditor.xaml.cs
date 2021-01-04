using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Project;
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

            MaterialViewer.RendererInit += MaterialViewer_RendererInit;
            SceneRenderer.RendererInit += SceneRenderer_RendererInit;
        }

        private void MaterialViewer_RendererInit(Object sender, EventArgs e)
        {
            ResetMaterialViewer();
        }

        private void SceneRenderer_RendererInit(Object sender, EventArgs e)
        {
            SceneRenderer.Glcontrol.MakeCurrent();
            SceneRenderer.Scene = new Rendering.Scene((float)SceneRenderer.GLHost.ActualWidth, (float)SceneRenderer.GLHost.ActualHeight,
                "LightTexture",
                (shd, s) =>
                {
                    s.DefaultShaderUniforms();
                },
                new Dictionary<uint, string>
                {
                    { 0, "in_Position" },
                    { 1, "in_Color" },
                    { 2, "in_Normal" },
                    { 3, "in_Texpos" }
                });
            SceneRenderer.Scene.SetCameraSpeed(0.2f);

            var rm = (RigidModelData)GetAssetData();
            RigidModel model = new RigidModel(rm);
            SceneRenderer.Scene.AddRender(model);
        }

        private void ResetMaterialViewer()
        {
            MaterialViewer.Glcontrol.MakeCurrent();
            MaterialViewer.Scene?.Delete();

            MaterialViewer.Scene = new Rendering.Scene((float)MaterialViewer.GLHost.ActualWidth, (float)MaterialViewer.GLHost.ActualHeight,
                "LightTexture",
                (shd, s) =>
                {
                    s.DefaultShaderUniforms();
                },
                new Dictionary<uint, string>
                {
                    { 0, "in_Position" },
                    { 1, "in_Color" },
                    { 2, "in_Normal" },
                    { 3, "in_Texpos" }
                });
            MaterialViewer.Scene.SetCameraSpeed(0);
            MaterialViewer.Scene.DisableCameraManipulation();

            var rm = (RigidModelData)GetAssetData();
            var matData = (MaterialData)ProjectManagerSingleton.PM.OpenedProject.GetAsset(rm.Materials[selectedMaterial]).GetData();
            MaterialName.Text = matData.Name;
            var texPlane = new Plane(matData);
            MaterialViewer.Scene.AddRender(texPlane);
        }

        private void PrevMatButton_Click(Object sender, RoutedEventArgs e)
        {
            selectedMaterial--;
            var rm = (RigidModelData)GetAssetData();
            if (selectedMaterial < 0)
            {
                selectedMaterial = rm.Materials.Count - 1;
            }
            ResetMaterialViewer();
        }

        private void NextMatButton_Click(Object sender, RoutedEventArgs e)
        {
            selectedMaterial++;
            var rm = (RigidModelData)GetAssetData();
            if (selectedMaterial >= rm.Materials.Count)
            {
                selectedMaterial = 0;
            }
            ResetMaterialViewer();
        }
    }
}
