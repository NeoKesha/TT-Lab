using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

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
            get => (string)GetValue(DropListNameProperty);
            set => SetValue(DropListNameProperty, value);
        }

        [Description("List of dropdown items."), Category("Common Properties")]
        public ObservableCollection<object> Items
        {
            get => (ObservableCollection<object>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        [Description("Index of the selected item from the dropdown."), Category("Common Properties")]
        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        [Description("Selected item from the dropdown."), Category("Common Properties")]
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
        
        [Description("Whether the textbox label is horizontal or vertical in relation to the textbox"), Category("Common Properties")]
        public Orientation LayoutOrientation
        {
            get => (Orientation)GetValue(LayoutOrientationProperty);
            set => SetValue(LayoutOrientationProperty, value);
        }
        
        // Dependency Property for ItemTemplate
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(LabeledDropList),
                new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(LabeledDropList),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender));

        // Using a DependencyProperty as the backing store for SelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(LabeledDropList),
                new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender));

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(nameof(Items), typeof(IEnumerable), typeof(LabeledDropList),
                new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for TextBoxName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropListNameProperty =
            DependencyProperty.Register(nameof(DropListName), typeof(string), typeof(LabeledDropList),
                new PropertyMetadata("Label"));
        
        // Using a DependencyProperty as the backing store for LayoutOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LayoutOrientationProperty =
            DependencyProperty.Register(nameof(LayoutOrientation), typeof(Orientation), typeof(LabeledDropList),
                new PropertyMetadata(Orientation.Vertical, OnLayoutOrientationChanged));

        public LabeledDropList()
        {
            InitializeComponent();
        }
        
        private static void OnLayoutOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not LabeledDropList control)
            {
                return;
            }
            
            var orientation = (Orientation)e.NewValue;
            control.UpdateOrientation(orientation);
        }

        private void UpdateOrientation(Orientation orientation)
        {
            if (Content is not DockPanel)
            {
                return;
            }
            
            switch (orientation)
            {
                case Orientation.Horizontal:
                    DockPanel.SetDock(LblDropBoxName, Dock.Left);
                    break;
                case Orientation.Vertical:
                    DockPanel.SetDock(LblDropBoxName, Dock.Top);
                    break;
            }
            ElementContainer.UpdateLayout();
        }
    }
}
