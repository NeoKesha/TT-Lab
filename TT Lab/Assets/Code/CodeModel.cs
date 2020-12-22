using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class CodeModel : SerializableAsset<CodeModelData>
    {
        public override String Type => "CodeModel";

        public CodeModel() {}

        public CodeModel(UInt32 id, String name, PS2AnyCodeModel codeModel) : base(id, name)
        {
            assetData = new CodeModelData(codeModel);
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
