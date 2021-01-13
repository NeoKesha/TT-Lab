using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            LabeledCheckBox control = d as LabeledCheckBox;
            control.CheckboxLabel.Content = e.NewValue;
        }

        private static void OnCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledCheckBox control = d as LabeledCheckBox;
            control.DisplayChecked = (bool)e.NewValue;
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
