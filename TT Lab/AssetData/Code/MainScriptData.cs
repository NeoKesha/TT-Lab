﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class MainScriptData : ScriptData
    {
        public MainScriptData()
        {
        }

        public MainScriptData(PS2MainScript mainScript) : base(mainScript)
        {
            Script = mainScript.ToString();
        }

        public String Script { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Save(string dataPath)
        {
            using (FileStream fs = new FileStream(dataPath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(Script.ToCharArray());
            }
        }

        public override void Load(String dataPath)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(dataPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            using (System.IO.StreamReader reader = new System.IO.StreamReader(fs))
            {
                Script = reader.ReadToEnd();
            }
        }
    }
}
