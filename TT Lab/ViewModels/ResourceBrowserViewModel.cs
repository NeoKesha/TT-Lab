using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using TT_Lab.Assets;

namespace TT_Lab.ViewModels;

public class ResourceBrowserViewModel : Screen
{
    private readonly BindableCollection<LabURI> _resourcesToBrowse;
    private BindableCollection<LabURI> _resourcesToBrowseView;
    private string _searchAsset = string.Empty;

    public ResourceBrowserViewModel(Type browseType)
    {
        _resourcesToBrowse = new BindableCollection<LabURI> { LabURI.Empty };
        SelectedLink = _resourcesToBrowse[0];
        _resourcesToBrowse.AddRange(AssetManager.Get().GetAllAssetsOf(browseType).Select(a => a.URI));
        _resourcesToBrowseView = new BindableCollection<LabURI>(_resourcesToBrowse.Order());
    }
    
    public ResourceBrowserViewModel(IEnumerable<LabURI> resourcesToBrowse)
    {
        _resourcesToBrowse = new BindableCollection<LabURI>(resourcesToBrowse);
        SelectedLink = _resourcesToBrowse[0];
        _resourcesToBrowseView = new BindableCollection<LabURI>(_resourcesToBrowse.Order());
    }

    public void Link()
    {
        TryCloseAsync(true);
    }

    private void DoSearch()
    {
        if (_searchAsset == string.Empty)
        {
            _resourcesToBrowseView = _resourcesToBrowse;
        }
        else
        {
            _resourcesToBrowseView = new BindableCollection<LabURI>(_resourcesToBrowse.Where(uri =>
            {
                if (uri == LabURI.Empty)
                {
                    return LabURI.Empty.ToString().Contains(_searchAsset, StringComparison.CurrentCultureIgnoreCase);
                }

                var asset = AssetManager.Get().GetAsset(uri);
                return asset.Name.Contains(_searchAsset, StringComparison.CurrentCultureIgnoreCase)
                       || asset.Alias.Contains(_searchAsset, StringComparison.CurrentCultureIgnoreCase)
                       || asset.Data.Contains(_searchAsset, StringComparison.CurrentCultureIgnoreCase);
            }));
        }

        _resourcesToBrowseView = new BindableCollection<LabURI>(_resourcesToBrowseView.Order());
        NotifyOfPropertyChange(nameof(ResourcesToBrowseView));
    }

    public LabURI SelectedLink { get; set; }
    public BindableCollection<LabURI> ResourcesToBrowseView => _resourcesToBrowseView;

    public string SearchAsset
    {
        get => _searchAsset;
        set
        {
            if (value != _searchAsset)
            {
                _searchAsset = value;
                DoSearch();
            }
        }
    }
}