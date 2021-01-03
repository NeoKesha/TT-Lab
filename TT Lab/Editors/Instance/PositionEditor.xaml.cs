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
using TT_Lab.AssetData.Instance;
using TT_Lab.Command;
using TT_Lab.ViewModels;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Editors.Instance
{
    /// <summary>
    /// Interaction logic for PositionEditor.xaml
    /// </summary>
    public partial class PositionEditor : BaseEditor
    {
        public PositionEditor()
        {
            InitializeComponent();
        }

        public PositionEditor(AssetViewModel positionModel, Command.CommandManager commandManager) : base(positionModel, commandManager)
        {
            InitializeComponent();
        }

        private void Coord_PreviewTextInput(Object sender, TextCompositionEventArgs e)
        {
            e.Handled = !float.TryParse(e.Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float _);
        }

        private void XCoord_TextChanged(Object sender, EventArgs e)
        {
            var x = float.Parse(XCoord.Text, System.Globalization.CultureInfo.InvariantCulture);
            SetData("X", x);
        }

        private void YCoord_TextChanged(Object sender, EventArgs e)
        {
            var y = float.Parse(YCoord.Text, System.Globalization.CultureInfo.InvariantCulture);
            SetData("Y", y);
        }

        private void ZCoord_TextChanged(Object sender, EventArgs e)
        {
            var z = float.Parse(ZCoord.Text, System.Globalization.CultureInfo.InvariantCulture);
            SetData("Z", z);
        }

        private void WCoord_TextChanged(Object sender, EventArgs e)
        {
            var w = float.Parse(WCoord.Text, System.Globalization.CultureInfo.InvariantCulture);
            SetData("W", w);
        }
    }
}
