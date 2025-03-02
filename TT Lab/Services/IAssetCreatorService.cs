using TT_Lab.Assets;

namespace TT_Lab.Services
{
    /// <summary>
    /// Provides a service to generate new assets
    /// </summary>
    public interface IAssetCreatorService
    {
        void CreateAsset<T>() where T : IAsset;
    }
}
