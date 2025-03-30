using System;
using SharpGLTF.Schema2;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;

namespace TT_Lab.Models;

public static class AssetCreationPreviewModelFactory
{
    public static AssetCreationPreviewModel CreatePreview<T>(string displayName, Func<IAsset, AssetCreationStatus>? dataCreator = null) where T : IAsset, new()
    {
        return new AssetCreationPreviewModel(new T(), displayName, dataCreator);
    }
}