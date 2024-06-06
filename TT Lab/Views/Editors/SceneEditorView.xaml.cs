using OpenTK.Wpf;
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

        public delegate void RenderEventHandler(object sender, RenderEventArgs delta);

        public event RenderEventHandler Render
        {
            add { AddHandler(RenderEvent, value); }
            remove { RemoveHandler(RenderEvent, value); }
        }

        public static readonly RoutedEvent RenderEvent = EventManager.RegisterRoutedEvent(
                name: "Render",
                routingStrategy: RoutingStrategy.Bubble,
                handlerType: typeof(RenderEventHandler),
                ownerType: typeof(SceneEditorView)
            );

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(SceneEditorView),
                new FrameworkPropertyMetadata("Scene viewer", FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnHeaderChanged)));

        public SceneEditorView()
        {
            InitializeComponent();

            var settings = new GLWpfControlSettings
            {
                MajorVersion = 4,
                MinorVersion = 3,
                GraphicsProfile = OpenTK.Windowing.Common.ContextProfile.Compatability,
                GraphicsContextFlags = OpenTK.Windowing.Common.ContextFlags.Debug
            };

            GlControlView.Start(settings);
            Loaded += SceneEditorView_Loaded;
            Unloaded += SceneEditorView_Unloaded;
        }

        private void SceneEditorView_Loaded(System.Object sender, RoutedEventArgs e)
        {
            GlControlView.Render += GlControl_Render;
        }

        private void SceneEditorView_Unloaded(System.Object sender, RoutedEventArgs e)
        {
            GlControlView.Render -= GlControl_Render;
        }

        private void GlControl_Render(System.TimeSpan obj)
        {
            if (GlControlView.Context == null)
            {
                return;
            }

            GlControlView.Context.MakeCurrent();
            RaiseEvent(new RenderEventArgs(obj));
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SceneEditorView control)
            {
                control.SceneHeader.Header = e.NewValue;
            }
        }
    }

    public class RenderEventArgs : RoutedEventArgs
    {
        private System.TimeSpan _delta;

        public RenderEventArgs(System.TimeSpan delta) : base()
        {
            _delta = delta;
            RoutedEvent = SceneEditorView.RenderEvent;
        }

        public System.TimeSpan Delta => _delta;
    }
}
