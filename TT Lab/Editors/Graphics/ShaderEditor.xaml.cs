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
using TT_Lab.AssetData.Graphics.Shaders;
using TT_Lab.ViewModels.Graphics;
using static Twinsanity.TwinsanityInterchange.Common.TwinShader;

namespace TT_Lab.Editors.Graphics
{
    /// <summary>
    /// Interaction logic for ShaderEditor.xaml
    /// </summary>
    public partial class ShaderEditor : UserControl
    {
        public ShaderEditor()
        {
            InitializeComponent();
        }

        public ShaderEditor(LabShaderViewModel shaderViewModel, MaterialEditor materialEditor) : this()
        {
            DataContext = new
            {
                ViewModel = shaderViewModel,
                MaterialEditor = materialEditor,
                ShaderTypes = new ObservableCollection<object>(Enum.GetValues(typeof(LabShader.Type)).Cast<object>()),
                AlphaTestMethods = new ObservableCollection<object>(Enum.GetValues(typeof(AlphaTestMethod)).Cast<object>()),
                ProcessAfterFailed = new ObservableCollection<object>(Enum.GetValues(typeof(ProcessAfterAlphaTestFailed)).Cast<object>()),
                DestinationAlphaTest = new ObservableCollection<object>(Enum.GetValues(typeof(DestinationAlphaTestMode)).Cast<object>()),
                DepthTestMethods = new ObservableCollection<object>(Enum.GetValues(typeof(DepthTestMethod)).Cast<object>()),
                ShadingMethods = new ObservableCollection<object>(Enum.GetValues(typeof(ShadingMethod)).Cast<object>()),
                TextureCoordinates = new ObservableCollection<object>(Enum.GetValues(typeof(TextureCoordinatesSpecification)).Cast<object>()),
                TextureFilters = new ObservableCollection<object>(Enum.GetValues(typeof(TextureFilter)).Cast<object>()),
                ZvalueDrawMasks = new ObservableCollection<object>(Enum.GetValues(typeof(ZValueDrawMask)).Cast<object>()),
                ColorSpecs = new ObservableCollection<object>(Enum.GetValues(typeof(ColorSpecMethod)).Cast<object>()),
                AlphaSpecs = new ObservableCollection<object>(Enum.GetValues(typeof(AlphaSpecMethod)).Cast<object>())
            };
        }
    }
}
