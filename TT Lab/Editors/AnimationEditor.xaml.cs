using System;
using System.Windows.Input;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors
{
    /// <summary>
    /// Interaction logic for AnimationEditor.xaml
    /// </summary>
    public partial class AnimationEditor : BaseEditor
    {
        public AnimationEditor()
        {
           InitializeComponent();
        }

        public AnimationEditor(AssetViewModel animation) : base(animation)
        {
            InitializeComponent();
        }

        private void BitfieldBox_PreviewTextInput(Object sender, TextCompositionEventArgs e)
        {
            e.Handled = !uint.TryParse(e.Text, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out uint _);
        }

        private void BitfieldBox_TextChanged(Object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
