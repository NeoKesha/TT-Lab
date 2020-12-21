using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives;

namespace Twinsanity_Command_Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            String readArchives = args[0];
            String testPath = args[1];
            String testSavePath = args[2];
            if (readArchives == "Y")
            {
                testPath += @"\Crash6\";
                String[] archivePaths = System.IO.Directory.GetFiles(testPath, "*.BD", System.IO.SearchOption.TopDirectoryOnly);
                archivePaths = archivePaths.Concat(System.IO.Directory.GetFiles(testPath, "*.MB", System.IO.SearchOption.TopDirectoryOnly)).ToArray();
                List<ITwinSerializable> archives = new List<ITwinSerializable>();
                foreach (var path in archivePaths)
                {
                    var headerPath = String.Empty;
                    ITwinSerializable archive = null;
                    if (path.EndsWith("MB") || path.EndsWith("mb"))
                    {
                        headerPath = path.Replace(".MB", ".MH");
                        if (headerPath == path)
                        {
                            headerPath = path.Replace(".mb", ".mh");
                        }
                        archive = new PS2MB(headerPath, System.IO.Path.Combine(testSavePath, System.IO.Path.GetFileName(headerPath)));
                    }
                    else if (path.EndsWith("BD"))
                    {
                        headerPath = path.Replace(".BD", ".BH");
                        archive = new PS2BD(headerPath, System.IO.Path.Combine(testSavePath, System.IO.Path.GetFileName(headerPath)));
                    }
                    using (System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    using (System.IO.BinaryReader reader = new System.IO.BinaryReader(stream))
                    {
                        archive.Read(reader, (Int32)reader.BaseStream.Length);
                        archives.Add(archive);
                    }
                    using (System.IO.FileStream stream = new System.IO.FileStream(System.IO.Path.Combine(testSavePath, System.IO.Path.GetFileName(path)),
                        System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream))
                    {
                        archive.Write(writer);
                    }
                }
                Console.WriteLine("Done");
            }
            else
            {
                String[] levelsPaths = System.IO.Directory.GetFiles(testPath, "*.rm2", System.IO.SearchOption.AllDirectories);
                levelsPaths = levelsPaths.Concat(System.IO.Directory.GetFiles(testPath, "*.sm2", System.IO.SearchOption.AllDirectories)).ToArray();
                List<ITwinSection> levels = new List<ITwinSection>();
                foreach (String levelPath in levelsPaths)
                {
                    ITwinSection twinLevel = null;
                    if (levelPath.EndsWith("Default.rm2"))
                    {
                        twinLevel = new PS2Default(true);
                    }
                    else if (levelPath.EndsWith("rm2"))
                    {
                        twinLevel = new PS2AnyTwinsanityRM2(true);
                    }
                    else if (levelPath.EndsWith("sm2"))
                    {
                        twinLevel = new PS2AnyTwinsanitySM2(true);
                    }
                    else
                    {
                        twinLevel = new BaseTwinSection(true);
                    }
                    using (System.IO.FileStream stream = new System.IO.FileStream(levelPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    using (System.IO.BinaryReader reader = new System.IO.BinaryReader(stream))
                    {
                        twinLevel.Read(reader, (Int32)reader.BaseStream.Length);
                        levels.Add(twinLevel);
                    }
                    using (System.IO.FileStream stream = new System.IO.FileStream(System.IO.Path.Combine(testSavePath, System.IO.Path.GetFileName(levelPath)),
                        System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream))
                    {
                        twinLevel.Write(writer);
                    }
                }
            }
        }
    }
}
