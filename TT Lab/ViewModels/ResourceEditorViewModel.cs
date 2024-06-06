using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Interfaces;

namespace TT_Lab.ViewModels
{
    public abstract class ResourceEditorViewModel : Conductor<IScreen>.Collection.AllActive, IEditorViewModel
    {
        public LabURI EditableResource { get; set; } = LabURI.Empty;
        public Boolean IsDirty { get; set; } = false;

        public void SaveChanges()
        {
            Save();
            var asset = AssetManager.Get().GetAsset(EditableResource);
            asset.Serialize();
        }

        protected abstract void Save();
        public abstract void LoadData();

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(LoadData, cancellationToken);
        }

        protected override Task OnDeactivateAsync(Boolean close, CancellationToken cancellationToken)
        {
            if (close)
            {
                Save();
            }

            return base.OnDeactivateAsync(close, cancellationToken);
        }
    }
}
