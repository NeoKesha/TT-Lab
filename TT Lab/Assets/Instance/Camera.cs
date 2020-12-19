using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class Camera : SerializableInstance
    {
        [JsonProperty(Required = Required.Always)]
        public Boolean Enabled { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Vector4 Position { get; set; } = new Vector4();

        public Camera(UInt32 id, String name, String chunk, Int32 layId, PS2AnyCamera camera) : base(id, name, chunk, layId)
        {
            Enabled = camera.CamTrigger.Enabled > 0;
            Position = camera.CamTrigger.Position;
        }

        public Camera()
        {
        }

        protected override String SavePath => base.SavePath + "Camera";

        public override String Type => "Camera";

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
