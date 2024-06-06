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
        protected IAsset _asset;

        private ResourceTreeElementViewModel? _parent;
        private BindableCollection<ResourceTreeElementViewModel>? _children;
        private List<ResourceTreeElementViewModel>? _internalChildren;
        private Boolean _isTargetItem;
        private Boolean _isSelected;
        private Boolean _isExpanded;
        private Visibility _isVisible;

        public ResourceTreeElementViewModel(LabURI asset) : this(asset, null)
        {
        }

        public ResourceTreeElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent)
        {
            _asset = AssetManager.Get().GetAsset(asset);
            _parent = parent;
        }


        public BindableCollection<ResourceTreeElementViewModel>? Children
        {
            get
            {
                if (_asset.Type == typeof(ChunkFolder)) return null;
                return _children;
            }
        }

        public void BuildChildren(Folder folder)
        {
            // Build the tree
            var myChildren = folder.GetData().To<FolderData>().Children;
            var cList = (from child in myChildren
                         orderby _asset.Order
                         let c = AssetManager.Get().GetAsset(child)
                         select c).ToList();
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
            if (Children != null)
            {
                return _internalChildren;
            }
            return null;
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

        public String IconPath
        {
            get => Asset.IconPath;
        }

        public Boolean IsSelected
        {
            get { return _isSelected; }
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
            get { return _isExpanded; }
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
            get { return _isVisible; }
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

        public virtual String Alias
        {
            get { return _asset.Alias; }
            set
            {
                if (value != _asset.Alias)
                {
                    _asset.Alias = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public IAsset Asset
        {
            get
            {
                return _asset;
            }
        }

        public T GetAsset<T>() where T : IAsset
        {
            return (T)_asset;
        }
    }
}
