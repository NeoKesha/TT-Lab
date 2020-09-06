using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity_Command_Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            String testPath = @"D:\Documents\Twinsanity\NTSC_Data\Levels";
            String testSavePath = @"D:\Documents\";
            String[] levelsPaths = System.IO.Directory.GetFiles(testPath, "*.rm2", System.IO.SearchOption.AllDirectories);
            levelsPaths = levelsPaths.Concat(System.IO.Directory.GetFiles(testPath, "*.sm2", System.IO.SearchOption.AllDirectories)).ToArray();
            List<ITwinSection> levels = new List<ITwinSection>();
            foreach (String levelPath in levelsPaths)
            {
                ITwinSection twinLevel = null;
                if (levelPath.EndsWith("rm2"))
                {
                    twinLevel = new PS2AnyTwinsanityRM2();
                }
                else if (levelPath.EndsWith("sm2"))
                {
                    twinLevel = new PS2AnyTwinsanitySM2();
                }
                else
                {
                    twinLevel = new BaseTwinSection();
                }
                using (System.IO.FileStream stream = new System.IO.FileStream(levelPath, System.IO.FileMode.Open,System.IO.FileAccess.Read)) 
                using (System.IO.BinaryReader reader = new System.IO.BinaryReader(stream))
                {
                    twinLevel.Read(reader, (Int32)reader.BaseStream.Length);
                    levels.Add(twinLevel);
                }
                using (System.IO.FileStream stream = new System.IO.FileStream(System.IO.Path.Combine(testSavePath,System.IO.Path.GetFileName(levelPath)), 
                    System.IO.FileMode.Create, System.IO.FileAccess.Write))
                using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(stream))
                {
                    twinLevel.Write(writer);
                }
                break;
            }
        }
    }
}
