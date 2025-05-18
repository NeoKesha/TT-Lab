using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using DataTrigger = Microsoft.Xaml.Behaviors.Core.DataTrigger;

namespace TT_Lab.Controls;

public class SelectedItemChangedEventArgs : RoutedEventArgs
{
    private readonly object? _selectedItem;
    
    public SelectedItemChangedEventArgs(object sender, RoutedEvent @event, object? selectedItem) : base(@event, sender)
    {
        _selectedItem = selectedItem;
    }

    public T? GetSelectedItem<T>()
    {
        return (T?)_selectedItem;
    }
    
    protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
    {
        var handler = (SelectedItemChangedEventHandler)genericHandler;
        handler(genericTarget, this);
    }
}

public delegate void SelectedItemChangedEventHandler(object sender, SelectedItemChangedEventArgs e);

public partial class EditableListBox : UserControl
{
    public event RoutedEventHandler AddItem
    {
        add => AddHandler(AddItemEvent, value);
        remove => RemoveHandler(AddItemEvent, value);
    }
    
    public event RoutedEventHandler DeleteItem
    {
        add => AddHandler(DeleteItemEvent, value);
        remove => RemoveHandler(DeleteItemEvent, value);
    }
    
    public event RoutedEventHandler DuplicateItem
    {
        add => AddHandler(DuplicateItemEvent, value);
        remove => RemoveHandler(DuplicateItemEvent, value);
    }
    
    public event SelectedItemChangedEventHandler SelectedItemChanged
    {
        add => AddHandler(SelectedItemChangedEvent, value);
        remove => RemoveHandler(SelectedItemChangedEvent, value);
    }
    
    public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent("SelectedItemChanged",
        RoutingStrategy.Bubble, typeof(SelectedItemChangedEventHandler), typeof(EditableListBox));
    
    public static readonly RoutedEvent AddItemEvent = EventManager.RegisterRoutedEvent("AddItem",
        RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EditableListBox));
    
    public static readonly RoutedEvent DeleteItemEvent = EventManager.RegisterRoutedEvent("DeleteItem",
        RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EditableListBox));
    
    public static readonly RoutedEvent DuplicateItemEvent = EventManager.RegisterRoutedEvent("DuplicateItem",
        RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EditableListBox));

    public static readonly DependencyProperty ListBoxNameProperty = DependencyProperty.Register(
        nameof(ListBoxName), typeof(string), typeof(EditableListBox), new PropertyMetadata("Editable List"));

    public string ListBoxName
    {
        get => (string)GetValue(ListBoxNameProperty);
        set => SetValue(ListBoxNameProperty, value);
    }

    public static readonly DependencyProperty SizeLimitProperty = DependencyProperty.Register(
        nameof(SizeLimit), typeof(int), typeof(EditableListBox), new PropertyMetadata(-1));

    public int SizeLimit
    {
        get => (int)GetValue(SizeLimitProperty);
        set => SetValue(SizeLimitProperty, value);
    }
    
    public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
        nameof(Items), typeof(IEnumerable), typeof(EditableListBox), new PropertyMetadata(null));

    public IEnumerable? Items
    {
        get => (IEnumerable?)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
        nameof(ItemTemplate), typeof(DataTemplate), typeof(EditableListBox), new PropertyMetadata(default(DataTemplate)));

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    public static readonly DependencyProperty DataTriggerAttachedValueProperty = DependencyProperty.RegisterAttached(
        "DataTriggerAttachedValue", typeof(object), typeof(EditableListBox), new PropertyMetadata(null, OnDataTriggerValueChanged));

    private static void OnDataTriggerValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DataTrigger dataTrigger)
        {
            dataTrigger.Value = e.NewValue;
        }
    }

    public static object GetDataTriggerAttachedValue(DependencyObject d)
    {
        return d.GetValue(DataTriggerAttachedValueProperty);
    }

    public static void SetDataTriggerAttachedValue(DependencyObject d, object value)
    {
        d.SetValue(DataTriggerAttachedValueProperty, value);
    }

    public EditableListBox()
    {
        InitializeComponent();
    }

    private void OnAddItemClick(object sender, RoutedEventArgs e)
    {
        e.Handled = true;
        RaiseEvent(new RoutedEventArgs(AddItemEvent, this));
    }

    private void OnDeleteItemClick(object sender, RoutedEventArgs e)
    {
        e.Handled = true;
        RaiseEvent(new RoutedEventArgs(DeleteItemEvent, this));
    }

    private void OnDuplicateItemClick(object sender, RoutedEventArgs e)
    {
        e.Handled = true;
        RaiseEvent(new RoutedEventArgs(DuplicateItemEvent, this));
    }

    private void OnItemsStorageSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0)
        {
            return;
        }

        e.Handled = true;
        RaiseEvent(new SelectedItemChangedEventArgs(this, SelectedItemChangedEvent, e.AddedItems[0]));
    }
}