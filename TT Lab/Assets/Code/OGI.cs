using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData.Code;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class OGI : SerializableAsset
    {
        public override String Type => "OGI";

        public OGI() { }

        public OGI(UInt32 id, String name, PS2AnyOGI ogi) : base(id, name)
        {
            assetData = new OGIData(ogi);
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override UserControl GetEditor()
        {
            throw new NotImplementedException();
        }
    }
}
