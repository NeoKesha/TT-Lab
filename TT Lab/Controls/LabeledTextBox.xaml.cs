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

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            nameof(IsReadOnly), typeof(bool), typeof(LabeledTextBox), new PropertyMetadata(false));

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(object), typeof(LabeledTextBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));

        // Using a DependencyProperty as the backing store for TextBoxName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxNameProperty =
            DependencyProperty.Register(nameof(TextBoxName), typeof(string), typeof(LabeledTextBox),
                new PropertyMetadata("Label"));

        public LabeledTextBox()
        {
            InitializeComponent();
        }
        
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = (LabeledTextBox)d;
            Log.WriteLine($"Changed text in {textBox.TextBoxName} to {e.NewValue}");
        }
    }
}
