using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using TT_Lab.AssetData;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.Project;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Editors;
using TT_Lab.ViewModels.Interfaces;

namespace TT_Lab.ViewModels
{
    public abstract class ResourceEditorViewModel : Conductor<IScreen>.Collection.AllActive, IEditorViewModel, IDirtyMarker
    {
        protected readonly DirtyTracker DirtyTracker;
        
        public LabURI EditableResource { get; set; } = LabURI.Empty;

        public void ResetDirty()
        {
            DirtyTracker.ResetDirty();
            NotifyOfPropertyChange(nameof(IsDirty));
        }

        public Boolean IsDirty => DirtyTracker.IsDirty;

        private Boolean _startedEditing;
        private Boolean _usingConfirmClose = false;

        protected List<SceneEditorViewModel> Scenes { get; set; } = new();
        protected Boolean IsDataLoaded { get; private set; }
        
        private ICommand _unsavedChangesCommand;
        private OpenDialogueCommand.DialogueResult _dialogueResult = new();
        private string tabDisplayName = string.Empty;

        public ResourceEditorViewModel()
        {
            _unsavedChangesCommand = new OpenDialogueCommand(() => new UnsavedChangesDialogue(_dialogueResult, AssetManager.Get().GetAsset(EditableResource).GetResourceTreeElement()));
            DirtyTracker = new DirtyTracker(this, () =>
            {
                EditorChangesHappened();
                NotifyOfPropertyChange(nameof(IsDirty));
            });
        }
        
        public override Task<Boolean> CanCloseAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (IsDirty)
            {
                _usingConfirmClose = true;
                _unsavedChangesCommand.Execute();
            }

            return Task.Factory.StartNew(() =>
            {
                if (!IsDirty)
                {
                    return true;
                }
            
                if (_dialogueResult.Result == null)
                {
                    return false;
                }
            
                var result = MiscUtils.ConvertEnum<UnsavedChangesDialogue.AnswerResult>(_dialogueResult.Result);
                switch (result)
                {
                    case UnsavedChangesDialogue.AnswerResult.YES:
                    case UnsavedChangesDialogue.AnswerResult.DISCARD:
                        return true;
                    case UnsavedChangesDialogue.AnswerResult.CANCEL:
                    default:
                        return false;
                }
            }, cancellationToken);
        }

        public void SaveChanges()
        {
            if (!IsDirty)
            {
                return;
            }
 
            ResetDirty();
            
            if (_usingConfirmClose && (_dialogueResult.Result == null || MiscUtils.ConvertEnum<UnsavedChangesDialogue.AnswerResult>(_dialogueResult.Result) == UnsavedChangesDialogue.AnswerResult.DISCARD))
            {
                return;
            }
            
            Save();
            var asset = AssetManager.Get().GetAsset(EditableResource);
            asset.Serialize(SerializationFlags.SetDirectoryToAssets | SerializationFlags.SaveData | SerializationFlags.FixReferences);
        }

        protected virtual void Save()
        {
            ResetDirty();
        }
        
        public abstract void LoadData();
        public override void NotifyOfPropertyChange([CallerMemberName] String propertyName = null)
        {
            if (_startedEditing && propertyName != nameof(IsDirty))
            {
                DirtyTracker.MarkDirtyByProperty(this, new PropertyChangedEventArgs(propertyName));
            }
        
            base.NotifyOfPropertyChange(propertyName);
        }

        protected override void OnViewAttached(object view, object context)
        {
            LoadData();
            
            IsDataLoaded = true;
            
            base.OnViewAttached(view, context);
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            foreach (var scene in Scenes)
            {
                ActivateItemAsync(scene, cancellationToken);
            }

            return base.OnActivateAsync(cancellationToken);
        }

        protected override void OnViewReady(Object view)
        {
            base.OnViewReady(view);

            ResetDirty();
            _startedEditing = true;
            if (Parent is TabbedEditorViewModel tabbedEditorViewModel)
            {
                tabDisplayName = tabbedEditorViewModel.DisplayName;
            }
        }

        protected override Task OnDeactivateAsync(Boolean close, CancellationToken cancellationToken)
        {
            _startedEditing = false;

            foreach (var scene in Scenes)
            {
                DeactivateItemAsync(scene, close, cancellationToken);
            }

            if (close)
            {
                SaveChanges();
            }
            
            var asset = AssetManager.Get().GetAsset(EditableResource);
            if (asset.IsLoaded && close)
            {
                asset.GetData<AbstractAssetData>().Dispose();
            }

            return base.OnDeactivateAsync(close, cancellationToken);
        }

        private void EditorChangesHappened()
        {
            if (Parent is not TabbedEditorViewModel parent || !_startedEditing)
            {
                return;
            }
            
            if (IsDirty)
            {
                parent.DisplayName = tabDisplayName + "*";
            }
            else
            {
                parent.DisplayName = tabDisplayName;
            }
        }
    }
}
