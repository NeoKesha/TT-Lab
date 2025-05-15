using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TT_Lab.Assets;

namespace TT_Lab.Controls;

public partial class ResourceBrowser : UserControl
{
    public event EventHandler? OnBrowseClicked;
    
    public static readonly DependencyProperty BrowseTypeProperty = DependencyProperty.Register(
        nameof(BrowseType), typeof(Type), typeof(ResourceBrowser), new PropertyMetadata(typeof(IAsset)));

    [Description("Type of asset to browse"), Category("Common Properties")]
    public Type BrowseType
    {
        get => (Type)GetValue(BrowseTypeProperty);
        set => SetValue(BrowseTypeProperty, value);
    }
    
    // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(object), typeof(ResourceBrowser),
            new FrameworkPropertyMetadata("No resource", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    
    [Description("Input text."), Category("Common Properties")]
    public object Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public ResourceBrowser()
    {
        InitializeComponent();
    }

    private void OnBrowseClick(object sender, RoutedEventArgs e)
    {
        OnBrowseClicked?.Invoke(this, e);
    }
}