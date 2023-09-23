using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
using TT_Lab.Command;
using TT_Lab.Rendering.Objects;
using TT_Lab.Util;
using TT_Lab.ViewModels.Graphics;

namespace TT_Lab.Editors.Graphics
{
    /// <summary>
    /// Interaction logic for MaterialEditor.xaml
    /// </summary>
    public partial class MaterialEditor : BaseEditor
    {
        public MaterialEditor()
        {
            InitializeComponent();
        }

        public MaterialEditor(MaterialViewModel matViewModel) : base(matViewModel)
        {
            InitializeComponent();
            TreeContextMenu.Items.Add(new MenuItem
            {
                Header = "Add",
                Command = new RelayCommand(matViewModel.AddShaderCommand, CommandManager)
            });
            TreeContextMenu.Items.Add(new MenuItem
            {
                Header = "Duplicate",
                Command = new RelayCommand(matViewModel.CloneShaderCommand, CommandManager)
            });
            TreeContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(matViewModel.DeleteShaderCommand, CommandManager)
            });
            InitPredicates();
            MaterialViewer_RendererInit();
            matViewModel.PropertyChanged += MatViewModel_PropertyChanged;
            matViewModel.PropChanged += MatViewModel_PropertyChanged;
        }

        private void MatViewModel_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ResetViewer();
        }

        private void MaterialViewer_RendererInit()
        {
            ResetViewer();
        }

        void ResetViewer()
        {
            MaterialViewer.Scene?.Delete();
            MaterialViewer.Scene = new Rendering.Scene((float)MaterialViewer.Glcontrol.ActualWidth, (float)MaterialViewer.Glcontrol.ActualWidth,
                new Rendering.Shaders.ShaderProgram.LibShader { Type = OpenTK.Graphics.OpenGL.ShaderType.FragmentShader, Path = "Shaders\\TwinmaterialPass.frag" });
            MaterialViewer.Scene.SetCameraSpeed(0);
            MaterialViewer.Scene.DisableCameraManipulation();
            var viewModel = (MaterialViewModel)AssetViewModel;
            List<Bitmap> textures = new List<Bitmap>();
            for (var i = 0; i < viewModel.Shaders.Count; ++i)
            {
                var tex = viewModel.Shaders[i].TexID;
                if (tex == LabURI.Empty)
                {
                    textures.Add(MiscUtils.GetBoatGuy());
                }
                else
                {
                    var texData = (TextureData)AssetManager.Get().GetAsset<Texture>(tex).GetData();
                    textures.Add(texData.Bitmap);
                }
            }
            TwinMaterialPlane plane = new TwinMaterialPlane(MaterialViewer.Scene.Renderer.RenderProgram, textures.ToArray(), viewModel.Shaders.ToArray(), viewModel.Shaders.Count);
            MaterialViewer.Scene.AddRender(plane);
        }

        void InitPredicates()
        {
            AcceptNewPropValuePredicate["Header"] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (nStr == oStr) return null;
                if (string.IsNullOrEmpty(nStr))
                {
                    return 0UL;
                }
                if (!UInt64.TryParse(nStr, out UInt64 result)) return null;
                return result;
            };
            AcceptNewPropValuePredicate["Name"] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (nStr.Length > UInt16.MaxValue || nStr == oStr) return null;
                return nStr;
            };
            AcceptNewPropValuePredicate["DmaChainIndex"] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (nStr == oStr) return null;
                if (string.IsNullOrEmpty(nStr))
                {
                    return 0U;
                }
                if (!UInt32.TryParse(nStr, out UInt32 result)) return null;
                if (result > 27) return null;
                return result;
            };
        }

        private void ShaderList_SelectedItemChanged(Object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            if (ShaderSettingsBox.Content != null)
            {
                ShaderSettingsBox.Content = null;
            }
            var viewModel = (MaterialViewModel)AssetViewModel;
            viewModel.DeleteShaderCommand.Index = ShaderList.Items.IndexOf(ShaderList.SelectedItem);
            viewModel.CloneShaderCommand.Item = ShaderList.SelectedItem;
            ShaderSettingsBox.Content = new ShaderEditor((LabShaderViewModel)ShaderList.SelectedItem, this);
        }
    }
}
