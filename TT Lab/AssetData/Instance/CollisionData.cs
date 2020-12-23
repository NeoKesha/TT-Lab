using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2;

namespace TT_Lab.AssetData.Instance
{
    public class CollisionData : AbstractAssetData
    {
        public CollisionData()
        {
        }

        public CollisionData(PS2AnyCollisionData collision) : this()
        {
            UnkInt = collision.UnkInt;
            foreach (var trigger in collision.Triggers)
            {
                Triggers.Add(new Collision.CollisionTrigger(trigger));
            }
            foreach (var group in collision.Groups)
            {
                Groups.Add(new Collision.GroupInformation(group));
            }
            foreach (var triangle in collision.Triangles)
            {
                Triangles.Add(new Collision.CollisionTriangle(triangle));
            }
            // Clone the vectors instead of reference copying
            Vectors = collision.Vectors.Select(v => v).ToList();
        }

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

        protected override void Dispose(Boolean disposing)
        {
            Triggers.Clear();
            Groups.Clear();
            Triangles.Clear();
            Vectors.Clear();
        }
    }
}
