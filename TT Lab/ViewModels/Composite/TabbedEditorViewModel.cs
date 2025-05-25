using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Interfaces;

namespace TT_Lab.ViewModels.Composite
{
    public class TabbedEditorViewModel : Conductor<IEditorViewModel>
    {
        private LabURI _editableResource;
        private Type _editorType;

        public TabbedEditorViewModel(LabURI editableResource, Type editorType)
        {
            _editableResource = editableResource;
            _editorType = editorType;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var editor = (IEditorViewModel)IoC.GetInstance(EditorType, null);
            editor.EditableResource = EditableResource;
            return ActivateItemAsync(editor, cancellationToken);
        }

        public override Task<Boolean> CanCloseAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return ActiveItem.CanCloseAsync(cancellationToken);
        }

        public LabURI EditableResource
        {
            get => _editableResource;
            set => _editableResource = value;
        }
        
        public String IconPath => $"/Media/LabIcons/{AssetManager.Get().GetAsset(EditableResource).IconPath}";

        public Type EditorType
        {
            get => _editorType;
            set => _editorType = value;
        }
    }
}
