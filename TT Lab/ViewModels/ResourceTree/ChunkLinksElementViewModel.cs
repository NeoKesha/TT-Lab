using TT_Lab.Assets;

namespace TT_Lab.ViewModels.ResourceTree;

public class ChunkLinksElementViewModel : ResourceTreeElementViewModel
{
    public ChunkLinksElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent = null) : base(asset, parent)
    {
    }

    protected override void CreateContextMenu()
    {
    }
}