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
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.ViewModels;
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
            MaterialViewer.RendererInit += MaterialViewer_RendererInit;
            matViewModel.PropertyChanged += MatViewModel_PropertyChanged;
        }

        private void MatViewModel_PropertyChanged(Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ResetViewer();
        }

        private void MaterialViewer_RendererInit(Object sender, EventArgs e)
        {
            ResetViewer();
        }

        void ResetViewer()
        {
            /*MaterialViewer.Glcontrol.MakeCurrent();
            MaterialViewer.Scene = new Rendering.Scene((float)MaterialViewer.GLHost.ActualWidth, (float)MaterialViewer.GLHost.ActualWidth,
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
                }
            );
            MaterialViewer.Scene.SetCameraSpeed(0);
            MaterialViewer.Scene.DisableCameraManipulation();*/
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
            viewModel.DeleteShaderCommand.Item = ShaderList.SelectedItem;
            viewModel.CloneShaderCommand.Item = ShaderList.SelectedItem;
            ShaderSettingsBox.Content = new ShaderEditor((LabShaderViewModel)ShaderList.SelectedItem, this);
        }
    }
}
