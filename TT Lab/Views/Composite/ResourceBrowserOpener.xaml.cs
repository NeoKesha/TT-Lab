using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using TT_Lab.Assets;
using TT_Lab.ViewModels;

namespace TT_Lab.Views.Composite;

public partial class ResourceBrowserOpener : UserControl
{
    public static readonly DependencyProperty BrowseTypeProperty = DependencyProperty.Register(
        nameof(BrowseType), typeof(Type), typeof(ResourceBrowserOpener), new PropertyMetadata(typeof(IAsset)));

    [Description("Type of asset to browse"), Category("Common Properties")]
    public Type BrowseType
    {
        get => (Type)GetValue(BrowseTypeProperty);
        set => SetValue(BrowseTypeProperty, value);
    }
    
    // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty BrowserNameProperty =
        DependencyProperty.Register(nameof(BrowserName), typeof(object), typeof(ResourceBrowserOpener),
            new FrameworkPropertyMetadata("Browser", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    
    [Description("Input text."), Category("Common Properties")]
    public object BrowserName
    {
        get => GetValue(BrowserNameProperty);
        set => SetValue(BrowserNameProperty, value);
    }

    public static readonly DependencyProperty LinkedResourceProperty = DependencyProperty.Register(
        nameof(LinkedResource), typeof(LabURI), typeof(ResourceBrowserOpener), new FrameworkPropertyMetadata(LabURI.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public LabURI LinkedResource
    {
        get => (LabURI)GetValue(LinkedResourceProperty);
        set => SetValue(LinkedResourceProperty, value);
    }

    public static readonly DependencyProperty ResourcesToBrowseProperty = DependencyProperty.Register(
        nameof(ResourcesToBrowse), typeof(ObservableCollection<LabURI>), typeof(ResourceBrowserOpener), new PropertyMetadata(default(ObservableCollection<LabURI>?)));

    public ObservableCollection<LabURI>? ResourcesToBrowse
    {
        get => (ObservableCollection<LabURI>?)GetValue(ResourcesToBrowseProperty);
        set => SetValue(ResourcesToBrowseProperty, value);
    }
    
    public ResourceBrowserOpener()
    {
        InitializeComponent();
    }

    private void OnOpenBrowser(object sender, RoutedEventArgs e)
    {
        var linkBrowser = ResourcesToBrowse == null ? new ResourceBrowserViewModel(BrowseType) : new ResourceBrowserViewModel(ResourcesToBrowse);
        var windowManager = IoC.Get<IWindowManager>();
        var linkBrowserWindow = windowManager.ShowDialogAsync(linkBrowser);
        linkBrowserWindow.Wait();
        var result = linkBrowserWindow.Result;
        if (result.HasValue && result.Value)
        {
            LinkedResource = linkBrowser.SelectedLink;
        }
    }
}