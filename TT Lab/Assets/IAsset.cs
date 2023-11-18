﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData;
using TT_Lab.Util;
using TT_Lab.ViewModels;

namespace TT_Lab.Assets
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IAsset
    {
        /// <summary>
        /// Asset's unique ID
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        Guid UUID { get; }

        /// <summary>
        /// Asset's string type
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        Type Type { get; }

        /// <summary>
        /// In-Game's ID number
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        UInt32 ID { get; set; }

        /// <summary>
        /// The main package the asset belongs to
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        LabURI Package { get; set; }

        /// <summary>
        /// In case of Twinsanity ID collisions the distinct category asset belongs to
        /// </summary>
        [JsonProperty(Required = Required.AllowNull)]
        string? Variation { get; set; }

        /// <summary>
        /// Asset's name
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        string Name { get; set; }

        /// <summary>
        /// Whether asset's data is in raw form
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        bool Raw { get; set; }

        /// <summary>
        /// Path to asset's data
        /// </summary>
        [JsonProperty(Required = Required.AllowNull)]
        string Data { get; set; }

        /// <summary>
        /// Name for the asset to display in project tree
        /// </summary>
        [JsonProperty(Required = Required.AllowNull)]
        string Alias { get; set; }

        /// <summary>
        /// Chunk path where this asset belongs to
        /// </summary>
        [JsonProperty(Required = Required.AllowNull)]
        string Chunk { get; }

        /// <summary>
        /// Resources unique URI to be accessed from around the project
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        LabURI URI { get; set; }

        /// <summary>
        /// TT Lab specific data
        /// </summary>
        [JsonProperty(Required = Required.AllowNull)]
        Dictionary<String, Object?> Parameters { get; }

        /// <summary>
        /// Order in the Project Tree
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        UInt32 Order { get; set; }

        /// <summary>
        /// For instances their Layout ID
        /// </summary>
        /// <remarks>Ranges from 0 to 7</remarks>
        [JsonProperty(Required = Required.AllowNull)]
        Int32? LayoutID { get; set; }

        /// <summary>
        /// Whether the data for this asset is currently in memory
        /// </summary>
        Boolean IsLoaded { get; }

        /// <summary>
        /// If asset shouldn't be exported during game's build stage
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Boolean SkipExport { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Asset's editor type</returns>
        Type GetEditorType();

        /// <summary>
        /// Loads in asset's data if it's not loaded
        /// </summary>
        /// <returns>Asset's data</returns>
        protected AbstractAssetData GetData();

        /// <summary>
        /// Loads in asset's data if it's not loaded
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Asset's data as a specific type</returns>
        T GetData<T>() where T : AbstractAssetData
        {
            return (T)GetData();
        }

        /// <summary>
        /// Returns asset's viewmodel for editing
        /// </summary>
        AssetViewModel GetViewModel(AssetViewModel? parent = null);

        /// <summary>
        /// Return asset's viewmodel as a specific type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        T GetViewModel<T>(AssetViewModel? parent = null) where T : AssetViewModel
        {
            return (T)GetViewModel(parent);
        }

        /// <summary>
        /// Disposes of the contained data if it was loaded
        /// </summary>
        void DisposeData()
        {
            GetData().Dispose();
        }

        /// <summary>
        /// Sets or creates the parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetParameter<T>(String key, T value)
        {
            Parameters[key] = value;
        }

        /// <summary>
        /// Gets the parameter as a specific type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T? GetParameter<T>(String key)
        {
            var retT = typeof(T);
            if (retT.IsEnum)
            {
                return MiscUtils.ConvertEnum<T?>(Parameters[key]);
            }
            return (T?)Parameters[key];
        }

        /// <summary>
        /// Regenerates the URI if package, subpackage or variation was changed
        /// </summary>
        void RegenerateURI();

        /// <summary>
        /// Dump on disk in JSON format
        /// </summary>
        void Serialize();

        /// <summary>
        /// Convert JSON format to the project asset object
        /// </summary>
        void Deserialize(string json);

        /// <summary>
        /// Converts given data to raw game's data
        /// </summary>
        /// <param name="data">Data to convert</param>
        void ToRaw(Byte[] data);

        /// <summary>
        /// Converts raw data to a general format
        /// </summary>
        /// <returns>Data in the format</returns>
        Byte[] ToFormat();

        /// <summary>
        /// Finishes import on Project Creation stage
        /// </summary>
        void Import();
    }
}
