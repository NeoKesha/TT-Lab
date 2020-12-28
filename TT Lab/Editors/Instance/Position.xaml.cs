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
    /// Interaction logic for Position.xaml
    /// </summary>
    public partial class Position : BaseEditor
    {
        public Position()
        {
            InitializeComponent();
        }

        public Position(AssetViewModel positionModel) : base(positionModel)
        {
            InitializeComponent();
            DataContext = positionModel.Asset.GetData();
        }

        private void Coord_PreviewTextInput(Object sender, TextCompositionEventArgs e)
        {
            e.Handled = !float.TryParse(e.Text, out float _);
        }

        private void XCoord_TextChanged(Object sender, EventArgs e)
        {
            var x = float.Parse(XCoord.Text);
            var posData = (PositionData)DataContext;
            UpdateVector(new Vector4(x, posData.Coords.Y, posData.Coords.Z, posData.Coords.W));
        }

        private void YCoord_TextChanged(Object sender, EventArgs e)
        {
            var y = float.Parse(YCoord.Text);
            var posData = (PositionData)DataContext;
            UpdateVector(new Vector4(posData.Coords.X, y, posData.Coords.Z, posData.Coords.W));
        }

        private void ZCoord_TextChanged(Object sender, EventArgs e)
        {
            var z = float.Parse(ZCoord.Text);
            var posData = (PositionData)DataContext;
            UpdateVector(new Vector4(posData.Coords.X, posData.Coords.Y, z, posData.Coords.W));
        }

        private void WCoord_TextChanged(Object sender, EventArgs e)
        {
            var w = float.Parse(WCoord.Text);
            var posData = (PositionData)DataContext;
            UpdateVector(new Vector4(posData.Coords.X, posData.Coords.Y, posData.Coords.Z, w));
        }

        private void UpdateVector(Vector4 newPos)
        {
            CommandManager.Execute(new SetDataCommand(DataContext, "Coords", newPos));
        }
    }
}
