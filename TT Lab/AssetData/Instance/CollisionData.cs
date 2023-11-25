using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.Collision;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM;

namespace TT_Lab.AssetData.Instance
{
    public class CollisionData : AbstractAssetData
    {
        public CollisionData()
        {
            Vectors = new();
        }

        public CollisionData(ITwinCollision collision) : this()
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

        public override void Import(LabURI package, String? variant, Int32? layoutId)
        {
            ITwinCollision collision = GetTwinItem<ITwinCollision>();
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

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(UnkInt);
            writer.Write(Triggers.Count);
            writer.Write(Groups.Count);
            writer.Write(Triangles.Count);
            writer.Write(Vectors.Count);

            foreach (var trigger in Triggers)
            {
                trigger.V1.Write(writer);
                writer.Write(trigger.MinTriggerIndex);
                trigger.V2.Write(writer);
                writer.Write(trigger.MaxTriggerIndex);
            }

            foreach (var group in Groups)
            {
                writer.Write(group.Size);
                writer.Write(group.Offset);
            }

            foreach (var tri in Triangles)
            {
                var twinTri = new TwinCollisionTriangle()
                {
                    Vector1Index = tri.Face.Indexes![0],
                    Vector2Index = tri.Face.Indexes[1],
                    Vector3Index = tri.Face.Indexes[2],
                    SurfaceIndex = tri.SurfaceIndex
                };
                twinTri.Write(writer);
            }

            foreach (var vec in Vectors)
            {
                vec.Write(writer);
            }

            writer.Flush();
            ms.Position = 0;
            return factory.GenerateCollision(ms);
        }
    }
}
