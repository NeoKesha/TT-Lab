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
        private MaterialViewModel MaterialView { get => (MaterialViewModel)viewModel; }

        public MaterialEditor()
        {
            InitializeComponent();
        }

        public MaterialEditor(AssetViewModel matViewModel) : base(matViewModel)
        {
            InitializeComponent();
        }

        private void MaterialHeaderBox_TextChanged(Object sender, EventArgs e)
        {
            var tb = (BaseTextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                SetData("Header", 0UL);
                return;
            }
            if (!UInt64.TryParse(tb.Text, out UInt64 result) || MaterialView.Header == result) return;
            SetData("Header", result);
        }

        private void MaterialNameBox_TextChanged(Object sender, EventArgs e)
        {
            var tb = (BaseTextBox)sender;
            if (tb.Text.Length > UInt16.MaxValue || MaterialView.Name == tb.Text) return;

            SetData("Name", tb.Text);
        }

        private void MaterialDmaIndexBox_TextChanged(Object sender, EventArgs e)
        {
            var tb = (BaseTextBox)sender;
            if (string.IsNullOrEmpty(tb.Text))
            {
                SetData("DmaChainIndex", 0U);
                return;
            }
            if (!UInt32.TryParse(tb.Text, out UInt32 result) || MaterialView.DmaChainIndex == result) return;
            if (result > 27) return;
            SetData("DmaChainIndex", result);
        }

        private void ShaderList_SelectedItemChanged(Object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            if (ShaderSettingsBox.Content != null)
            {
                ShaderSettingsBox.Content = null;
            }
            ShaderSettingsBox.Content = new ShaderEditor((LabShaderViewModel)ShaderList.SelectedItem, this);
        }
    }
}
