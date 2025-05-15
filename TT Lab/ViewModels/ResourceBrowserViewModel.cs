using System;
using System.Linq;
using Caliburn.Micro;
using TT_Lab.Assets;

namespace TT_Lab.ViewModels;

public class ResourceBrowserViewModel : Screen
{
    private BindableCollection<LabURI> _resourcesToBrowse;

    public ResourceBrowserViewModel(Type browseType)
    {
        _resourcesToBrowse = new BindableCollection<LabURI> { LabURI.Empty };
        SelectedLink = _resourcesToBrowse[0];
        _resourcesToBrowse.AddRange(AssetManager.Get().GetAllAssetsOf(browseType).Select(a => a.URI));
    }

    public void Link()
    {
        TryCloseAsync(true);
    }

    public LabURI SelectedLink { get; set; }
    public BindableCollection<LabURI> ResourcesToBrowse => _resourcesToBrowse;
}