using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Common.Lights;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2
{
    public class PS2AnyScenery : BaseTwinItem, ITwinScenery
    {
        public UInt32 Flags { get; set; }
        public String Name { get; set; }
        public UInt32 UnkUInt { get; set; }
        public Byte UnkByte { get; set; }
        public UInt32 SkydomeID { get; set; }
        public Boolean[] UnkLightFlags { get; set; }
        public Byte[] ReservedBlob { get; set; }
        public List<AmbientLight> AmbientLights { get; set; }
        public List<DirectionalLight> DirectionalLights { get; set; }
        public List<PointLight> PointLights { get; set; }
        public List<NegativeLight> NegativeLights { get; set; }
        public List<TwinSceneryBaseType> Sceneries { get; set; }

        public PS2AnyScenery()
        {
            UnkLightFlags = new Boolean[6];
            AmbientLights = new List<AmbientLight>();
            DirectionalLights = new List<DirectionalLight>();
            PointLights = new List<PointLight>();
            NegativeLights = new List<NegativeLight>();
            Sceneries = new List<TwinSceneryBaseType>();
        }

        public override Int32 GetLength()
        {
            return 4 + 4 + Name.Length + 4 + 4 + 1 +
                (SkydomeID != 0 ? 4 : 0) +
                (ReservedBlob != null ? 0x3E8 + 24 + 4 * 5 + AmbientLights.Sum(a => a.GetLength()) +
                    DirectionalLights.Sum(d => d.GetLength()) + PointLights.Sum(p => p.GetLength()) +
                    NegativeLights.Sum(n => n.GetLength()) : 0) +
                Sceneries.Sum(s => s.GetLength());
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            Flags = reader.ReadUInt32();
            var NameLen = reader.ReadInt32();
            Name = new String(reader.ReadChars(NameLen));
            UnkUInt = reader.ReadUInt32();
            var sceneryType = reader.ReadInt32();
            UnkByte = reader.ReadByte();
            if ((Flags & 0x10000) != 0)
            {
                SkydomeID = reader.ReadUInt32();
            }
            if ((Flags & 0x20000) != 0)
            {
                for (var i = 0; i < UnkLightFlags.Length; ++i)
                {
                    UnkLightFlags[i] = reader.ReadInt32() != 0;
                }
                ReservedBlob = reader.ReadBytes(0x3E8);
                reader.ReadInt32(); // Total lights amount
                var ambientLights = reader.ReadInt32();
                var dirLights = reader.ReadInt32();
                var pointLights = reader.ReadInt32();
                var negativeLights = reader.ReadInt32();
                // GetLength methods can be used here since all these classes have static length
                for (var i = 0; i < ambientLights; ++i)
                {
                    var ambient = new AmbientLight();
                    ambient.Read(reader, ambient.GetLength());
                    AmbientLights.Add(ambient);
                }
                for (var i = 0; i < dirLights; ++i)
                {
                    var directional = new DirectionalLight();
                    directional.Read(reader, directional.GetLength());
                    DirectionalLights.Add(directional);
                }
                for (var i = 0; i < pointLights; ++i)
                {
                    var point = new PointLight();
                    point.Read(reader, point.GetLength());
                    PointLights.Add(point);
                }
                for (var i = 0; i < negativeLights; ++i)
                {
                    var negative = new NegativeLight();
                    negative.Read(reader, negative.GetLength());
                    NegativeLights.Add(negative);
                }
            }
            if (sceneryType == 0x160A)
            {
                var root = new TwinSceneryRoot();
                Sceneries.Add(root);
                root.Read(reader, length, Sceneries);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            UInt32 newFlags = 0x0;
            if (SkydomeID != 0)
            {
                newFlags |= 0x10000;
            }
            if (ReservedBlob != null)
            {
                newFlags |= 0x20000;
            }
            newFlags |= (Flags & 0xFFFCFFFF);
            Flags = newFlags;
            writer.Write(Flags);
            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray());
            writer.Write(UnkUInt);
            writer.Write(Sceneries.Count != 0 ? 0x160A : 3);
            writer.Write(UnkByte);
            if ((Flags & 0x10000) != 0)
            {
                writer.Write(SkydomeID);
            }
            if ((Flags & 0x20000) != 0)
            {
                for (var i = 0; i < UnkLightFlags.Length; ++i)
                {
                    writer.Write(UnkLightFlags[i] ? 1 : 0);
                }
                writer.Write(ReservedBlob);
                writer.Write(AmbientLights.Count + DirectionalLights.Count + PointLights.Count + NegativeLights.Count);
                writer.Write(AmbientLights.Count);
                writer.Write(DirectionalLights.Count);
                writer.Write(PointLights.Count);
                writer.Write(NegativeLights.Count);
                foreach (var l in AmbientLights)
                {
                    l.Write(writer);
                }
                foreach (var l in DirectionalLights)
                {
                    l.Write(writer);
                }
                foreach (var l in PointLights)
                {
                    l.Write(writer);
                }
                foreach (var l in NegativeLights)
                {
                    l.Write(writer);
                }
            }
            if (Sceneries.Count != 0)
            {
                foreach (var s in Sceneries)
                {
                    s.Write(writer);
                }
            }
        }

        public override String GetName()
        {
            return $"Scenery {id:X}";
        }
    }
}
