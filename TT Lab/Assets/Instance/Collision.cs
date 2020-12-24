using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;

namespace TT_Lab.Assets.Instance
{
    public class Collision : SerializableInstance
    {
        public override String Type => "CollisionData";

        public Collision()
        {
        }

        public Collision(UInt32 id, String name, String chunk, PS2AnyCollisionData collisionData) : base(id, name, chunk, null)
        {
            assetData = new CollisionData(collisionData);
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
