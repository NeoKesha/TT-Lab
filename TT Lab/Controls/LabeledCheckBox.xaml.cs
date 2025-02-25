using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TT_Lab.Controls
{
    /// <summary>
    /// Interaction logic for LabeledCheckBox.xaml
    /// </summary>
    public partial class LabeledCheckBox : UserControl
    {
        public LabeledCheckBox()
        {
            InitializeComponent();
        }

        [Description("Name of the checkbox displayed above."), Category("Common Properties")]
        public string CheckBoxName
        {
            get { return (string)GetValue(CheckBoxNameProperty); }
            set { SetValue(CheckBoxNameProperty, value); }
        }

        [Description("Whether the checkbox is checked"), Category("Common Properties")]
        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        [Description("Whether the checkbox label is horizontal or vertical in relation to the checkbox"), Category("Common Properties")]
        public Orientation LayoutOrientation
        {
            get => (Orientation)GetValue(LayoutOrientationProperty);
            set => SetValue(LayoutOrientationProperty, value);
        }

        // Using a DependencyProperty as the backing store for LayoutOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayoutOrientationProperty =
            DependencyProperty.Register(nameof(LayoutOrientation), typeof(Orientation), typeof(LabeledCheckBox),
                new PropertyMetadata(Orientation.Vertical, OnLayoutOrientationChanged));

        // Using a DependencyProperty as the backing store for Checked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.Register(nameof(Checked), typeof(bool), typeof(LabeledCheckBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        // Using a DependencyProperty as the backing store for TextBoxName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckBoxNameProperty =
            DependencyProperty.Register(nameof(CheckBoxName), typeof(string), typeof(LabeledCheckBox), new PropertyMetadata("Label"));

        
        private static void OnLayoutOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LabeledCheckBox control)
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
