using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets;

namespace TT_Lab.Project
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IProject
    {
        /// <summary>
        /// Project's UUID
        /// </summary>
        [JsonProperty]
        Guid UUID { get; }

        /// <summary>
        /// Project's name
        /// </summary>
        [JsonProperty]
        string Name { get; set; }

        /// <summary>
        /// Project's folder location
        /// </summary>
        [JsonProperty]
        string Path { get; set; }

        /// <summary>
        /// Game's disc content path
        /// </summary>
        [JsonProperty]
        string DiscContentPath { get; set; }

        /// <summary>
        /// Date and time when project was last modified
        /// </summary>
        [JsonProperty]
        DateTime LastModified { get; set; }

        /// <summary>
        /// Project's root file path
        /// </summary>
        string ProjectPath { get; }

        /// <summary>
        /// Unpacks game archives data into necessary folders
        /// </summary>
        void UnpackAssets();

        /// <summary>
        /// Gets a requested asset
        /// </summary>
        /// <typeparam name="T">Asset type</typeparam>
        /// <param name="id">Asset ID</param>
        /// <returns>Returns requested asset or null if not found</returns>
        T GetAsset<T>(Guid id) where T : IAsset;
    }
}
