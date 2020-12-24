using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;

namespace TT_Lab.Assets.Instance
{
    public class DynamicScenery : SerializableInstance
    {
        public DynamicScenery()
        {
        }

        public DynamicScenery(UInt32 id, String name, String chunk, PS2AnyDynamicScenery dynamicScenery) : base(id, name, chunk, null)
        {
            assetData = new DynamicSceneryData(dynamicScenery);
        }

        public override String Type => "DynamicScenery";

        public override UserControl GetEditor()
        {
            throw new NotImplementedException();
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
