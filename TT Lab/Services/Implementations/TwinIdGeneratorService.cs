using System;
using System.Linq;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Graphics;
using TT_Lab.ServiceProviders;

namespace TT_Lab.Services.Implementations;

public class TwinIdGeneratorService<T> : ITwinIdGeneratorService where T : IAsset
{
    public virtual UInt32 GenerateTwinId()
    {
        var currentlyRegistered = AssetManager.Get().GetAllAssetsOf<T>();
        var maxId = currentlyRegistered.Max(asset => asset.ID) + 1;
        return maxId;
    }
}

public class TwinIdGeneratorServiceFolder : TwinIdGeneratorService<Folder>
{
    public override UInt32 GenerateTwinId()
    {
        return (UInt32)Guid.NewGuid().GetHashCode();
    }
}