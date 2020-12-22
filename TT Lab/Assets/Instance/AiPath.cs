using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class AiPath : SerializableInstance<AiPathData>
    {
        public AiPath(UInt32 id, String name, String chunk, Int32 layId, PS2AnyAIPath path) : base(id, name, chunk, layId)
        {
            assetData = new AiPathData(path);
        }

        public AiPath()
        {
        }

        public override String Type => "AiPath";

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
