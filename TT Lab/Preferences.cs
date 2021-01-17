using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;

namespace TT_Lab
{
    /// <summary>
    /// Stores information about user's preferences and settings
    /// </summary>
    public static class Preferences
    {
        private static readonly Dictionary<string, object> Settings = new Dictionary<string, object>();
        private static readonly string ExePath = ManifestResourceLoader.GetPathInExe("");
        private static readonly string PrefFileName = "settings.json";

        public static void Save()
        {
            using FileStream settings = File.Create(Path.Combine(ExePath, PrefFileName));
            using BinaryWriter writer = new BinaryWriter(settings);
            writer.Write(JsonConvert.SerializeObject(Settings).ToCharArray());
        }

        public static void Load()
        {
            using FileStream settings = new FileStream(Path.Combine(ExePath, PrefFileName), FileMode.Open, FileAccess.Read);
            using StreamReader reader = new StreamReader(settings);
            JsonConvert.PopulateObject(reader.ReadToEnd(), Settings);
        }

        public static void SetPreference(string prefName, object value)
        {
            Settings[prefName] = value;
        }

        public static T GetPreference<T>(string prefName)
        {
            return (T)Settings[prefName];
        }
    }
}
