using System;
using System.ComponentModel;
using System.Windows;

namespace TT_Lab.Controls
{
    /// <summary>
    /// Interaction logic for LabeledCheckBox.xaml
    /// </summary>
    public partial class LabeledCheckBox : BoundUserControl
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
        public bool IsHorizontal
        {
            get { return (bool)GetValue(IsHorizontalProperty); }
            set { SetValue(IsHorizontalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsHorizontal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHorizontalProperty =
            DependencyProperty.Register("IsHorizontal", typeof(bool), typeof(LabeledCheckBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnLayoutChanged)));



        public bool DisplayChecked
        {
            get { return (bool)GetValue(DisplayCheckedProperty); }
            set { SetValue(DisplayCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayCheckedProperty =
            DependencyProperty.Register("DisplayChecked", typeof(bool), typeof(LabeledCheckBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        // Using a DependencyProperty as the backing store for Checked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.Register("Checked", typeof(bool), typeof(LabeledCheckBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnCheckedChanged)));

        // Using a DependencyProperty as the backing store for TextBoxName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckBoxNameProperty =
            DependencyProperty.Register("CheckBoxName", typeof(string), typeof(LabeledCheckBox),
                new FrameworkPropertyMetadata("Label", FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnNameChanged)));

        private static void OnNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledCheckBox control = (LabeledCheckBox)d;
            control.CheckboxLabel.Content = e.NewValue;
            if (control.IsHorizontal)
            {
                control.CheckBox.Content = control.CheckboxLabel.Content;
            }
        }

        private static void OnCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledCheckBox control = (LabeledCheckBox)d;
            control.DisplayChecked = (bool)e.NewValue;
        }

        private static void OnLayoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledCheckBox control = (LabeledCheckBox)d;
            control.IsHorizontal = (bool)e.NewValue;
            if (control.IsHorizontal)
            {
                control.CheckboxLabel.Visibility = Visibility.Collapsed;
                control.CheckBox.Content = control.CheckboxLabel.Content;
            }
            else
            {
                control.CheckboxLabel.Visibility = Visibility.Visible;
                control.CheckBox.Content = String.Empty;
            }
        }

        private void CheckBox_Checked(Object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BoundProperty)) return;
            InvokePropChange(true, false);
        }

        private void CheckBox_Unchecked(Object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BoundProperty)) return;
            InvokePropChange(false, true);
        }
    }
}
