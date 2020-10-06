using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.Lights;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2
{
    public class PS2AnyScenery : ITwinScenery
    {
        UInt32 id;
        public UInt32 Flags;
        public String Name;
        public UInt32 UnkUInt;
        public Byte UnkByte;
        public UInt32 SkydomeID;
        public Byte[] UnkBlob;
        public List<AmbientLight> AmbientLights;
        public List<DirectionalLight> DirectionalLights;
        public List<PointLight> PointLights;
        public List<NegativeLight> NegativeLights;
        public List<SceneryBaseType> Sceneries;

        public PS2AnyScenery()
        {
            AmbientLights = new List<AmbientLight>();
            DirectionalLights = new List<DirectionalLight>();
            PointLights = new List<PointLight>();
            NegativeLights = new List<NegativeLight>();
            Sceneries = new List<SceneryBaseType>();
        }

        public UInt32 GetID()
        {
            return id;
        }

        public Int32 GetLength()
        {
            return 4 + 4 + Name.Length + 4 + 4 + 1 + 
                (SkydomeID != 0 ? 4 : 0) +
                (UnkBlob != null ? 0x400 + 4 * 5 + AmbientLights.Sum(a => a.GetLength()) + 
                    DirectionalLights.Sum(d => d.GetLength()) + PointLights.Sum(p => p.GetLength()) +
                    NegativeLights.Sum(n => n.GetLength()) : 0) +
                Sceneries.Sum(s => s.GetLength());
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            Flags = reader.ReadUInt32();
            var nameLen = reader.ReadInt32();
            Name = new String(reader.ReadChars(nameLen));
            UnkUInt = reader.ReadUInt32();
            var sceneryType = reader.ReadInt32();
            UnkByte = reader.ReadByte();
            if ((Flags & 0x10000) != 0)
            {
                SkydomeID = reader.ReadUInt32();
            }
            if ((Flags & 0x20000) != 0)
            {
                UnkBlob = reader.ReadBytes(0x400);
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
                }
            }
            if (sceneryType == 0x160A)
            {
                var root = new SceneryRoot();
                Sceneries.Add(root);
                root.Read(reader, length, Sceneries);
            }
        }

        public void SetID(UInt32 id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            UInt32 newFlags = 0x0;
            if (SkydomeID != 0)
            {
                newFlags |= 0x10000;
            }
            if (UnkBlob != null)
            {
                newFlags |= 0x20000;
            }
            newFlags |= (Flags & 0xFFFCFFFF);
            Flags = newFlags;
            writer.Write(Flags);
            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray());
            writer.Write(UnkUInt);
            writer.Write(Sceneries.Count != 0 ? 0x1609 : 0);
            writer.Write(UnkByte);
            if ((Flags & 0x10000) != 0)
            {
                writer.Write(SkydomeID);
            }
            if ((Flags & 0x20000) != 0)
            {
                writer.Write(UnkBlob);
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
    }
}
