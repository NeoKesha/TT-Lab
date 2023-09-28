using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Util;
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
            SetTwinItem(collision);
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Collision.CollisionTrigger> Triggers { get; set; } = new List<Collision.CollisionTrigger>();
        [JsonProperty(Required = Required.Always)]
        public List<Collision.GroupInformation> Groups { get; set; } = new List<Collision.GroupInformation>();
        [JsonProperty(Required = Required.Always)]
        public List<Collision.CollisionTriangle> Triangles { get; set; } = new List<Collision.CollisionTriangle>();
        [JsonProperty(Required = Required.Always)]
        public List<Vector4> Vectors { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Triggers.Clear();
            Groups.Clear();
            Triangles.Clear();
            Vectors.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            PS2AnyCollisionData collision = GetTwinItem<PS2AnyCollisionData>();
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
            Vectors = CloneUtils.CloneList(collision.Vectors);
        }
    }
}
