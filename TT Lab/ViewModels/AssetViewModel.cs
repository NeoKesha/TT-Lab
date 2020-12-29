using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData;
using TT_Lab.Assets;
using TT_Lab.Controls;
using TT_Lab.Project;

namespace TT_Lab.ViewModels
{
    public class AssetViewModel : ObservableObject
    {
        private IAsset _asset;
        private AssetViewModel _parent;
        private IReadOnlyCollection<AssetViewModel> _children;
        private Boolean _isSelected;
        private Boolean _isExpanded;
        private Control _editor;

        public AssetViewModel(Guid asset) : this(asset, null) { }

        public AssetViewModel(Guid asset, AssetViewModel parent)
        {
            _asset = ProjectManagerSingleton.PM.OpenedProject.GetAsset(asset);
            _parent = parent;
            // Personally, I am against ever using type checks but in this situation it's acceptable
            if (_asset is Folder)
            {
                // Build the tree
                var myChildren = ((FolderData)(_asset as Folder).GetData()).Children;
                _children = new ReadOnlyCollection<AssetViewModel>(
                    (from child in myChildren
                     orderby _asset.Order
                     select new AssetViewModel(child, this as AssetViewModel)).ToList());
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
                    Header = new ClosableTab(Alias, tabContainer, _editor),
                    Content = Activator.CreateInstance(Asset.GetEditorType(), this)
                };
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
        public IReadOnlyCollection<AssetViewModel> Children
        {
            get
            {
                if (_asset.Type == "ChunkFolder") return null;
                return _children;
            }
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
