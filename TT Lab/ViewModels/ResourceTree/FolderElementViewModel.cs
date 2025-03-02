using System.Threading.Tasks;
using Caliburn.Micro;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;

namespace TT_Lab.ViewModels.ResourceTree;

public class FolderElementViewModel : ResourceTreeElementViewModel
{
    public FolderElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent = null) : base(asset, parent)
    {
    }

    public override async Task Init()
    {
        await base.Init();
        Log.WriteLine($"Building Children for {Alias}...");
        await BuildChildren((Folder)Asset);
    }

    protected override void CreateContextMenu()
    {
        RegisterMenuItem(new MenuItemSettings
        {
            Header = "Create Asset",
            Action = CreateItem
        });
        
        base.CreateContextMenu();
    }

    protected virtual void ListCreatableAssets(CreateAssetViewModel createAssetViewModel)
    {
        createAssetViewModel.RegisterAssetToCreate<Folder>("Folder");
        createAssetViewModel.RegisterAssetToCreate<SoundEffect>("Sound Effect");
    }

    private void CreateItem()
    {
        var assetCreatorDialogue = IoC.Get<CreateAssetViewModel>();
        ListCreatableAssets(assetCreatorDialogue);
        IoC.Get<IWindowManager>().ShowDialogAsync(assetCreatorDialogue);
    }
}