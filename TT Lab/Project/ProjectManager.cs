using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using TT_Lab.ViewModels;

namespace TT_Lab.Project
{
    // Wrapper for keeping ProjectManager singleton and keep the ability to have ProjectManager as an observable object
    public static class ProjectManagerSingleton
    {
        private static ProjectManager _pm;

        public static ProjectManager PM
        {
            get
            {
                if (_pm == null)
                {
                    _pm = new ProjectManager();
                }
                return _pm;
            }
            private set
            {
                _pm = value;
            }
        }
    }

    public class ProjectManager : ObservableObject
    {
        private Project _openedProject;

        public Project OpenedProject
        {
            get
            {
                return _openedProject;
            }
            set
            {
                _openedProject = value;
                RaisePropertyChangedEvent("OpenedProject");
                RaisePropertyChangedEvent("ProjectOpened");
                RaisePropertyChangedEvent("ProjectTitle");
            }
        }

        public bool ProjectOpened
        {
            get
            {
                return OpenedProject != null;
            }
        }

        public string ProjectTitle
        {
            get
            {
                return OpenedProject != null ? $"TT Lab - {OpenedProject.Name}" : "TT Lab";
            }
        }

        public void CreateProject(string name, string path, string discContentPath)
        {
            var discFiles = Directory.GetFiles(discContentPath).Select(s => Path.GetFileName(s)).ToArray();
            // Check for either XBOX or PS2 required root disc files
            if (!discFiles.Contains("default.xbe") && !discFiles.Contains("System.cnf"))
            {
                throw new Exception("Improper disc content provided!");
            }
            OpenedProject = new Project(name, path, discContentPath);
        }

        public void OpenProject(string path)
        {
            if (Directory.GetFiles(path, "*.tson").Length == 0)
            {
                throw new Exception("No project root found!");
            }

            var prFile = Directory.GetFiles(path, "*.tson")[0];
            using (FileStream fs = new FileStream(prFile, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                var prText = new string(reader.ReadChars((Int32)fs.Length));
                OpenedProject = JsonConvert.DeserializeObject<Project>(prText);
            }
        }

        public void CloseProject()
        {
            OpenedProject = null;
        }
    }
}
