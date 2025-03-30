using TT_Lab.ViewModels.Composite;

namespace TT_Lab.ViewModels.Editors.Instance;

public abstract class ViewportEditableInstanceViewModel : InstanceSectionResourceEditorViewModel
{
    public Vector4ViewModel Position { get; protected set; } = new();

    public Vector3ViewModel Rotation { get; protected set; } = new();

    public Vector3ViewModel Scale { get; protected set; } = new();
}