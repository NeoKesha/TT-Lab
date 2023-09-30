using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics
{
    public class PS2AnyLOD : BaseTwinItem, ITwinLOD
    {
        public Int32 Type { get; set; }
        public Int32 MinDrawDistance { get; set; }
        public Int32 MaxDrawDistance { get; set; }
        public Int32[] ModelsDrawDistances { get; set; }
        public List<UInt32> Meshes { get; set; }

        public PS2AnyLOD()
        {
            ModelsDrawDistances = new Int32[3];
            Meshes = new List<UInt32>();
        }

        public override int GetLength()
        {
            if (Type == 0x1001)
            {
                return 28 + Meshes.Count * Constants.SIZE_UINT32;
            }
            return 25 + Meshes.Count * Constants.SIZE_UINT32;
        }

        public override void Read(BinaryReader reader, int length)
        {
            Type = reader.ReadInt32();
            if (Type == 0x1001)
            {
                var meshAmt = reader.ReadInt32() & 0xFF;
                MinDrawDistance = reader.ReadInt32();
                MaxDrawDistance = reader.ReadInt32();
                for (int i = 0; i < 3; ++i)
                {
                    ModelsDrawDistances[i] = reader.ReadInt32();
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
                MinDrawDistance = reader.ReadInt32();
                MaxDrawDistance = reader.ReadInt32();
                for (int i = 0; i < 3; ++i)
                {
                    ModelsDrawDistances[i] = reader.ReadInt32();
                }
                for (int i = 0; i < meshAmt; ++i)
                {
                    Meshes.Add(reader.ReadUInt32());
                }
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Type);
            if (Type == 0x1001)
            {
                writer.Write(Meshes.Count);
                writer.Write(MinDrawDistance);
                writer.Write(MaxDrawDistance);
                for (int i = 0; i < 3; ++i)
                {
                    writer.Write(ModelsDrawDistances[i]);
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
                writer.Write(MinDrawDistance);
                writer.Write(MaxDrawDistance);
                for (int i = 0; i < 3; ++i)
                {
                    writer.Write(ModelsDrawDistances[i]);
                }
                for (int i = 0; i < Meshes.Count; ++i)
                {
                    writer.Write(Meshes[i]);
                }
            }
        }

        public override String GetName()
        {
            return $"LOD {id:X}";
        }
    }
}
