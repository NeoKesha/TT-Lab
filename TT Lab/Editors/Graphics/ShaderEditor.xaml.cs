using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Controls;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.Rendering.Objects;
using TT_Lab.Util;
using TT_Lab.ViewModels.Graphics;

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
            DataContext = new MaterialShaderViewModel(shaderViewModel, materialEditor);
            InitValidators();
            TextureViewer.FileDrop += TextureViewer_FileDrop;
            TextureViewer_RendererInit();
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

        public override void CloseEditor(Object? sender, EventArgs e)
        {
            TextureViewer.CloseEditor();

            base.CloseEditor(sender, e);
        }

        private void ShaderViewModel_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TexID" || e.PropertyName == "TxtMapping")
            {
                ResetViewer();
            }
        }

        private void TextureViewer_RendererInit()
        {
            ResetViewer();
        }

        private void TextureViewer_FileDrop(Object? sender, Controls.FileDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.File))
            {
                var data = e.Data.Data as ViewModels.AssetViewModel;
                if (data.Asset.Type.Name == "Texture")
                {
                    SetData("TexID", data.Asset.URI, viewModel);
                }
            }
        }

        private void ResetViewer()
        {
            var texId = ((LabShaderViewModel)viewModel).TexID;
            var hasMapping = ((LabShaderViewModel)viewModel).TxtMapping;
            Bitmap bitmap;
            if (texId == LabURI.Empty || !hasMapping)
            {
                bitmap = MiscUtils.GetBoatGuy();
            }
            else
            {
                var texData = AssetManager.Get().GetAsset(texId).GetData<TextureData>();
                bitmap = texData.Bitmap;
            }
            TextureViewer.Scene = new Rendering.Scene((float)TextureViewer.Glcontrol.ActualWidth, (float)TextureViewer.Glcontrol.ActualHeight,
                Rendering.Shaders.ShaderStorage.LibraryFragmentShaders.TexturePass);
            TextureViewer.Scene.SetCameraSpeed(0);
            TextureViewer.Scene.DisableCameraManipulation();
            var texPlane = new Plane(TextureViewer.Scene, bitmap);
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
