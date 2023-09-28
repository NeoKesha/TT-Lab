using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        /// Project's collection of assets
        /// </summary>
        AssetManager AssetManager { get; }

        Folder Packages { get; }

        Package BasePackage { get; }

        Package Ps2Package { get; }

        Package XboxPackage { get; }

        /// <summary>
        /// Project's UUID
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        Guid UUID { get; }

        /// <summary>
        /// Project's Name
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        string Name { get; set; }

        /// <summary>
        /// Project's folder location
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        string Path { get; set; }

        /// <summary>
        /// PS2's disc content path
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        string? DiscContentPathPS2 { get; set; }

        /// <summary>
        /// XBox's disc content path
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        string? DiscContentPathXbox { get; set; }

        /// <summary>
        /// Date and time when project was last modified
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        DateTime LastModified { get; set; }

        /// <summary>
        /// Project's version
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        string Version { get; }

        /// <summary>
        /// Project's root file path
        /// </summary>
        string ProjectPath { get; }

        void CreateBasePackages();

        /// <summary>
        /// Unpacks PS2 assets data into project readable format
        /// </summary>
        void UnpackAssetsPS2();

        /// <summary>
        /// Unpacks XBox assets data into project readable format
        /// </summary>
        void UnpackAssetsXbox();

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
