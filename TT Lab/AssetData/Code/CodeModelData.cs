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
            Script = codeModel.ToString();
        }
        public String Script { get; set; }
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
                Script = reader.ReadToEnd();
            }
        }
        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
