using System.Windows;
using System.Windows.Controls;


namespace TT_Lab.Views.Editors
{
    /// <summary>
    /// Interaction logic for ChunkEditor.xaml
    /// </summary>
    public partial class ChunkEditorView : UserControl
    {
        public ChunkEditorView()
        {
            InitializeComponent();
        }

        private void UIElement_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox is { IsVisible: true })
            {
                textBox.Focus();
            }
        }
    }
}
