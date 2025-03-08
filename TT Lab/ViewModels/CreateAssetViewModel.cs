using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using TT_Lab.Assets;
using TT_Lab.Models;
using TT_Lab.Project;
using TT_Lab.ServiceProviders;
using TT_Lab.Services;
using TT_Lab.ViewModels.ResourceTree;

namespace TT_Lab.ViewModels;

public class CreateAssetViewModel : Screen, INotifyDataErrorInfo
{
    private string _assetName = "NewAsset";
    private AssetCreationPreviewModel _selectedCreationModel;
    private FolderElementViewModel _selectedFolder;
    private IDataValidatorService _dataValidatorService;
    
    const Int32 ASSET_NAME_LIMIT = 64;
    const String ASSET_NAME_INVALID_CHARS_ERROR = "Asset name must not contain invalid characters";
    const String ASSET_NAME_EMPTY_ERROR = "Asset name must not be empty";
    const String ASSET_NAME_TOO_LONG_ERROR = "Asset name must be less than 64 characters long";
    const String ASSET_NAME_ALREADY_EXISTS = "Asset with the same name already exists";

    public CreateAssetViewModel(IDataValidatorService dataValidatorService)
    {
        _dataValidatorService = dataValidatorService;
        _dataValidatorService.RegisterProperty<string>(nameof(AssetName), IsAssetNameValid);
    }

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
        newAsset.Name = _assetName.Trim();
        newAsset.Alias = _assetName.Trim();
        var parent = _selectedFolder;
        var folder = parent.GetAsset<Folder>();
        newAsset.Package = folder.Package;
        // Newly created assets will have their project's variation
        newAsset.Variation = IoC.Get<ProjectManager>().OpenedProject!.BasePackage.ID.ToString();
        newAsset.ID = TwinIdGeneratorServiceProvider.GetGenerator(SelectedCreationModel.AssetType).GenerateTwinId();
        newAsset.RegenerateLinks(true);
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
        
        parent.AddNewChild(newAsset.GetResourceTreeElement(parent));
        parent.ClearChildren();
        parent.LoadChildrenBack();
        parent.NotifyOfPropertyChange(nameof(parent.Children));
        
        return TryCloseAsync();
    }

    private Boolean IsAssetNameValid(string name)
    {
        var isValid = true;

        if (String.IsNullOrEmpty(name.Trim()))
        {
            _dataValidatorService.AddError(nameof(AssetName), ASSET_NAME_EMPTY_ERROR);
            isValid = false;
        }
        else
        {
            _dataValidatorService.RemoveError(nameof(AssetName), ASSET_NAME_EMPTY_ERROR);
        }

        if (name.Length > ASSET_NAME_LIMIT)
        {
            _dataValidatorService.AddError(nameof(AssetName), ASSET_NAME_TOO_LONG_ERROR);
            isValid = false;
        }
        else
        {
            _dataValidatorService.RemoveError(nameof(AssetName), ASSET_NAME_TOO_LONG_ERROR);
        }

        if (name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
        {
            _dataValidatorService.AddError(nameof(AssetName), ASSET_NAME_INVALID_CHARS_ERROR);
            isValid = false;
        }
        else
        {
            _dataValidatorService.RemoveError(nameof(AssetName), ASSET_NAME_INVALID_CHARS_ERROR);
        }
        
        var assetsOfType = AssetManager.Get().GetAllAssetsOf(SelectedCreationModel.AssetType);
        if (assetsOfType.Any(asset => asset.Name == name))
        {
            _dataValidatorService.AddError(nameof(AssetName), ASSET_NAME_ALREADY_EXISTS);
            isValid = false;
        }
        else
        {
            _dataValidatorService.RemoveError(nameof(AssetName), ASSET_NAME_ALREADY_EXISTS);
        }

        return isValid;
    }

    protected override void OnViewReady(object view)
    {
        base.OnViewReady(view);

        SelectedCreationModel = CreatableAssets[0];
        _dataValidatorService.ValidateProperty(AssetName, nameof(AssetName));
    }

    public BindableCollection<AssetCreationPreviewModel> CreatableAssets { get; set; } = new();

    public string AssetName
    {
        get => _assetName;
        set
        {
            _dataValidatorService.ValidateProperty(value);
            _assetName = value;
            NotifyOfPropertyChange();
            NotifyOfPropertyChange(nameof(CanCreate));
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

    public IEnumerable GetErrors(string? propertyName)
    {
        return _dataValidatorService.GetErrors(propertyName);
    }

    public bool CanCreate => !HasErrors;

    public bool HasErrors => _dataValidatorService.HasErrors;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged
    {
        add => _dataValidatorService.ErrorsChanged += value;
        remove => _dataValidatorService.ErrorsChanged -= value;
    }
}