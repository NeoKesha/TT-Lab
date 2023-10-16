using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Implementations.Xbox.Items.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.Xbox
{
    public class XboxPTC : ITwinPTC
    {
        public UInt32 TexID { get; set; }
        public UInt32 MatID { get; set; }
        public ITwinTexture Texture { get; set; }
        public ITwinMaterial Material { get; set; }

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
