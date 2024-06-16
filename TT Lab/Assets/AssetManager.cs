﻿using Caliburn.Micro;
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
        private AssetStorage _importAssets = new();

        // TODO: Check if locks are required to access assets for thread-safety
        //private object assetAccessLock = new object();

        public AssetManager() { }

        public AssetManager(Dictionary<LabURI, IAsset> assets)
        {
            AddAllAssets(assets);
        }

        public void AddAllAssets(Dictionary<LabURI, IAsset> assets)
        {
            foreach (var ass in assets)
            {
                _assets.Add(ass.Key, ass.Value);
            }
        }

        /// <summary>
        /// Adds the asset to the manager with a specified URI
        /// </summary>
        /// <param name="uri">Asset's unique resource identifier</param>
        /// <param name="asset">Asset to add</param>
        /// <remarks>
        /// THIS METHOD SHOULD ONLY BE USED WHEN YOU ARE SURE THAT DUPLICATES ARE SKIPPED INTENTIONALLY.
        /// AS THIS SKIPS LOGGING A WARNING INTO A LOG CONSOLE OF TT Lab
        /// </remarks>
        public void AddAssetUnsafe(LabURI uri, IAsset asset)
        {
            if (_assets.ContainsKey(uri))
            {
                return;
            }

            _assets.Add(uri, asset);
        }

        /// <summary>
        /// Adds the asset to the manager with a specified URI
        /// </summary>
        /// <param name="uri">Asset's unique resource identifier</param>
        /// <param name="asset">Asset to add</param>
        public void AddAsset(LabURI uri, IAsset asset)
        {
            if (_assets.ContainsKey(uri))
            {
                Log.WriteLine($"WARNING: Attempted to add already existing asset at {uri}! The asset was not added.");
                return;
            }

            _assets.Add(uri, asset);
        }

        /// <summary>
        /// Adds the asset to the manager
        /// </summary>
        /// <param name="asset">Asset to add</param>
        public void AddAsset(IAsset asset)
        {
            AddAsset(asset.URI, asset);
        }

        /// <summary>
        /// Only use during the project's creation stage to delay import of embedded assets in other assets.
        /// <para>Adds an asset to delayed import stage</para>
        /// </summary>
        /// <param name="asset">Asset to add</param>
        public void AddAssetToImport(IAsset asset)
        {
            _importAssets.Add(asset.URI, asset);
        }

        /// <summary>
        /// Removes the asset from the manager
        /// </summary>
        /// <param name="uri">Asset's unique resource identifier</param>
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
            return IoC.Get<ProjectManager>().OpenedProject!.AssetManager;
        }

        /// <summary>
        /// Obtain a URI explicitly by specifying the package, folder, variant(if needed) and id
        /// </summary>
        /// <param Name="package">Package</param>
        /// <param Name="subpackage">Subpackage</param>
        /// <param Name="folder">Folder</param>
        /// <param Name="variant">Variant if present</param>
        /// <param Name="id">ID</param>
        /// <returns>Found URI.<para/><code>LabURI.Empty</code> if URI was not found</returns>
        public LabURI GetUri(LabURI package, String folder, String? variant, UInt32 id)
        {
            var variantString = variant != null ? $"/{variant}" : "";

            // First with try to find the URI with its variant
            // If that fails we attempt to find its no variant counterpart
            // Finally we do the same but walk through every dependency the package has
            var resultUri = GetUri($"{package}/{folder}/{id}{variantString}");
            if (resultUri == LabURI.Empty && variant != null)
            {
                resultUri = GetUri($"{package}/{folder}/{id}");
            }
            if (resultUri == LabURI.Empty)
            {
                var packageAsset = GetAsset<Package>(package);
                foreach (var dependency in packageAsset.Dependencies)
                {
                    var dependencyAsset = GetAsset<Package>(dependency);
                    resultUri = GetUri($"{dependency}/{folder}/{id}{variantString}");
                    if (resultUri == LabURI.Empty)
                    {
                        var dependencyVariantString = dependencyAsset.Variant.Length == 0 ? "" : $"/{dependencyAsset.Variant}";
                        resultUri = GetUri($"{dependency}/{folder}/{id}{dependencyVariantString}");
                    }
                }
            }
            return resultUri;
        }

        /// <summary>
        /// Obtain a URI explicitly by specifying the package, folder, variant(if needed), layoutId(if needed) and id
        /// </summary>
        /// <param name="package"></param>
        /// <param name="folder"></param>
        /// <param name="variant"></param>
        /// <param name="layoutId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public LabURI GetUri(LabURI package, String folder, String? variant, Int32? layoutId, UInt32 id)
        {
            if (layoutId == null)
            {
                return GetUri(package, folder, variant, id);
            }
            var resultVariant = variant == null ? $"{layoutId}" : $"{variant}/{layoutId}";
            return GetUri(package, folder, resultVariant, id);
        }

        // Disallow obtaining URIs by pure strings publically
        private LabURI GetUri(string uri)
        {
            return _assets.ContainsKey((LabURI)uri) ? _assets[(LabURI)uri].URI : LabURI.Empty;
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
        public IAsset GetAsset(LabURI package, String folder, String? variant, uint id)
        {
            return _assets[GetUri(package, folder, variant, id)];
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
        /// 
        /// </summary>
        /// <typeparam Name="T">Specific asset type</typeparam>
        /// <param Name="package"></param>
        /// <param Name="subpackage"></param>
        /// <param Name="folder"></param>
        /// <param Name="variant"></param>
        /// <param Name="id"></param>
        /// <returns>Asset of a specific type</returns>
        /// <seealso cref="GetAsset(LabURI, string, string?, uint)"/>
        public T GetAsset<T>(LabURI package, String folder, String? variant, uint id) where T : IAsset
        {
            return (T)GetAsset(package, folder, variant, id);
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
        /// Get asset data by its package, subpackage, folder, variant(if needed) and id
        /// </summary>
        /// <typeparam Name="T">Specific asset data type</typeparam>
        /// <param Name="package"></param>
        /// <param Name="subpackage"></param>
        /// <param Name="folder"></param>
        /// <param Name="variant"></param>
        /// <param Name="id"></param>
        /// <returns>Data of the asset of a specified type</returns>
        public T GetAssetData<T>(LabURI package, String folder, String? variant, uint id) where T : AbstractAssetData
        {
            return GetAsset(package, folder, variant, id).GetData<T>();
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
        /// 
        /// </summary>
        /// <returns>All the currently stored assets</returns>
        public ImmutableList<IAsset> GetAssets() { return _assets.Values.Distinct().ToImmutableList(); }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All assets queued for delayed import stage</returns>
        public ImmutableList<IAsset> GetAssetsToImport() { var immut = _importAssets.Values.Distinct().ToImmutableList(); _importAssets.Clear(); return immut; }
    }
}
