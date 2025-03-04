using TT_Lab.Assets;

namespace TT_Lab.ViewModels.ResourceTree;

public class InstanceElementViewModel : ResourceTreeElementViewModel
{
    public InstanceElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent = null) : base(asset, parent)
    {
    }

    protected override void CreateContextMenu()
    {
        
        base.CreateContextMenu();
    }
}