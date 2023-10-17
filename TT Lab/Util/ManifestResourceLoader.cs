using System;
using System.IO;
using System.Reflection;

namespace TT_Lab.Util
{
    public static class ManifestResourceLoader
    {
        public static string LoadTextFile(string textFileName)
        {
            using FileStream stream = new(GetPathInExe(textFileName), FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static string GetPathInExe(string pathToFile)
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            UriBuilder uri = new(assemblyLocation);
            string path = Uri.UnescapeDataString(uri.Path);

            return Path.Combine(Path.GetDirectoryName(path)!, pathToFile);
        }
    }
}
