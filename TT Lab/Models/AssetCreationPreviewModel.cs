using System;
using TT_Lab.Assets;

namespace TT_Lab.Models;

public class AssetCreationPreviewModel
{
    private readonly IAsset _asset;

    public AssetCreationPreviewModel(IAsset assetToPreview, string displayName, Action<IAsset>? createCallback = null)
    {
        _asset = assetToPreview;
        DisplayName = displayName;
        CreateCallback = createCallback;
    }
    
    public string IconPath => $"/Media/LabIcons/{_asset.IconPath}";
    public string DisplayName { get; }
    public Action<IAsset>? CreateCallback { get; }
    public Type AssetType => _asset.GetType();
}