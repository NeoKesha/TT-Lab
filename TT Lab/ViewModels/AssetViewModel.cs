using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TT_Lab.AssetData;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.Editors;
using TT_Lab.Project;
using TT_Lab.Util;

namespace TT_Lab.ViewModels
{
    public class AssetViewModel : SaveableViewModel
    {
        protected IAsset _asset;

        private AssetViewModel? _parent;
        private ObservableCollection<AssetViewModel> _children;
        private List<AssetViewModel> _internalChildren;
        private Boolean _isSelected;
        private Boolean _isExpanded;
        private Visibility _isVisible;
        private Control? _editor;
        private bool _dirty;
        private OpenDialogueCommand.DialogueResult _dialogueResult = new();
        private ICommand _unsavedChangesCommand;

        private AssetViewModel()
        {
            _unsavedChangesCommand = new OpenDialogueCommand(typeof(UnsavedChangesDialogue), _dialogueResult);
        }

        public AssetViewModel(LabURI asset) : this(asset, null)
        {
        }

        public AssetViewModel(LabURI asset, AssetViewModel? parent) : this()
        {
            _asset = AssetManager.Get().GetAsset(asset);
            _parent = parent;
            // Personally, I am against ever using type checks but in this situation it's acceptable
            if (_asset is Folder folder)
            {
                // Build the tree
                var myChildren = folder.GetData().To<FolderData>().Children;
                var cList = (from child in myChildren
                             orderby _asset.Order
                             let c = AssetManager.Get().GetAsset(child)
                             select c).ToList();
                _children = new ObservableCollection<AssetViewModel>(
                    (from child in myChildren
                     orderby _asset.Order
                     let c = AssetManager.Get().GetAsset(child)
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

        public override void Save(object? o)
        {
            Directory.SetCurrentDirectory("assets");
            _asset.Serialize();
            IsDirty = false;
            Directory.SetCurrentDirectory(ProjectManagerSingleton.PM.OpenedProject!.ProjectPath);
            if (_internalChildren != null)
            {
                foreach (var c in _internalChildren)
                {
                    if (c.IsDirty || c.Asset is Folder)
                    {
                        c.Save(o);
                    }
                }
            }
        }

        public Control GetEditor()
        {
            if (_editor == null)
            {
                LoadData();
                _editor = (Control)Activator.CreateInstance(Asset.GetEditorType(), this)!;
                _editor.Unloaded += EditorUnload;
            }
            return _editor;
        }

        public Control GetEditor(CommandManager commandManager)
        {
            if (_editor == null)
            {
                LoadData();
                _editor = (Control)Activator.CreateInstance(Asset.GetEditorType(), this, commandManager)!;
            }
            return _editor;
        }

        public Control GetEditor(TabControl tabContainer)
        {
            if (_editor == null)
            {
                LoadData();
                var baseEdit = (BaseEditor)Activator.CreateInstance(Asset.GetEditorType(), this)!;
                _editor = new TabItem
                {
                    Content = baseEdit
                };
                var closableTab = new ClosableTab(Alias, tabContainer, _editor);
                closableTab.CloseTab += EditorUnload;
                closableTab.CloseTab += baseEdit.CloseEditor;
                closableTab.UndoTab += baseEdit.UndoExecuted;
                closableTab.RedoTab += baseEdit.RedoExecuted;
                closableTab.SaveTab += baseEdit.SaveExecuted;
                ((TabItem)_editor).Header = closableTab;
            }
            return _editor;
        }

        private void EditorUnload(Object? sender, EventArgs e)
        {
            if (IsDirty)
            {
                _unsavedChangesCommand.Execute();
                if (_dialogueResult.Result == null) return;

                var result = MiscUtils.ConvertEnum<UnsavedChangesDialogue.AnswerResult>(_dialogueResult.Result);
                switch (result)
                {
                    case UnsavedChangesDialogue.AnswerResult.YES:
                        Save(null);
                        break;
                    case UnsavedChangesDialogue.AnswerResult.DISCARD:
                        IsDirty = false;
                        break;
                    case UnsavedChangesDialogue.AnswerResult.CANCEL:
                    default:
                        return;
                }
            }
            UnloadData();
            // Unload tab
            if (sender is ClosableTab closeTab)
            {
                var close = new CloseTabCommand(closeTab.Container, closeTab.TabParent);
                close.Execute();
            }
        }

        protected virtual void LoadData()
        {
        }

        protected virtual void UnloadData()
        {
            _asset.DisposeData();
            _editor = null;
        }

        public ObservableCollection<AssetViewModel>? Children
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

        public List<AssetViewModel>? GetInternalChildren()
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
                    NotifyChange();
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
                    NotifyChange();
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
                    NotifyChange();
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
                    NotifyChange();
                }
            }
        }

        public bool IsDirty
        {
            get { return _dirty; }
            set
            {
                if (value != _dirty)
                {
                    _dirty = value;
                    NotifyChange();
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
