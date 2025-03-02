using System;
using Caliburn.Micro;
using TT_Lab.Assets;
using TT_Lab.Models;
using TT_Lab.ViewModels.ResourceTree;

namespace TT_Lab.ViewModels;

public class CreateAssetViewModel : Screen
{
    private string _assetName = "NewAsset";
    private AssetCreationPreviewModel _selectedCreationModel;
    
    public void RegisterAssetToCreate<T>(string displayName) where T : IAsset, new()
    {
        CreatableAssets.Add(AssetCreationPreviewModelFactory.CreatePreview<T>(displayName));
    }

    public void CreateAssetButton()
    {
        var newAsset = (IAsset)Activator.CreateInstance(SelectedCreationModel.AssetType)!;
        var parent = (FolderElementViewModel)Parent;
        var folder = parent.GetAsset<Folder>();
        folder.AddChild(newAsset);
        var task = newAsset.GetResourceTreeElement(parent);
        task.Wait();
        parent.AddChild(task.Result);
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