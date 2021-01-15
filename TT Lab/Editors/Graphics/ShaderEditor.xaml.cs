using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using TT_Lab.AssetData.Graphics;
using TT_Lab.AssetData.Graphics.Shaders;
using TT_Lab.Command;
using TT_Lab.Project;
using TT_Lab.Rendering.Objects;
using TT_Lab.Util;
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
            TextureViewer.FileDrop += TextureViewer_FileDrop;
            TextureViewer.RendererInit += TextureViewer_RendererInit;
            TextureViewer.ContextMenu.Items.Add(new MenuItem
            {
                Header = "Clear",
                Command = new GenerateCommand(() =>
                {
                    CommandManager.Execute(new SetDataCommand<Guid>(shaderViewModel, "TexID", Guid.Empty));
                })
            });
            shaderViewModel.PropertyChanged += ShaderViewModel_PropertyChanged;
        }

        private void ShaderViewModel_PropertyChanged(Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TexID" || e.PropertyName == "TxtMapping")
            {
                ResetViewer();
            }
        }

        private void TextureViewer_RendererInit(Object sender, EventArgs e)
        {
            ResetViewer();
        }

        private void TextureViewer_FileDrop(Object sender, Controls.FileDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.File))
            {
                var data = e.Data.Data as ViewModels.AssetViewModel;
                if (data.Asset.Type.Name == "Texture")
                {
                    SetData("TexID", data.Asset.UUID, viewModel);
                }
            }
        }

        private void ResetViewer()
        {
            TextureViewer.Glcontrol.MakeCurrent();
            var texId = ((LabShaderViewModel)viewModel).TexID;
            var hasMapping = ((LabShaderViewModel)viewModel).TxtMapping;
            Bitmap bitmap;
            if (texId == Guid.Empty || !hasMapping)
            {
                bitmap = MiscUtils.GetBoatGuy();
            }
            else
            {
                var texData = (TextureData)ProjectManagerSingleton.PM.OpenedProject.GetAsset(texId).GetData();
                bitmap = texData.Bitmap;
            }
            TextureViewer.Scene = new Rendering.Scene((float)TextureViewer.GLHost.ActualWidth, (float)TextureViewer.GLHost.ActualHeight);
            TextureViewer.Scene.SetCameraSpeed(0);
            TextureViewer.Scene.DisableCameraManipulation();
            var texPlane = new Plane(bitmap);
            TextureViewer.Scene.AddRender(texPlane);
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
