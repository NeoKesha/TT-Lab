using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TT_Lab.Editors;

namespace TT_Lab.Controls
{
    /// <summary>
    /// Interaction logic for LabeledTextBox.xaml
    /// </summary>
    public partial class LabeledTextBox : UserControl
    {
        public event EventHandler UndoPerformed;
        public event EventHandler RedoPerformed;
        public event EventHandler<TextChangedEventArgs> TextChanged;

        [Description("Name of the textbox displayed above."), Category("Common Properties")]
        public string TextBoxName
        {
            get { return (string)GetValue(TextBoxNameProperty); }
            set { SetValue(TextBoxNameProperty, value); }
        }


        [Description("Input text."), Category("Common Properties")]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        [Description("Editor owning this textbox"), Category("Common Properties")]
        public BaseEditor Editor
        {
            get { return (BaseEditor)GetValue(EditorProperty); }
            set { SetValue(EditorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Editor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditorProperty =
            DependencyProperty.Register("Editor", typeof(BaseEditor), typeof(LabeledTextBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnEditorChanged)));

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LabeledTextBox),
                new FrameworkPropertyMetadata("This is labeled textbox", FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnTextChanged)));

        // Using a DependencyProperty as the backing store for TextBoxName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxNameProperty =
            DependencyProperty.Register("TextBoxName", typeof(string), typeof(LabeledTextBox),
                new FrameworkPropertyMetadata("Label", FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnNameChanged)));

        public LabeledTextBox()
        {
            InitializeComponent();
        }

        private void BaseTextBox_UndoPerformed(Object sender, EventArgs e)
        {
            var handler = UndoPerformed;
            handler?.Invoke(this, e);
        }

        private void BaseTextBox_RedoPerformed(Object sender, EventArgs e)
        {
            var handler = RedoPerformed;
            handler?.Invoke(this, e);
        }

        private void BaseTextBox_TextChanged(Object sender, TextChangedEventArgs e)
        {
            e.Source = TextContainer;
            var handler = TextChanged;
            handler?.Invoke(this, e);
        }

        private static void OnNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledTextBox control = d as LabeledTextBox;
            control.TextBoxLabel.Content = e.NewValue;
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledTextBox control = d as LabeledTextBox;
            control.TextContainer.Text = (string)e.NewValue;
        }

        private static void OnEditorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledTextBox control = d as LabeledTextBox;
            control.UndoPerformed += control.Editor.UndoExecuted;
            control.RedoPerformed += control.Editor.RedoExecuted;
        }
    }
}
