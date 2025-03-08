using TT_Lab.Assets;

namespace TT_Lab.ViewModels.ResourceTree;

public class DynamicSceneryElementViewModel : ResourceTreeElementViewModel
{
    public DynamicSceneryElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent = null) : base(asset, parent)
    {
    }

    protected override void CreateContextMenu()
    {
    }
}