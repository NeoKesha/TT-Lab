using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Code;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class HeaderScript : Script<HeaderScriptData>
    {
        public override String Type => "HeaderScript";

        public HeaderScript() { }

        public HeaderScript(UInt32 id, String name, PS2HeaderScript script) : base(id, name)
        {
            assetData = new HeaderScriptData(script);
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
