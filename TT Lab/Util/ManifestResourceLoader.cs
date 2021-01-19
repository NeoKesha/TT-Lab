using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Util
{
    public static class ManifestResourceLoader
    {
        public static string LoadTextFile(string textFileName)
        {
            using FileStream stream = new FileStream(GetPathInExe(textFileName), FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static string GetPathInExe(string pathToFile)
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            UriBuilder uri = new UriBuilder(assemblyLocation);
            string path = Uri.UnescapeDataString(uri.Path);

            return Path.Combine(Path.GetDirectoryName(path)!, pathToFile);
        }
    }
}
