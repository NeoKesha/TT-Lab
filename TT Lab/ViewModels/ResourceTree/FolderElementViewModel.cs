using System.Threading.Tasks;
using Caliburn.Micro;
using TT_Lab.AssetData;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Graphics;

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

    private void DefaultCreatableAssets(CreateAssetViewModel createAssetViewModel)
    {
        createAssetViewModel.RegisterAssetToCreate<Folder>("Folder", asset =>
        {
            var folderData = asset.GetData<FolderData>();
            folderData.Parent = Asset.URI;
        });
    }

    protected virtual void ListCreatableAssets(CreateAssetViewModel createAssetViewModel)
    {
        createAssetViewModel.RegisterAssetToCreate<SoundEffect>("Sound Effect");
        createAssetViewModel.RegisterAssetToCreate<Texture>("Texture");
        createAssetViewModel.RegisterAssetToCreate<Skydome>("Skydome");
        createAssetViewModel.RegisterAssetToCreate<GameObject>("Game Object");
        createAssetViewModel.RegisterAssetToCreate<BehaviourStarter>("Behaviour");
    }

    private void CreateItem()
    {
        var assetCreatorDialogue = IoC.Get<CreateAssetViewModel>();
        DefaultCreatableAssets(assetCreatorDialogue);
        ListCreatableAssets(assetCreatorDialogue);
        assetCreatorDialogue.AssignFolder(this);
        IoC.Get<IWindowManager>().ShowDialogAsync(assetCreatorDialogue);
    }
}