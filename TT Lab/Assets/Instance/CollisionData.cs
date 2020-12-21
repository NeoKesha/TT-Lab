using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;

namespace TT_Lab.Assets.Instance
{
    public class CollisionData : SerializableInstance
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt;
        [JsonProperty(Required = Required.Always)]
        public List<Collision.CollisionTrigger> Triggers { get; } = new List<Collision.CollisionTrigger>();
        [JsonProperty(Required = Required.Always)]
        public List<Collision.GroupInformation> Groups { get; } = new List<Collision.GroupInformation>();
        [JsonProperty(Required = Required.Always)]
        public List<Collision.CollisionTriangle> Triangles { get; } = new List<Collision.CollisionTriangle>();
        [JsonProperty(Required = Required.Always)]
        public List<Vector4> Vectors { get; } = new List<Vector4>();

        public override String Type => "CollisionData";

        public CollisionData()
        {
        }

        public CollisionData(UInt32 id, String name, String chunk, PS2AnyCollisionData collisionData) : base(id, name, chunk, -1)
        {
            UnkInt = collisionData.UnkInt;
            foreach (var trigger in collisionData.Triggers)
            {
                Triggers.Add(new Collision.CollisionTrigger(trigger));
            }
            foreach (var group in collisionData.Groups)
            {
                Groups.Add(new Collision.GroupInformation(group));
            }
            foreach (var triangle in collisionData.Triangles)
            {
                Triangles.Add(new Collision.CollisionTriangle(triangle));
            }
            // Clone the vectors instead of reference copying
            Vectors = collisionData.Vectors.Select(v => v).ToList();
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
