using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Implementations.Xbox.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.Xbox
{
    public class XboxPTC : ITwinSerializable
    {
        public UInt32 TexID;
        public UInt32 MatID;
        public XboxAnyTexture Texture;
        public XboxAnyMaterial Material;

        public Int32 GetLength()
        {
            return 8 + Texture.GetLength() + Material.GetLength();
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            TexID = reader.ReadUInt32();
            MatID = reader.ReadUInt32();
            Texture = new XboxAnyTexture();
            Texture.Read(reader, length);
            Material = new XboxAnyMaterial();
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
