using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WK.Libraries.BetterFolderBrowserNS;

namespace TT_Lab.Project
{
    public static class ProjectManager
    {
        private static Project OpenedProject;

        public static bool OpenProject(string path)
        {
            if (Directory.GetFiles(path, "*.tson").Length == 0) return false;

            var prFile = Directory.GetFiles(path, "*.tson")[0];
            try
            {
                using (FileStream fs = new FileStream(prFile, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    var prText = new string(reader.ReadChars((Int32)fs.Length));
                    Project pr = JsonConvert.DeserializeObject<Project>(prText);
                    OpenedProject = pr;
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        public static void CloseProject()
        {
            OpenedProject = null;
        }
    }
}
