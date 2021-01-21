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
            var isVert = (bool)e.NewValue;
            if (isVert)
            {
                control.FormationGrid.Columns = 0;
                control.FormationGrid.Rows = 4;
            }
            else
            {
                control.FormationGrid.Columns = 4;
                control.FormationGrid.Rows = 1;
            }
        }
    }
}
