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
    public partial class LabeledDropList : UserControl
    {

        [Description("Name of the droplist displayed above."), Category("Common Properties")]
        public string DropListName
        {
            get { return (string)GetValue(DropListNameProperty); }
            set { SetValue(DropListNameProperty, value); }
        }


        [Description("List of dropdown items."), Category("Common Properties")]
        public ObservableCollection<string> Items
        {
            get { return (ObservableCollection<string>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<string>), typeof(LabeledDropList),
                new FrameworkPropertyMetadata(new ObservableCollection<string>(), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnCollectionChanged)));


        // Using a DependencyProperty as the backing store for TextBoxName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropListNameProperty =
            DependencyProperty.Register("DropListName", typeof(string), typeof(LabeledDropList),
                new FrameworkPropertyMetadata("LabeledTextBox", FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnNameChanged)));


        public LabeledDropList()
        {
            InitializeComponent();
            SetValue(ItemsProperty, new ObservableCollection<string>());
            DataContext = this;
        }

        private static void OnNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledDropList control = d as LabeledDropList;
            control.ComboBoxName.Content = e.NewValue;
        }

        private static void OnCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LabeledDropList control = d as LabeledDropList;
            control.DropList.ItemsSource = (ObservableCollection<string>)e.NewValue;
        }
    }
}
