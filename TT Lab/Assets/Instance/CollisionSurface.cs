using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class CollisionSurface : SerializableInstance
    {

        [JsonProperty(Required = Required.Always)]
        public UInt16 SurfaceID { get; set; }

        public CollisionSurface(UInt32 id, String name, String chunk, Int32 layId, PS2AnyCollisionSurface surface) : base(id, name, chunk, layId)
        {
            SurfaceID = surface.SurfaceId;
        }

        public CollisionSurface()
        {
        }

        protected override String SavePath => base.SavePath + "CollisionSurface";

        public override String Type => "CollisionSurface";

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
