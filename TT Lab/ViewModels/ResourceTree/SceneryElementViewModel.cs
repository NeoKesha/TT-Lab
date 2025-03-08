using TT_Lab.Assets;

namespace TT_Lab.ViewModels.ResourceTree;

public class SceneryElementViewModel : ResourceTreeElementViewModel
{
    public SceneryElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent = null) : base(asset, parent)
    {
    }

    protected override void CreateContextMenu()
    {
    }
}