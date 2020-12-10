using Newtonsoft.Json;
using System;

namespace TT_Lab.Assets
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IAsset
    {
        /// <summary>
        /// Asset's unique ID
        /// </summary>
        [JsonProperty]
        Guid UUID { get; }

        /// <summary>
        /// Asset's name
        /// </summary>
        [JsonProperty]
        string Name { get; set; }

        /// <summary>
        /// Whether asset's data is in raw form
        /// </summary>
        [JsonProperty]
        bool Raw { get; set; }

        /// <summary>
        /// Path to asset's data
        /// </summary>
        [JsonProperty]
        string Data { get; set; }
    }
}
