using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class InstanceTemplate : SerializableInstance<InstanceTemplateData>
    {
        public InstanceTemplate(UInt32 id, String name, String chunk, Int32 layId, PS2AnyTemplate template) : base(id, name, chunk, layId)
        {
            assetData = new InstanceTemplateData(template);
        }

        public InstanceTemplate()
        {
        }

        public override String Type => "InstanceTemplate";

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
