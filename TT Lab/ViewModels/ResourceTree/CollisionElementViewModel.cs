using TT_Lab.Assets;

namespace TT_Lab.ViewModels.ResourceTree;

public class CollisionElementViewModel : ResourceTreeElementViewModel
{
    public CollisionElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent = null) : base(asset, parent)
    {
    }

    protected override void CreateContextMenu()
    {
    }
}