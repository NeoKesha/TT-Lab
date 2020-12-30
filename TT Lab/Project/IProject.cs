using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets;

namespace TT_Lab.Project
{
    /// <summary>
    /// Base interface for the PS2 and XBox project types
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public interface IProject
    {
        /// <summary>
        /// Maps a string to an asset Type
        /// </summary>
        /// <example>
        /// IAsset newTex = (IAsset)Activator.CreateInstance(StringToAsset["Texture"]);
        /// </example>
        Dictionary<string, Type> StringToAsset { get; }

        /// <summary>
        /// Project's collection of assets
        /// </summary>
        Dictionary<Guid, IAsset> Assets { get; }

        /// <summary>
        /// Project's list of assets
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        Dictionary<Guid, UInt32> GuidToTwinId { get; }

        /// <summary>
        /// Project's UUID
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        Guid UUID { get; }

        /// <summary>
        /// Project's name
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        string Name { get; set; }

        /// <summary>
        /// Project's folder location
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        string Path { get; set; }

        /// <summary>
        /// Game's disc content path
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        string DiscContentPath { get; set; }

        /// <summary>
        /// Date and time when project was last modified
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        DateTime LastModified { get; set; }

        /// <summary>
        /// Project's root file path
        /// </summary>
        string ProjectPath { get; }

        /// <summary>
        /// Unpacks game assets data into project readable format
        /// </summary>
        void UnpackAssets();

        /// <summary>
        /// Gets a requested asset
        /// </summary>
        /// <typeparam name="T">Asset type</typeparam>
        /// <param name="id">Asset ID</param>
        /// <returns>Returns requested asset or null if not found</returns>
        IAsset GetAsset(Guid id);

        /// <summary>
        /// Dump on disk in JSON format
        /// </summary>
        void Serialize();

        /// <summary>
        /// Initializes project directories
        /// </summary>
        void CreateProjectStructure();
    }
}
