using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for LabeledDropList.xaml
    /// </summary>
    public partial class LabeledDropList : BoundUserControl
    {

        [Description("Name of the droplist displayed above."), Category("Common Properties")]
        public string DropListName
        {
            get { return (string)GetValue(DropListNameProperty); }
            set { SetValue(DropListNameProperty, value); }
        }

        [Description("List of dropdown items."), Category("Common Properties")]
        public ObservableCollection<object> Items
        {
            get { return (ObservableCollection<object>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        [Description("Index of the selected item from the dropdown."), Category("Common Properties")]
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        [Description("Selected item from the dropdown."), Category("Common Properties")]
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public int DisplaySelectedIndex
        {
            get { return (int)GetValue(DisplaySelectedIndexProperty); }
            set { SetValue(DisplaySelectedIndexProperty, value); }
        }

        public object DisplaySelectedItem
        {
            get { return GetValue(DisplaySelectedItemProperty); }
            set { SetValue(DisplaySelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplaySelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplaySelectedItemProperty =
            DependencyProperty.Register("DisplaySelectedItem", typeof(object), typeof(LabeledDropList),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        // Using a DependencyProperty as the backing store for DisplaySelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplaySelectedIndexProperty =
            DependencyProperty.Register("DisplaySelectedIndex", typeof(int), typeof(LabeledDropList),
                new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.AffectsRender));

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(LabeledDropList),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnItemChanged)));

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(LabeledDropList),
                new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnIndexChanged)));

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<object>), typeof(LabeledDropList),
                new FrameworkPropertyMetadata(new ObservableCollection<object>(), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnCollectionChanged)));

        // Using a DependencyProperty as the backing store for TextBoxName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropListNameProperty =
            DependencyProperty.Register("DropListName", typeof(string), typeof(LabeledDropList),
                new FrameworkPropertyMetadata("LabeledTextBox", FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnNameChanged)));


        public LabeledDropList()
        {
            InitializeComponent();
            SetValue(ItemsProperty, new ObservableCollection<object>());
        }

        private static void OnNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledDropList control = d as LabeledDropList;
            control.ComboBoxName.Content = e.NewValue;
        }

        private static void OnCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledDropList control = d as LabeledDropList;
            control.DropList.ItemsSource = (ObservableCollection<object>)e.NewValue;
        }

        private static void OnIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledDropList control = d as LabeledDropList;
            control.DisplaySelectedIndex = (int)e.NewValue;
        }

        private static void OnItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledDropList control = d as LabeledDropList;
            control.DisplaySelectedItem = e.NewValue;
        }

        private void DropList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(BoundProperty)) return;
            InvokePropChange(DropList.SelectedItem, SelectedItem);
        }
    }
}
