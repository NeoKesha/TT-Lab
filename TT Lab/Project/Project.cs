using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Project
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Project
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string Path { get; set; }

        [JsonProperty]
        public string DiscContentPath { get; set; }

        [JsonProperty]
        public DateTime LastModified { get; set; }

        public Project() { }

        public Project(string name, string path, string discContentPath)
        {
            Name = name;
            Path = path;
            DiscContentPath = discContentPath;
            LastModified = DateTime.Now;
            var projDir = System.IO.Path.Combine(Path, Name);
            System.IO.Directory.CreateDirectory(projDir);
            System.IO.Directory.SetCurrentDirectory(projDir);
            using (System.IO.FileStream fs = new System.IO.FileStream(Name + ".tson", System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs))
            {
                writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented).ToCharArray());
            }
        }
    }
}
