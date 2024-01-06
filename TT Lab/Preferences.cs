using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Util;

namespace TT_Lab
{
    /// <summary>
    /// Stores information about user's preferences and settings
    /// </summary>
    public static class Preferences
    {
        public static event EventHandler<PreferenceChangedArgs>? PreferenceChanged;

        private static readonly Dictionary<string, object> Settings = new();
        private static readonly string ExePath = ManifestResourceLoader.GetPathInExe("");
        private static readonly string PrefFileName = "settings.json";
        private static readonly string PrefFilePath;

        // List of Named settings
        public static readonly string TranslucencyMethod = "TranslucencyMethod";

        static Preferences()
        {
            PrefFilePath = Path.Combine(ExePath, PrefFileName);
            Settings[TranslucencyMethod] = Rendering.RenderSwitches.TranslucencyMethod.WBOIT;
        }

        public static void Save()
        {
            using FileStream settings = File.Create(PrefFilePath);
            using BinaryWriter writer = new(settings);
            writer.Write(JsonConvert.SerializeObject(Settings, Formatting.Indented).ToCharArray());
        }

        public static void Load()
        {
            if (File.Exists(PrefFilePath))
            {
                using FileStream settings = new(PrefFilePath, FileMode.Open, FileAccess.Read);
                using StreamReader reader = new(settings);
                JsonConvert.PopulateObject(reader.ReadToEnd(), Settings);
            }
        }

        public static void SetPreference<T>(string prefName, T value) where T : class
        {
            Settings[prefName] = value;
            PreferenceChanged?.Invoke(null, new PreferenceChangedArgs { PreferenceName = prefName });
        }

        public static T GetPreference<T>(string prefName)
        {
            var retT = typeof(T);
            if (retT.IsEnum)
            {
                return MiscUtils.ConvertEnum<T>(Settings[prefName])!;
            }
            return (T)Settings[prefName];
        }

        public class PreferenceChangedArgs : EventArgs
        {
            public string PreferenceName { get; set; } = "";
        }
    }
}
