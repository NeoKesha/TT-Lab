using System;
using System.Windows.Input;
using TT_Lab.AssetData.Code;
using TT_Lab.Command;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors.Code
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
            if (!uint.TryParse(BitfieldBox.Text, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out uint test))
            {
                return;
            }
            var animData = (AnimationData)GetAssetData();
            if (test == animData.Bitfield) return;
            CommandManager.Execute(new SetDataCommand(animData, "Bitfield",
                uint.Parse(BitfieldBox.Text, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture)));
        }

        private void BitfieldBox_UndoPerformed(Object sender, EventArgs e)
        {
            CommandManager.Undo();
        }

        private void BitfieldBox_RedoPerformed(Object sender, EventArgs e)
        {
            CommandManager.Redo();
        }
    }
}
