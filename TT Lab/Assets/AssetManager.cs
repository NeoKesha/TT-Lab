using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TT_Lab.AssetData;
using TT_Lab.Project;

namespace TT_Lab.Assets
{
    /// <summary>
    /// Manages the assets and wraps the getters in a type safe package for obtaining them for the currently opened project
    /// </summary>
    public class AssetManager
    {
        private AssetStorage _assets = new();
        private GuidManager _guidManager = new();

        public AssetManager(Dictionary<Guid, IAsset> assets)
        {
            _guidManager.InitMappers(assets);

            foreach (var ass in assets)
            {
                // Unlike AddAsset we allow duplicate keys and instead generate a Variation for the asset
                if (_assets.ContainsKey(ass.Value.URI))
                {
                    ass.Value.Variation = ass.Value.UUID.ToString();
                    ass.Value.RegenerateURI();
                }
                _assets.Add(ass.Value.URI, ass.Value);
            }
        }

        public void AddAsset(LabURI uri, IAsset asset)
        {
            if (_assets.ContainsKey(uri))
            {
                Log.WriteLine($"WARNING: Attempted to add already existing asset at {uri}! The asset was not added.");
                return;
            }

            _assets.Add(uri, asset);
        }

        public void AddAsset(IAsset asset)
        {
            if (_assets.ContainsKey(asset.URI))
            {
                Log.WriteLine($"WARNING: Attempted to add already existing asset at {asset.URI}! The asset was not added.");
                return;
            }

            _assets.Add(asset.URI, asset);
        }

        public void RemoveAsset(LabURI uri)
        {
            if (!_assets.ContainsKey(uri))
            {
                Log.WriteLine($"WARNING: Unable to remove unexisting asset {uri}!");
                return;
            }

            _assets.Remove(uri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>AssetManager for the currently opened project</returns>
        public static AssetManager Get()
        {
            return ProjectManagerSingleton.PM.OpenedProject.AssetManager;
        }

        /// <summary>
        /// Obtain a URI explicitly by specifying the package, subpackage, folder, variant and id
        /// </summary>
        /// <param Name="package">Package</param>
        /// <param Name="subpackage">Subpackage</param>
        /// <param Name="folder">Folder</param>
        /// <param Name="variant">Variant if present</param>
        /// <param Name="id">ID</param>
        /// <returns>Returns a URI</returns>
        public LabURI GetUri(String package, String subpackage, String folder, String? variant, uint id)
        {
            var variantString = "";
            if (variant != null)
            {
                variantString = $"/{variant}";
            }
            return GetUri($"{package}/{subpackage}/{folder}/{id}{variantString}");
        }

        // Disallow obtaining URIs by pure strings
        private LabURI GetUri(string uri)
        {
            var uriPath = $"res://{uri}";
            return _assets.ContainsKey((LabURI)uriPath) ? _assets[(LabURI)uriPath].URI : LabURI.Empty;
        }

        /// <summary>
        /// Gets URI by asset's GUID
        /// </summary>
        /// <param Name="assetID">Asset's unique GUID</param>
        /// <returns>Asset's URI</returns>
        public LabURI GetUri(Guid assetID)
        {
            return _guidManager.GetLabUriByGuid(assetID);
        }

        /// <summary>
        /// Get asset by its package, subpackage, folder, variant(if needed) and id
        /// </summary>
        /// <param Name="package"></param>
        /// <param Name="subpackage"></param>
        /// <param Name="folder"></param>
        /// <param Name="variant"></param>
        /// <param Name="id"></param>
        /// <returns>Any asset</returns>
        public IAsset GetAsset(String package, String subpackage, String folder, String? variant, uint id)
        {
            return _assets[GetUri(package, subpackage, folder, variant, id)];
        }

        /// <summary>
        /// Get asset by URI
        /// </summary>
        /// <param Name="labURI">Asset's URI</param>
        /// <returns>Any asset</returns>
        public IAsset GetAsset(LabURI labURI)
        {
            return _assets[labURI];
        }

        /// <summary>
        /// Get asset by GUID
        /// </summary>
        /// <param Name="assetID">Asset's unique GUID</param>
        /// <returns>Any asset</returns>
        public IAsset GetAsset(Guid assetID)
        {
            return _assets[_guidManager.GetLabUriByGuid(assetID)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam Name="T">Specific asset type</typeparam>
        /// <param Name="package"></param>
        /// <param Name="subpackage"></param>
        /// <param Name="folder"></param>
        /// <param Name="variant"></param>
        /// <param Name="id"></param>
        /// <returns>Asset of a specific type</returns>
        /// <seealso cref="GetAsset(string, string, string, string?, uint)"/>
        public T GetAsset<T>(String package, String subpackage, String folder, String? variant, uint id) where T : IAsset
        {
            return (T)GetAsset(package, subpackage, folder, variant, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam Name="T">Specific asset type</typeparam>
        /// <param Name="labURI"></param>
        /// <returns>Asset of a specific type</returns>
        /// <seealso cref="GetAsset(LabURI)"/>
        public T GetAsset<T>(LabURI labURI) where T : IAsset
        {
            return (T)GetAsset(labURI);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam Name="T">Specific asset type</typeparam>
        /// <param Name="assetID"></param>
        /// <returns>Asset of a specific type</returns>
        /// <seealso cref="GetAsset(Guid)"/>
        public T GetAsset<T>(Guid assetID) where T : IAsset
        {
            return (T)GetAsset(assetID);
        }

        /// <summary>
        /// Get asset data by its package, subpackage, folder, variant(if needed) and id
        /// </summary>
        /// <typeparam Name="T">Specific asset data type</typeparam>
        /// <param Name="package"></param>
        /// <param Name="subpackage"></param>
        /// <param Name="folder"></param>
        /// <param Name="variant"></param>
        /// <param Name="id"></param>
        /// <returns>Data of the asset of a specified type</returns>
        public T GetAssetData<T>(String package, String subpackage, String folder, String? variant, uint id) where T : AbstractAssetData
        {
            return GetAsset(package, subpackage, folder, variant, id).GetData<T>();
        }

        /// <summary>
        /// Get asset data by its URI
        /// </summary>
        /// <typeparam Name="T">Specific asset data type</typeparam>
        /// <param Name="labURI"></param>
        /// <returns>Data of the asset of a specified type</returns>
        public T GetAssetData<T>(LabURI labURI) where T : AbstractAssetData
        {
            return GetAsset(labURI).GetData<T>();
        }

        /// <summary>
        /// Get asset data by its unique GUID
        /// </summary>
        /// <typeparam Name="T">Specific asset data type</typeparam>
        /// <param Name="assetID"></param>
        /// <returns>Data of the asset of a specified type</returns>
        public T GetAssetData<T>(Guid assetID) where T : AbstractAssetData
        {
            return GetAsset(assetID).GetData<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All the currently stored assets</returns>
        public ImmutableList<IAsset> GetAssets() { return _assets.Values.Distinct().ToImmutableList(); }

        private class GuidManager
        {

            public Dictionary<Guid, LabURI> GuidToLabUri { get; set; } = new();

            public void InitMappers(Dictionary<Guid, IAsset> Assets)
            {
                foreach (var key in Assets.Keys)
                {
                    IAsset asset = Assets[key];
                    try
                    {
                        GuidToLabUri.Add(key, asset.URI);
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLine($"Error initializing mapper: {ex.Message} for {asset.Type} ID {asset.ID}");
                    }
                }
            }

            public LabURI GetLabUriByGuid(Guid id) { return GuidToLabUri.ContainsKey(id) ? GuidToLabUri[id] : LabURI.Empty; }
        }
    }
}
