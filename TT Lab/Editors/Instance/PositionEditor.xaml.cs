using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using Twinsanity.TwinsanityInterchange.Enumerations;

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
            var pvm = (PositionViewModel)positionModel;
            InitializeComponent();
            DataContext = new
            {
                ViewModel = pvm,
                Layers = new ObservableCollection<object>(Enum.GetValues(typeof(Enums.Layouts)).Cast<object>())
            };
            InitValidators();
        }

        private void InitValidators()
        {
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
