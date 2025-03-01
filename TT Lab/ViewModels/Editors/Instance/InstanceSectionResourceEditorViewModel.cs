using Caliburn.Micro;
using TT_Lab.ViewModels.Interfaces;

namespace TT_Lab.ViewModels.Editors.Instance
{
    public abstract class InstanceSectionResourceEditorViewModel : ResourceEditorViewModel, IHaveParentEditor<ChunkEditorViewModel>
    {
        public ChunkEditorViewModel ParentEditor { get; set; }
    }
}
