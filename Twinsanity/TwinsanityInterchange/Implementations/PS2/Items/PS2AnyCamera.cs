using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items
{
    public class PS2AnyCamera : ITwinCamera
    {
        UInt32 id;
        public UInt32 Header1 { get; set; }
        public UInt32 Enabled { get; set; }
        public Single HeaderT { get; set; }
        public UInt32 HeaderH { get; set; }
        public Vector4 Rotation { get; }
        public Vector4 Position { get; }
        public Vector4 Scale { get; }
        public List<UInt16> Instances { get; }
        public UInt32 CameraHeader { get; set; }
        public UInt16 UnkShort { get; set; }
        public Single UnkFloat1 { get; set; } // 10
        public Vector4 UnkVector1 { get; private set; }
        public Vector4 UnkVector2 { get; private set; } // 42
        public Single UnkFloat2 { get; set; } 
        public Single UnkFloat3 { get; set; } // 50
        public UInt32 UnkInt1 { get; set; }
        public UInt32 UnkInt2 { get; set; }
        public UInt32 UnkInt3 { get; set; }
        public UInt32 UnkInt4 { get; set; } // 66
        public UInt32 UnkInt5 { get; set; }
        public UInt32 UnkInt6 { get; set; } // 74
        public Single UnkFloat4 { get; set; }
        public Single UnkFloat5 { get; set; }
        public Single UnkFloat6 { get; set; }
        public Single UnkFloat7 { get; set; } // 90
        public UInt32 UnkInt7 { get; set; }
        public UInt32 UnkInt8 { get; set; } // 98
        public UInt32 UnkInt9 { get; set; } 
        public Single UnkFloat8 { get; set; } // 106
        public UInt32 TypeIndex1 { get; set; }
        public UInt32 TypeIndex2 { get; set; } // 114
        public Byte UnkByte { get; set; } // 115
        public PS2AnyCamera()
        {
            Rotation = new Vector4();
            Position = new Vector4();
            Scale = new Vector4();
            Instances = new List<UInt16>();
            UnkVector1 = new Vector4();
            UnkVector2 = new Vector4();
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return 16 + Position.GetLength() + Rotation.GetLength() + Scale.GetLength() + 8 + Instances.Count * Constants.SIZE_UINT16 + 115;
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
            // Camera
            CameraHeader = reader.ReadUInt32();
            UnkShort = reader.ReadUInt16();
            UnkFloat1 = reader.ReadSingle();
            UnkVector1.Read(reader, Constants.SIZE_VECTOR4);
            UnkVector2.Read(reader, Constants.SIZE_VECTOR4);
            UnkFloat2 = reader.ReadSingle();
            UnkFloat3 = reader.ReadSingle();
            UnkInt1 = reader.ReadUInt32();
            UnkInt2 = reader.ReadUInt32();
            UnkInt3 = reader.ReadUInt32();
            UnkInt4 = reader.ReadUInt32();
            UnkInt5 = reader.ReadUInt32();
            UnkInt6 = reader.ReadUInt32();
            UnkFloat4 = reader.ReadSingle();
            UnkFloat5 = reader.ReadSingle();
            UnkFloat6 = reader.ReadSingle();
            UnkFloat7 = reader.ReadSingle();
            UnkInt7 = reader.ReadUInt32();
            UnkInt8 = reader.ReadUInt32();
            UnkInt9 = reader.ReadUInt32();
            UnkFloat8 = reader.ReadSingle();
            TypeIndex1 = reader.ReadUInt32();
            TypeIndex2 = reader.ReadUInt32();
            UnkByte = reader.ReadByte();
        }

        public void SetID(uint id)
        {
            this.id = id;
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
            //
            writer.Write(CameraHeader);
            writer.Write(UnkShort);
            writer.Write(UnkFloat1);
            UnkVector1.Write(writer);
            UnkVector2.Write(writer);
            writer.Write(UnkFloat2);
            writer.Write(UnkFloat3);
            writer.Write(UnkInt1);
            writer.Write(UnkInt2);
            writer.Write(UnkInt3);
            writer.Write(UnkInt4);
            writer.Write(UnkInt5);
            writer.Write(UnkInt6);
            writer.Write(UnkFloat4);
            writer.Write(UnkFloat5);
            writer.Write(UnkFloat6);
            writer.Write(UnkFloat7);
            writer.Write(UnkInt7);
            writer.Write(UnkInt8);
            writer.Write(UnkInt9);
            writer.Write(UnkFloat8);
            writer.Write(TypeIndex1);
            writer.Write(TypeIndex2);
            writer.Write(UnkByte);
        }
    }
}
