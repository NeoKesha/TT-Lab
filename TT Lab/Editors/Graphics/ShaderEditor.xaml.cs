using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    public partial class ShaderEditor : BaseEditor
    {

        public ShaderEditor()
        {
            InitializeComponent();
        }

        public ShaderEditor(LabShaderViewModel shaderViewModel, MaterialEditor materialEditor) : base(shaderViewModel, materialEditor.CommandManager)
        {
            InitializeComponent();
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
            InitValidators();
        }

        private void InitValidators()
        {
            AcceptNewPropValuePredicate["IntParam"] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (oStr == nStr) return null;
                if (string.IsNullOrEmpty(nStr)) return 0U;
                if (!UInt32.TryParse(nStr, out UInt32 result)) return null;
                return result;
            };
            AcceptNewPropValuePredicate["AlphaRegSettingsIndex"] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (oStr == nStr) return null;
                if (string.IsNullOrEmpty(nStr)) return (Byte)0;
                if (!Byte.TryParse(nStr, out Byte result)) return null;
                if (result > 9) return null;
                return result;
            };
            AcceptNewPropValuePredicate["AlphaValueToCompareTo"] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (oStr == nStr) return null;
                if (string.IsNullOrEmpty(nStr)) return (Byte)0;
                if (!Byte.TryParse(nStr, out Byte result)) return null;
                return result;
            };
            AcceptNewPropValuePredicate["FixedAlphaValue"] = AcceptNewPropValuePredicate["AlphaValueToCompareTo"];
            AcceptNewPropValuePredicate["LodParamK"] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (oStr == nStr) return null;
                if (string.IsNullOrEmpty(nStr)) return (UInt16)0;
                if (!UInt16.TryParse(nStr, out UInt16 result)) return null;
                return result;
            };
            AcceptNewPropValuePredicate["LodParamL"] = AcceptNewPropValuePredicate["LodParamK"];
            AcceptNewPropValuePredicate["UnkVal1"] = AcceptNewPropValuePredicate["AlphaValueToCompareTo"];
            AcceptNewPropValuePredicate["UnkVal2"] = AcceptNewPropValuePredicate["AlphaValueToCompareTo"];
            AcceptNewPropValuePredicate["UnkVal3"] = AcceptNewPropValuePredicate["AlphaValueToCompareTo"];
            AcceptNewPropValuePredicate["X"] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (oStr == nStr) return null;
                if (string.IsNullOrEmpty(nStr)) return 0f;
                if (!Single.TryParse(nStr, NumberStyles.Float, CultureInfo.InvariantCulture, out Single result)) return null;
                return result;
            };
            AcceptNewPropValuePredicate["Y"] = AcceptNewPropValuePredicate["X"];
            AcceptNewPropValuePredicate["Z"] = AcceptNewPropValuePredicate["X"];
            AcceptNewPropValuePredicate["W"] = AcceptNewPropValuePredicate["X"];
        }
    }
}
