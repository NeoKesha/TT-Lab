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
using TT_Lab.Rendering.Objects;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors.Graphics
{
    /// <summary>
    /// Interaction logic for RigidModelEditor.xaml
    /// </summary>
    public partial class RigidModelEditor : BaseEditor
    {
        public RigidModelEditor()
        {
            InitializeComponent();
        }

        public RigidModelEditor(AssetViewModel asset) : base(asset)
        {
            InitializeComponent();
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
            var rm = (RigidModelData)asset.Asset.GetData();
            RigidModel model = new RigidModel(rm);
            SceneRenderer.Scene.AddRender(model);
        }
    }
}
