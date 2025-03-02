using System;
using TT_Lab.Assets;

namespace TT_Lab.Models;

public class AssetCreationPreviewModel
{
    private readonly IAsset _asset;

    public AssetCreationPreviewModel(IAsset assetToPreview, string displayName)
    {
        _asset = assetToPreview;
        DisplayName = displayName;
    }
    
    public string IconPath => $"/Media/LabIcons/{_asset.IconPath}";
    public string DisplayName { get; }
    public Type AssetType => _asset.GetType();
}