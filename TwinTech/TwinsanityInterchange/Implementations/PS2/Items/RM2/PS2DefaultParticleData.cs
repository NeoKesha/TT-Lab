using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2
{
    public class PS2DefaultParticleData : PS2AnyParticleData
    {
        public UInt32[] TextureIDs;
        public UInt32[] MaterialIDs;
        public UInt32 DecalTextureID;
        public UInt32 DecalMaterialID;
        public Byte[] UnkData;
        public Byte[] UnkBlob;
        public Int32[] UnkInts;
        public List<Byte[]> UnkBlobs;

        public PS2DefaultParticleData() : base()
        {
            UnkBlobs = new List<Byte[]>();
            TextureIDs = new UInt32[3];
            MaterialIDs = new UInt32[3];
            UnkInts = new Int32[16];
        }

        public override Int32 GetLength()
        {
            return 24 + 8 + ParticleTypes.Sum(t => t.GetLength()) + 12 + 0x420 + 0x40 + UnkBlobs.Sum(b => 0x890);
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            for (var i = 0; i < 3; ++i)
            {
                TextureIDs[i] = reader.ReadUInt32();
                MaterialIDs[i] = reader.ReadUInt32();
            }
            Version = reader.ReadUInt32();
            if ((Version < 0x5 || Version > 0x1E) && Version != 0x20)
            {
                throw new Exception($"Invalid/Deprecated particle data section version: {Version}");
            }
            var typesAmt = reader.ReadInt32();
            for (var i = 0; i < typesAmt; ++i)
            {
                var type = new ParticleType(Version);
                type.Read(reader, length);
                ParticleTypes.Add(type);
            }
            DecalTextureID = reader.ReadUInt32();
            DecalMaterialID = reader.ReadUInt32();
            UnkData = reader.ReadBytes(4);
            UnkBlob = reader.ReadBytes(0x420);
            for (var i = 0; i < 16; ++i)
            {
                UnkInts[i] = reader.ReadInt32();
            }
            for (var i = 0; i < 16; ++i)
            {
                if (UnkInts[i] != 0)
                {
                    UnkBlobs.Add(reader.ReadBytes(0x890));
                }
            }
        }

        public override void Write(BinaryWriter writer)
        {
            for (var i = 0; i < 3; ++i)
            {
                writer.Write(TextureIDs[i]);
                writer.Write(MaterialIDs[i]);
            }
            // We can do base here because we gonna have 0 particle types, so this is fine
            // since otherwise base would read a texture ID as the amount of particle instances
            base.Write(writer);
            writer.BaseStream.Position -= 4;
            writer.Write(DecalTextureID);
            writer.Write(DecalMaterialID);
            writer.Write(UnkData);
            writer.Write(UnkBlob);
            for (var i = 0; i < 16; ++i)
            {
                writer.Write(UnkInts[i]);
            }
            foreach (var b in UnkBlobs)
            {
                writer.Write(b);
            }
        }
    }
}
