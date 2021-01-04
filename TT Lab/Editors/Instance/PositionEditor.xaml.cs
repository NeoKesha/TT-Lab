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
using TT_Lab.ViewModels.Instance;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.Editors.Instance
{
    /// <summary>
    /// Interaction logic for PositionEditor.xaml
    /// </summary>
    public partial class PositionEditor : BaseEditor
    {
        private float eps = 0.00001f;
        private PositionViewModel pvm;

        public PositionEditor()
        {
            InitializeComponent();
        }

        public PositionEditor(AssetViewModel positionModel, Command.CommandManager commandManager) : base(positionModel, commandManager)
        {
            pvm = (PositionViewModel)positionModel;
            InitializeComponent();
        }

        private void Coord_PreviewTextInput(Object sender, TextCompositionEventArgs e)
        {
        }

        private void XCoord_TextChanged(Object sender, EventArgs e)
        {
            if (!float.TryParse(XCoord.Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float _))
                return;
            var x = float.Parse(XCoord.Text, System.Globalization.CultureInfo.InvariantCulture);
            if (!CompareFloats(x, pvm.X))
            {
                SetData("X", x);
            }
        }

        private void YCoord_TextChanged(Object sender, EventArgs e)
        {
            if (!float.TryParse(YCoord.Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float _))
                return;
            var y = float.Parse(YCoord.Text, System.Globalization.CultureInfo.InvariantCulture);
            if (!CompareFloats(y, pvm.Y))
            {
                SetData("Y", y);
            }
        }

        private void ZCoord_TextChanged(Object sender, EventArgs e)
        {
            if (!float.TryParse(ZCoord.Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float _))
                return;
            var z = float.Parse(ZCoord.Text, System.Globalization.CultureInfo.InvariantCulture);
            if (!CompareFloats(z, pvm.Z))
            {
                SetData("Z", z);
            }
        }

        private void WCoord_TextChanged(Object sender, EventArgs e)
        {
            if (!float.TryParse(WCoord.Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float _))
                return;
            var w = float.Parse(WCoord.Text, System.Globalization.CultureInfo.InvariantCulture);
            if (!CompareFloats(w, pvm.W))
            {
                SetData("W", w);
            }
        }

        private bool CompareFloats(float f1, float f2)
        {
            return Math.Abs(f1 - f2) < eps;
        }
    }
}
