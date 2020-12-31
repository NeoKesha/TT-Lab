using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class CodeModelData : AbstractAssetData
    {
        public CodeModelData()
        {
        }

        public CodeModelData(PS2AnyCodeModel codeModel) : this()
        {
            twinRef = codeModel;
        }
        public String Script { get; set; }
        public List<UInt32> ScriptIds { get; set; }
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
            using (FileStream fs = new FileStream(dataPath, System.IO.FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs))
            {
                var cm = new PS2AnyCodeModel();
                cm.ReadText(reader);
                Script = cm.ToString();
                GetIds(cm);
                twinRef = cm;
            }
        }
        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
            PS2AnyCodeModel codeModel = (PS2AnyCodeModel)twinRef;
            Script = codeModel.ToString();
            GetIds(codeModel);
        }

        private void GetIds(PS2AnyCodeModel cm)
        {
            ScriptIds = new List<uint>();
            foreach (var e in cm.ScriptPacks)
            {
                ScriptIds.Add(e.Key);
            }
        }
    }
}
