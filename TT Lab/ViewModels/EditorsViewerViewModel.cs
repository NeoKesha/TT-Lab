using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Composite;

namespace TT_Lab.ViewModels
{
    public abstract class EditorsViewerViewModel : Conductor<TabbedEditorViewModel>.Collection.OneActive
    {
        protected readonly Dictionary<LabURI, TabbedEditorViewModel> ActiveEditors = new();

        public virtual Task CloseEditorTab(TabbedEditorViewModel editor)
        {
            RemoveCurrentActiveEditor();
            
            return DeactivateItemAsync(editor, true);
        }

        public virtual void SaveEditorTab(TabbedEditorViewModel editor)
        {
            editor.ActiveItem.SaveChanges();
        }

        public override async Task<Boolean> CanCloseAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = true;
            foreach (var editor in ActiveEditors.Values)
            {
                result = await editor.ActiveItem.CanCloseAsync(cancellationToken);
                if (!result)
                {
                    break;
                }
            }
            
            return result;
        }

        public virtual void Save()
        {
            foreach (var item in ActiveEditors)
            {
                item.Value.ActiveItem.SaveChanges();
            }
        }

        protected void RemoveCurrentActiveEditor()
        {
            if (ActiveEditors.Count <= 0)
            {
                return;
            }
            
            if (ActiveEditors.FirstOrDefault(kv => kv.Value == ActiveItem,
                    new KeyValuePair<LabURI, TabbedEditorViewModel>(LabURI.Empty, null)).Key == LabURI.Empty)
            {
                return;
            }
                
            ActiveEditors.Remove(ActiveEditors.First(kv => kv.Value == ActiveItem).Key);
        }

        protected override Task OnDeactivateAsync(Boolean close, CancellationToken cancellationToken)
        {
            DeactivateItemAsync(ActiveItem, close, cancellationToken);

            if (ActiveEditors.Count > 0)
            {
                if (!close)
                {
                    if (ActiveEditors.FirstOrDefault(kv => kv.Value == ActiveItem,
                            new KeyValuePair<LabURI, TabbedEditorViewModel>(LabURI.Empty, null)).Key != LabURI.Empty)
                    {
                        ActiveEditors.Remove(ActiveEditors.First(kv => kv.Value == ActiveItem).Key);
                    }
                }
                else
                {
                    ActiveEditors.Clear();
                }
            }

            GC.Collect();
            return base.OnDeactivateAsync(close, cancellationToken);
        }
    }
}
