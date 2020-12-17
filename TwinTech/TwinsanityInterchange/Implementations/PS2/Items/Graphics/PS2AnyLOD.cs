using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics
{
    public class PS2AnyLOD : ITwinLOD
    {
        UInt32 id;
        public Int32 Type;
        public Int32 UnkInt1;
        public Int32 UnkInt2; // Draw distance?
        public Int32[] UnkInts; // For Type 0x1001
        public Byte[] UnkData; // For Type 0x1002
        public List<UInt32> Meshes;

        public PS2AnyLOD()
        {
            UnkInts = new Int32[3];
            Meshes = new List<UInt32>();
        }

        public UInt32 GetID()
        {
            return id;
        }

        public int GetLength()
        {
            if (Type == 0x1001)
            {
                return 28 + Meshes.Count * Constants.SIZE_UINT32;
            }
            return 25 + Meshes.Count * Constants.SIZE_UINT32;
        }

        public void Read(BinaryReader reader, int length)
        {
            Type = reader.ReadInt32();
            if (Type == 0x1001)
            {
                var meshAmt = reader.ReadInt32() & 0xFF;
                UnkInt1 = reader.ReadInt32();
                UnkInt2 = reader.ReadInt32();
                for (int i = 0; i < 3; ++i)
                {
                    UnkInts[i] = reader.ReadInt32();
                }
                reader.ReadInt32(); // Unused by the game
                for (int i = 0; i < meshAmt; ++i)
                {
                    Meshes.Add(reader.ReadUInt32());
                }
            }
            else
            {
                var meshAmt = reader.ReadByte();
                UnkInt1 = reader.ReadInt32();
                UnkInt2 = reader.ReadInt32();
                UnkData = reader.ReadBytes(0xC);
                for (int i = 0; i < meshAmt; ++i)
                {
                    Meshes.Add(reader.ReadUInt32());
                }
            }
        }

        public void SetID(UInt32 id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Type);
            if (Type == 0x1001)
            {
                writer.Write(Meshes.Count);
                writer.Write(UnkInt1);
                writer.Write(UnkInt2);
                for (int i = 0; i < 3; ++i)
                {
                    writer.Write(UnkInts[i]);
                }
                writer.Write(0);
                for (int i = 0; i < Meshes.Count; ++i)
                {
                    writer.Write(Meshes[i]);
                }
            }
            else
            {
                writer.Write((byte)Meshes.Count);
                writer.Write(UnkInt1);
                writer.Write(UnkInt2);
                writer.Write(UnkData);
                for (int i = 0; i < Meshes.Count; ++i)
                {
                    writer.Write(Meshes[i]);
                }
            }
        }

        public String GetName()
        {
            return $"LOD {id:X}";
        }
    }
}
