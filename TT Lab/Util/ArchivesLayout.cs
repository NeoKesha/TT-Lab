using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Util
{
    public static class ArchivesLayout
    {
        public static List<String> ExtrasFolders
        {
            get => new()
            {
                "Bosses",
                "Concept",
                "Enemies",
                "Storyboards",
                "Test",
                "Unseen"
            };
        }

        public static List<String> LanguageFolder
        {
            get => new()
            {
                "AgentLab",
                "Code",
                "Credits",
                "Gameover",
                "Legal",
                "Loading",
                "Titles"
            };
        }

        public static List<String> StartupItems
        {
            get => new()
            {
                "Fonts",
                "Decal",
                "Default",
                "Frontend",
                "Icons",
                "Crash",
                "LevelSelect"
            };
        }
    }
}
