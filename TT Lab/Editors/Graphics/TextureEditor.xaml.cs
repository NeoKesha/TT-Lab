using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TT_Lab.Assets.Graphics;
using TT_Lab.Rendering.Objects;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Editors.Graphics
{
    /// <summary>
    /// Interaction logic for TextureEditor.xaml
    /// </summary>
    public partial class TextureEditor : BaseEditor
    {
        public TextureEditor()
        {
            InitializeComponent();
        }

        public TextureEditor(AssetViewModel texture) : base(texture)
        {
            DataContext = new
            {
                ViewModel = viewModel,
                TexFuns = new ObservableCollection<object>(Enum.GetValues(typeof(PS2AnyTexture.TextureFunction)).Cast<object>()),
                PixelFormats = new ObservableCollection<object>(Enum.GetValues(typeof(PS2AnyTexture.TexturePixelFormat)).Cast<object>())
            };
            InitializeComponent();
            TextureViewer.RendererInit += TextureViewer_RendererInit;
        }

        private void TextureViewer_RendererInit(Object sender, EventArgs e)
        {
            TextureViewer.Glcontrol.MakeCurrent();
            TextureViewer.Scene = new Rendering.Scene((float)TextureViewer.GLHost.ActualWidth, (float)TextureViewer.GLHost.ActualHeight,
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
            TextureViewer.Scene.SetCameraSpeed(0);
            TextureViewer.Scene.DisableCameraManipulation();
            var texData = (TextureData)GetAssetData();
            var texPlane = new Plane(texData);
            TextureViewer.Scene.AddRender(texPlane);
        }
    }
}
