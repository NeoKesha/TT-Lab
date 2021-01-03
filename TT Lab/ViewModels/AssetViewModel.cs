using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TT_Lab.AssetData;
using TT_Lab.Assets;
using TT_Lab.Controls;
using TT_Lab.Project;

namespace TT_Lab.ViewModels
{
    public class AssetViewModel : ObservableObject
    {
        protected IAsset _asset;

        private AssetViewModel _parent;
        private ObservableCollection<AssetViewModel> _children;
        private List<AssetViewModel> _internalChildren;
        private Boolean _isSelected;
        private Boolean _isExpanded;
        private Visibility _isVisible;
        private Control _editor;

        public AssetViewModel(Guid asset) : this(asset, null)
        {
        }

        public AssetViewModel(Guid asset, AssetViewModel parent)
        {
            _asset = ProjectManagerSingleton.PM.OpenedProject.GetAsset(asset);
            _parent = parent;
            // Personally, I am against ever using type checks but in this situation it's acceptable
            if (_asset is Folder)
            {
                // Build the tree
                var myChildren = ((FolderData)(_asset as Folder).GetData()).Children;
                _children = new ObservableCollection<AssetViewModel>(
                    (from child in myChildren
                     orderby _asset.Order
                     let c = ProjectManagerSingleton.PM.OpenedProject.GetAsset(child)
                     select c.GetViewModel(this)).ToList());
                _internalChildren = new List<AssetViewModel>(_children);
            }
        }

        public Boolean EditorOpened
        {
            get
            {
                return _editor != null;
            }
        }

        public Control GetEditor()
        {
            if (_editor == null)
            {
                _editor = (Control)Activator.CreateInstance(Asset.GetEditorType(), this);
                _editor.Unloaded += (sender, e) =>
                {
                    CloseEditor();
                };
            }
            return _editor;
        }

        public Control GetEditor(Command.CommandManager commandManager)
        {
            if (_editor == null)
            {
                _editor = (Control)Activator.CreateInstance(Asset.GetEditorType(), this, commandManager);
                _editor.Unloaded += (sender, e) =>
                {
                    CloseEditor();
                };
            }
            return _editor;
        }

        public Control GetEditor(TabControl tabContainer)
        {
            if (_editor == null)
            {
                _editor = new TabItem
                {
                    Content = Activator.CreateInstance(Asset.GetEditorType(), this)
                };
                ((TabItem)_editor).Header = new ClosableTab(Alias, tabContainer, _editor);
                _editor.Unloaded += (sender, e) =>
                {
                    CloseEditor();
                };
            }
            return _editor;
        }

        public void CloseEditor()
        {
            _editor = null;
        }

        public ObservableCollection<AssetViewModel> Children
        {
            get
            {
                if (_asset.Type == typeof(ChunkFolder)) return null;
                return _children;
            }
        }

        public void ClearChildren()
        {
            // Only folders should be capable of this
            if (Children != null)
            {
                _children = new ObservableCollection<AssetViewModel>();
                _internalChildren.ForEach(a => a.ClearChildren());
            }
        }

        public void LoadChildrenBack()
        {
            if (Children != null)
            {
                _children = new ObservableCollection<AssetViewModel>(_internalChildren);
                _internalChildren.ForEach(a => a.LoadChildrenBack());
            }
        }

        public void AddChild(AssetViewModel a)
        {
            if (Children != null)
            {
                if (_internalChildren.Contains(a))
                {
                    _children.Add(a);
                }
            }
        }

        public List<AssetViewModel> GetInternalChildren()
        {
            if (Children != null)
            {
                return _internalChildren;
            }
            return null;
        }

        public Boolean IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    NotifyChange("IsSelected");
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
                    NotifyChange("IsExpanded");
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
                    NotifyChange("IsVisible");
                }

                if (_isVisible == Visibility.Visible && _parent != null)
                {
                    _parent._isVisible = Visibility.Visible;
                }
            }
        }

        public String Alias
        {
            get { return _asset.Alias; }
            set
            {
                if (value != _asset.Alias)
                {
                    _asset.Alias = value;
                    NotifyChange("Alias");
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
    }
}
