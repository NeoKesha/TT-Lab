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
        [JsonProperty(Required = Required.Always)]
        Guid UUID { get; }

        /// <summary>
        /// Asset's string type
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        string Type { get; }

        /// <summary>
        /// In-Game's ID number
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        UInt32 ID { get; set; }

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
    }
}
