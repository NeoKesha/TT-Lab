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
    public class PS2AnyTexture : ITwinTexture
    {
        UInt32 id;
        public UInt32 HeaderSignature { get; private set; }
        public UInt16 ImageWidthPower { get; set; }
        public UInt16 ImageHeightPower { get; set; }
        public Byte MipLevels { get; set; }
        public Byte[] Header { get; private set; }
        public Byte[] Metadata {get; private set; }
        public Byte[] TextureData { get; private set; }

        public PS2AnyTexture()
        {
            Header = new byte[96];
            Metadata = new byte[32];
        }

        public UInt32 GetID()
        {
            return id;
        }

        public Int32 GetLength()
        {
            return 4 + Header.Length + Metadata.Length + (TextureData != null ? TextureData.Length : 0);
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            int dataLen = reader.ReadInt32();
            reader.Read(Header, 0, Header.Length);
            reader.Read(Metadata, 0, Metadata.Length);
            TextureData = reader.ReadBytes(dataLen - Header.Length - Metadata.Length);
        }

        public void SetID(UInt32 id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(GetLength() - 4);
            writer.Write(Header);
            writer.Write(Metadata);
            writer.Write(TextureData);

        }
    }
}
