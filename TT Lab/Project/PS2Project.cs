using Newtonsoft.Json;
using System;
using TT_Lab.Assets;

namespace TT_Lab.Project
{
    /// <summary>
    /// PS2 project class, the root has the reserved extension .tson.
    /// When implementing XBox project use .xson extension to differentiate between the two
    /// </summary>
    public class PS2Project : IProject
    {
        public Guid UUID { get; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string DiscContentPath { get; set; }

        public DateTime LastModified { get; set; }

        public string ProjectPath
        {
            get
            {
                return System.IO.Path.Combine(Path, Name);
            }
        }

        public PS2Project() { }

        public PS2Project(string name, string path, string discContentPath)
        {
            Name = name;
            Path = path;
            DiscContentPath = discContentPath;
            LastModified = DateTime.Now;
            UUID = Guid.NewGuid();
            var projDir = System.IO.Path.Combine(Path, Name);
            System.IO.Directory.CreateDirectory(projDir);
            System.IO.Directory.SetCurrentDirectory(projDir);
            using (System.IO.FileStream fs = new System.IO.FileStream(Name + ".tson", System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs))
            {
                writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented).ToCharArray());
            }
        }

        public void UnpackAssets()
        {
            throw new NotImplementedException();
        }

        public T GetAsset<T>(Guid id) where T : IAsset
        {
            throw new NotImplementedException();
        }
    }
}
