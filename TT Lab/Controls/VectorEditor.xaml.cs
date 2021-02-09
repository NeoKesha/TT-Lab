using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TT_Lab.ViewModels;

namespace TT_Lab.Controls
{
    /// <summary>
    /// Interaction logic for VectorEditor.xaml
    /// </summary>
    public partial class VectorEditor : BoundUserControl
    {

        [Description("Whether the fields will be vertical or horizontal"), Category("Common Properties")]
        public bool VerticalLayout
        {
            get { return (bool)GetValue(VerticalLayoutProperty); }
            set { SetValue(VerticalLayoutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalLayout.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalLayoutProperty =
            DependencyProperty.Register("VerticalLayout", typeof(bool), typeof(VectorEditor),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnLayoutChanged)));


        [Description("Amount of components in the given vector. Up to 4"), Category("Common Properties")]
        public int VectorComponentsAmount
        {
            get { return (int)GetValue(VectorComponentsAmountProperty); }
            set { SetValue(VectorComponentsAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VectorComponentsAmount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VectorComponentsAmountProperty =
            DependencyProperty.Register("VectorComponentsAmount", typeof(int), typeof(VectorEditor),
                new FrameworkPropertyMetadata(4, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnLayoutChanged)));



        public VectorEditor()
        {
            InitializeComponent();
        }

        public static Dictionary<string, Func<object, object, object?>> GetValidators()
        {
            var validators = new Dictionary<string, Func<object, object, object?>>
            {
                ["X"] = (n, o) =>
                {
                    var nStr = (string)n;
                    var oStr = (string)o;
                    if (oStr == nStr) return null;
                    if (string.IsNullOrEmpty(nStr)) return 0f;
                    if (!Single.TryParse(nStr, NumberStyles.Float, CultureInfo.InvariantCulture, out Single result)) return null;
                    return result;
                }
            };
            validators["Y"] = validators["X"];
            validators["Z"] = validators["X"];
            validators["W"] = validators["X"];
            return validators;
        }

        private static void OnLayoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as VectorEditor;
            var isVert = control.VerticalLayout;
            var compAmount = control.VectorComponentsAmount;
            if (e.Property.Name == nameof(control.VerticalLayout))
            {
                isVert = (bool)e.NewValue;
            }
            else if (e.Property.Name == nameof(control.VectorComponentsAmount))
            {
                compAmount = (int)e.NewValue;
                if (compAmount < 1 || compAmount > 4) return;

                control.YCoordBox.Visibility = Visibility.Visible;
                control.ZCoordBox.Visibility = Visibility.Visible;
                control.WCoordBox.Visibility = Visibility.Visible;
                switch (compAmount)
                {
                    case 1:
                        control.YCoordBox.Visibility = Visibility.Collapsed;
                        control.ZCoordBox.Visibility = Visibility.Collapsed;
                        control.WCoordBox.Visibility = Visibility.Collapsed;
                        break;
                    case 2:
                        control.ZCoordBox.Visibility = Visibility.Collapsed;
                        control.WCoordBox.Visibility = Visibility.Collapsed;
                        break;
                    case 3:
                        control.WCoordBox.Visibility = Visibility.Collapsed;
                        break;
                }
            }
            if (isVert)
            {
                control.FormationGrid.Columns = 0;
                control.FormationGrid.Rows = compAmount;
            }
            else
            {
                control.FormationGrid.Columns = compAmount;
                control.FormationGrid.Rows = 1;
            }
        }
    }
}
