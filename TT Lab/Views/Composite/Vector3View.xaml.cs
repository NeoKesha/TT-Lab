using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TT_Lab.Views.Composite
{
    /// <summary>
    /// Interaction logic for Vector3View.xaml
    /// </summary>
    public partial class Vector3View : UserControl
    {

        [Description("Whether the fields will be vertical or horizontal"), Category("Common Properties")]
        public bool VerticalLayout
        {
            get { return (bool)GetValue(VerticalLayoutProperty); }
            set { SetValue(VerticalLayoutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalLayout.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalLayoutProperty =
            DependencyProperty.Register(nameof(VerticalLayout), typeof(bool), typeof(Vector3View),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnLayoutChanged)));

        public Vector3View()
        {
            InitializeComponent();
        }

        private static void OnLayoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Vector3View;
            var isVert = control.VerticalLayout;
            if (e.Property.Name == nameof(control.VerticalLayout))
            {
                isVert = (bool)e.NewValue;
            }

            if (isVert)
            {
                control.FormationGrid.Columns = 0;
                control.FormationGrid.Rows = 3;
            }
            else
            {
                control.FormationGrid.Columns = 3;
                control.FormationGrid.Rows = 1;
            }
        }
    }
}
