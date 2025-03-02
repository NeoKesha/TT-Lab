using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using TT_Lab.Assets;
using TT_Lab.Models;
using TT_Lab.ServiceProviders;
using TT_Lab.ViewModels.ResourceTree;

namespace TT_Lab.ViewModels;

public class CreateAssetViewModel : Screen
{
    private string _assetName = "NewAsset";
    private AssetCreationPreviewModel _selectedCreationModel;
    private FolderElementViewModel _selectedFolder;
    
    public void RegisterAssetToCreate<T>(string displayName, Func<IAsset, AssetCreationStatus>? createCallback = null) where T : IAsset, new()
    {
        CreatableAssets.Add(AssetCreationPreviewModelFactory.CreatePreview<T>(displayName, createCallback));
    }

    public void AssignFolder(FolderElementViewModel folder)
    {
        _selectedFolder = folder;
    }

    public Task CreateAssetButton()
    {
        var newAsset = (IAsset)Activator.CreateInstance(SelectedCreationModel.AssetType)!;
        newAsset.Name = _assetName;
        newAsset.Alias = _assetName;
        var parent = _selectedFolder;
        var folder = parent.GetAsset<Folder>();
        newAsset.Package = folder.Package;
        newAsset.ID = TwinIdGeneratorServiceProvider.GetGenerator(SelectedCreationModel.AssetType).GenerateTwinId();
        newAsset.RegenerateLinks(false);
        var creationResult = SelectedCreationModel.CreateCallback?.Invoke(newAsset);
        if (creationResult is AssetCreationStatus.Failed)
        {
            Log.WriteLine("Error: Failed to create asset");
            return Task.CompletedTask;
        }
        
        folder.AddChild(newAsset);
        AssetManager.Get().AddAsset(newAsset);
        newAsset.Serialize(SerializationFlags.SetDirectoryToAssets | SerializationFlags.SaveData);
        folder.Serialize(SerializationFlags.SetDirectoryToAssets | SerializationFlags.SaveData | SerializationFlags.FixReferences);
        Log.WriteLine($"Saved new asset {newAsset.Name}");
        Log.WriteLine($"Saved new asset in {folder.Name}");
        var task = newAsset.GetResourceTreeElement(parent);
        task.Wait();
        parent.AddNewChild(task.Result);
        parent.ClearChildren();
        parent.LoadChildrenBack();

        return TryCloseAsync();
    }

    protected override void OnViewReady(object view)
    {
        base.OnViewReady(view);

        SelectedCreationModel = CreatableAssets[0];
    }

    public BindableCollection<AssetCreationPreviewModel> CreatableAssets { get; set; } = new();

    public string AssetName
    {
        get => _assetName;
        set
        {
            if (_assetName != value)
            {
                _assetName = value;
                NotifyOfPropertyChange();
            }
        }
    }

    public AssetCreationPreviewModel SelectedCreationModel
    {
        get => _selectedCreationModel;
        set
        {
            _selectedCreationModel = value;
            _assetName = $"New {_selectedCreationModel.DisplayName}";
            NotifyOfPropertyChange();
            NotifyOfPropertyChange(nameof(AssetName));
        }
    }
    
}