using System.ComponentModel;
using System.Windows;

namespace TT_Lab.Views.Editors
{
    /// <summary>
    /// Interaction logic for SceneEditorView.xaml
    /// </summary>
    public partial class SceneEditorView : System.Windows.Controls.UserControl
    {

        [Description("Scene viewer's header"), Category("Common Properties")]
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(SceneEditorView),
                new FrameworkPropertyMetadata("Scene viewer", FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnHeaderChanged)));

        public SceneEditorView()
        {
            InitializeComponent();
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SceneEditorView control)
            {
                control.SceneHeader.Header = e.NewValue;
            }
        }
    }
}
