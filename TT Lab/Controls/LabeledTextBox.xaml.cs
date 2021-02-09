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
    public partial class LabeledTextBox : BoundUserControl
    {
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

        public string DisplayText
        {
            get { return (string)GetValue(DisplayTextProperty); }
            set { SetValue(DisplayTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register("DisplayText", typeof(string), typeof(LabeledTextBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender));

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LabeledTextBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnTextChanged)));

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
            InvokeUndo();
        }

        private void BaseTextBox_RedoPerformed(Object sender, EventArgs e)
        {
            InvokeRedo();
        }

        private static void OnNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledTextBox control = d as LabeledTextBox;
            control.TextBoxLabel.Content = e.NewValue;
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledTextBox control = d as LabeledTextBox;
            control.DisplayText = control.Text;
            Log.WriteLine($"Changed text in {control.Name} to {(string)e.NewValue}");
        }

        private void TextContainer_TextChanged(Object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(BoundProperty)) return;
            InvokePropChange(TextContainer.Text, Text);
        }
    }
}
