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
        protected readonly Dictionary<LabURI, TabbedEditorViewModel> _activeEditors = new();

        public virtual Task CloseEditorTab(TabbedEditorViewModel editor)
        {
            return DeactivateItemAsync(editor, true);
        }

        public virtual void Save()
        {
            foreach (var item in _activeEditors)
            {
                item.Value.ActiveItem.SaveChanges();
            }
        }

        protected override Task OnDeactivateAsync(Boolean close, CancellationToken cancellationToken)
        {
            DeactivateItemAsync(ActiveItem, close, cancellationToken);

            if (_activeEditors.Count > 0)
            {
                if (!close)
                {
                    _activeEditors.Remove(_activeEditors.Where(kv => kv.Value == ActiveItem).First().Key);
                }
                else
                {
                    _activeEditors.Clear();
                }
            }

            GC.Collect();
            return base.OnDeactivateAsync(close, cancellationToken);
        }
    }
}
