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
    public class Scenery : SerializableInstance<SceneryData>
    {
        public Scenery()
        {
        }

        public Scenery(UInt32 id, String name, String chunk, PS2AnyScenery scenery) : base(id, name, chunk, null)
        {
            assetData = new SceneryData(scenery);
        }

        public override String Type => "Scenery";

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
