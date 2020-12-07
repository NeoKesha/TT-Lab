using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2
{
    public class PS2PTC : ITwinSerializable
    {
        public UInt32 TexID;
        public UInt32 MatID;
        public PS2AnyTexture Texture;
        public PS2AnyMaterial Material;

        public Int32 GetLength()
        {
            return 8 + Texture.GetLength() + Material.GetLength();
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            TexID = reader.ReadUInt32();
            MatID = reader.ReadUInt32();
            Texture = new PS2AnyTexture();
            Texture.Read(reader, length);
            Material = new PS2AnyMaterial();
            Material.Read(reader, length);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(TexID);
            writer.Write(MatID);
            Texture.Write(writer);
            Material.Write(writer);
        }
    }
}
