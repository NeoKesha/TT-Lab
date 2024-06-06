using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Editors;
using TT_Lab.ViewModels.Interfaces;

namespace TT_Lab.ViewModels
{
    public abstract class ResourceEditorViewModel : Conductor<IScreen>.Collection.AllActive, IEditorViewModel
    {
        public LabURI EditableResource { get; set; } = LabURI.Empty;
        public Boolean IsDirty { get; set; } = false;

        protected List<SceneEditorViewModel> Scenes { get; set; } = new();

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

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            foreach (var scene in Scenes)
            {
                ActivateItemAsync(scene, cancellationToken);
            }

            return base.OnActivateAsync(cancellationToken);
        }

        protected override Task OnDeactivateAsync(Boolean close, CancellationToken cancellationToken)
        {
            if (close)
            {
                Save();
            }

            foreach (var scene in Scenes)
            {
                DeactivateItemAsync(scene, close, cancellationToken);
            }

            return base.OnDeactivateAsync(close, cancellationToken);
        }
    }
}
