using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TT_Lab.AssetData;
using TT_Lab.Assets;

namespace TT_Lab.ViewModels
{
    public class ResourceTreeElementViewModel : PropertyChangedBase
    {
        private readonly IAsset _asset;
        private readonly ResourceTreeElementViewModel? _parent;
        private BindableCollection<ResourceTreeElementViewModel>? _children;
        private List<ResourceTreeElementViewModel>? _internalChildren;
        private Boolean _isTargetItem;
        private Boolean _isSelected;
        private Boolean _isExpanded;
        private Visibility _isVisible;

        public ResourceTreeElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent = null)
        {
            _asset = AssetManager.Get().GetAsset(asset);
            _parent = parent;
        }


        public BindableCollection<ResourceTreeElementViewModel>? Children => _asset.Type == typeof(ChunkFolder) ? null : _children;

        public void BuildChildren(Folder folder)
        {
            // Build the tree
            var myChildren = folder.GetData().To<FolderData>().Children;
            _children = new BindableCollection<ResourceTreeElementViewModel>(
                (from child in myChildren
                 orderby _asset.Order
                 let c = AssetManager.Get().GetAsset(child)
                 select c.GetResourceTreeElement(this)).ToList());
            _internalChildren = new List<ResourceTreeElementViewModel>(_children);
        }

        public void ClearChildren()
        {
            // Only folders should be capable of this
            if (_internalChildren != null)
            {
                _children = new BindableCollection<ResourceTreeElementViewModel>();
                _internalChildren.ForEach(a => a.ClearChildren());
            }
        }

        public void LoadChildrenBack()
        {
            if (_internalChildren != null)
            {
                _children = new BindableCollection<ResourceTreeElementViewModel>(_internalChildren);
                _internalChildren.ForEach(a => a.LoadChildrenBack());
            }
        }

        public void AddChild(ResourceTreeElementViewModel a)
        {
            if (_internalChildren != null && _children != null)
            {
                if (_internalChildren.Contains(a))
                {
                    _children.Add(a);
                }
            }
        }

        public List<ResourceTreeElementViewModel>? GetInternalChildren()
        {
            return Children != null ? _internalChildren : null;
        }

        public Boolean IsTargetItem
        {
            get => _isTargetItem;
            set
            {
                if (value != _isTargetItem)
                {
                    _isTargetItem = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public String IconPath => $"/Media/LabIcons/{Asset.IconPath}";

        public Boolean IsSelected
        {
            get => _isSelected;
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public Boolean IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    NotifyOfPropertyChange();
                }

                if (_isExpanded && _parent != null)
                {
                    _parent.IsExpanded = true;
                }
            }
        }

        public Visibility IsVisible
        {
            get => _isVisible;
            set
            {
                if (value != _isVisible)
                {
                    _isVisible = value;
                    NotifyOfPropertyChange();
                }

                if (_isVisible == Visibility.Visible && _parent != null)
                {
                    _parent._isVisible = Visibility.Visible;
                }
            }
        }

        public String Alias
        {
            get => _asset.Alias;
            set
            {
                if (value != _asset.Alias)
                {
                    _asset.Alias = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public IAsset Asset => _asset;

        public T GetAsset<T>() where T : IAsset
        {
            return (T)_asset;
        }
    }
}
