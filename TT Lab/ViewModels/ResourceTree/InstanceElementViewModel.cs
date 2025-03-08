using TT_Lab.Assets;

namespace TT_Lab.ViewModels.ResourceTree;

public abstract class InstanceElementViewModel : ResourceTreeElementViewModel
{
    protected InstanceElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent = null) : base(asset, parent)
    {
    }

    protected override void CreateContextMenu()
    {
        RegisterMenuItem(new MenuItemSettings
        {
            Header = "Duplicate",
            Action = DuplicateInstance
        });
        base.CreateContextMenu();
    }

    protected abstract void DuplicateInstance();
}