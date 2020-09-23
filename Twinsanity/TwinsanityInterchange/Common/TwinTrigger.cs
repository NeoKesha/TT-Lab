using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class TwinTrigger : ITwinSerializable
    {
        public UInt32 Header1 { get; set; }
        public UInt32 Enabled { get; set; }
        public Single HeaderT { get; set; }
        public UInt32 HeaderH { get; set; }
        public Vector4 Rotation { get; }
        public Vector4 Position { get; }
        public Vector4 Scale { get; }
        public List<UInt16> Instances { get; }
        public TwinTrigger()
        {
            Rotation = new Vector4();
            Position = new Vector4();
            Scale = new Vector4();
            Instances = new List<UInt16>();
        }

        public int GetLength()
        {
            return 16 + Position.GetLength() + Rotation.GetLength() + Scale.GetLength() + 8 + Instances.Count * Constants.SIZE_UINT16;
        }

        public void Read(BinaryReader reader, int length)
        {
            Header1 = reader.ReadUInt32();
            Enabled = reader.ReadUInt32();
            HeaderT = reader.ReadSingle();
            Rotation.Read(reader, Constants.SIZE_VECTOR4);
            Position.Read(reader, Constants.SIZE_VECTOR4);
            Scale.Read(reader, Constants.SIZE_VECTOR4);
            reader.ReadUInt32();
            UInt32 instances_cnt = reader.ReadUInt32();
            HeaderH = reader.ReadUInt32();
            Instances.Clear();
            for (int i = 0; i < instances_cnt; ++i)
            {
                Instances.Add(reader.ReadUInt16());
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Header1);
            writer.Write(Enabled);
            writer.Write(HeaderT);
            Rotation.Write(writer);
            Position.Write(writer);
            Scale.Write(writer);
            writer.Write(Instances.Count);
            writer.Write(Instances.Count);
            writer.Write(HeaderH);
            for (int i = 0; i < Instances.Count; ++i)
            {
                writer.Write(Instances[i]);
            }
        }
    }
}
