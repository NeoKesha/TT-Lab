using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TT_Lab.Controls
{
    /// <summary>
    /// Interaction logic for LabeledTextBox.xaml
    /// </summary>
    public partial class LabeledTextBox : UserControl
    {
        [Description("Name of the textbox displayed above."), Category("Common Properties")]
        public string TextBoxName
        {
            get => (string)GetValue(TextBoxNameProperty);
            set => SetValue(TextBoxNameProperty, value);
        }

        [Description("Input text."), Category("Common Properties")]
        public object Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        
        [Description("Allows updates only from the bindings"), Category("Common Properties")]
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }
        
        [Description("Whether the textbox label is horizontal or vertical in relation to the textbox"), Category("Common Properties")]
        public Orientation LayoutOrientation
        {
            get => (Orientation)GetValue(LayoutOrientationProperty);
            set => SetValue(LayoutOrientationProperty, value);
        }

        // Using a DependencyProperty as the backing store for LayoutOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayoutOrientationProperty =
            DependencyProperty.Register(nameof(LayoutOrientation), typeof(Orientation), typeof(LabeledTextBox),
                new PropertyMetadata(Orientation.Vertical, OnLayoutOrientationChanged));

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            nameof(IsReadOnly), typeof(bool), typeof(LabeledTextBox), new PropertyMetadata(false));

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(object), typeof(LabeledTextBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        // Using a DependencyProperty as the backing store for TextBoxName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxNameProperty =
            DependencyProperty.Register(nameof(TextBoxName), typeof(string), typeof(LabeledTextBox),
                new PropertyMetadata("Label"));

        public LabeledTextBox()
        {
            InitializeComponent();
        }
        
        private static void OnLayoutOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LabeledTextBox control)
            {
                control.UpdateOrientation();
            }
        }

        private void UpdateOrientation()
        {
            if (Content is StackPanel stackPanel)
            {
                stackPanel.Orientation = LayoutOrientation;
            }
        }
    }
}
