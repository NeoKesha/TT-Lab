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
    public class CollisionSurface : SerializableInstance<CollisionSurfaceData>
    {
        public CollisionSurface(UInt32 id, String name, String chunk, Int32 layId, PS2AnyCollisionSurface surface) : base(id, name, chunk, layId)
        {
            assetData = new CollisionSurfaceData(surface);
        }

        public CollisionSurface()
        {
        }

        public override String Type => "CollisionSurface";

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
