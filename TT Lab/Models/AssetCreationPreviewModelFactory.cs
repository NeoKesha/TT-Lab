using SharpGLTF.Schema2;
using TT_Lab.Assets;

namespace TT_Lab.Models;

public static class AssetCreationPreviewModelFactory
{
    public static AssetCreationPreviewModel CreatePreview<T>(string displayName) where T : IAsset, new()
    {
        return new AssetCreationPreviewModel(new T(), displayName);
    }
}