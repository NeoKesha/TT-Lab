using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.Libraries;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics
{
    public class PS2AnyTexture : BaseTwinItem, ITwinTexture
    {
        public UInt32 HeaderSignature { get; set; }
        public UInt16 ImageWidthPower { get; set; }
        public UInt16 ImageHeightPower { get; set; }
        public Byte MipLevels { get; set; }
        public TexturePixelFormat TextureFormat { get; set; }
        public TexturePixelFormat DestinationTextureFormat { get; set; }
        public TextureColorComponent ColorComponent;
        public Byte UnkByte;
        public TextureFunction TexFun;
        public Byte[] UnkBytes1;
        public Int32 TextureBasePointer { get; set; }
        public Int32[] MipLevelsTBP { get; set; }
        public Int32 TextureBufferWidth { get; set; }
        public Int32[] MipLevelsTBW { get; set; }
        public Int32 ClutBufferBasePointer { get; set; }
        public Byte[] UnkBytes2;
        public Byte[] UnkBytes3;
        public Byte[] UnusedMetadata {get; set; }
        public Byte[] TextureData { get; set; }

        public PS2AnyTexture()
        {
            UnusedMetadata = new byte[32];
        }

        public override Int32 GetLength()
        {
            return 4 + 96 + UnusedMetadata.Length + (TextureData != null ? TextureData.Length : 0);
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            int dataLen = reader.ReadInt32();
            HeaderSignature = reader.ReadUInt32();
            ImageWidthPower = reader.ReadUInt16();
            ImageHeightPower = reader.ReadUInt16();
            MipLevels = reader.ReadByte();
            TextureFormat = (TexturePixelFormat)reader.ReadByte();
            DestinationTextureFormat = (TexturePixelFormat)reader.ReadByte();
            ColorComponent = (TextureColorComponent)reader.ReadByte();
            UnkByte = reader.ReadByte();
            TexFun = (TextureFunction)reader.ReadByte();
            UnkBytes1 = reader.ReadBytes(2);
            TextureBasePointer = reader.ReadInt32();
            MipLevelsTBP = new int[6];
            for (var i = 0; i < 6; ++i)
            {
                MipLevelsTBP[i] = reader.ReadInt32();
            }
            TextureBufferWidth = reader.ReadInt32();
            MipLevelsTBW = new int[6];
            for (var i = 0; i < 6; ++i)
            {
                MipLevelsTBW[i] = reader.ReadInt32();
            }
            ClutBufferBasePointer = reader.ReadInt32();
            UnkBytes2 = reader.ReadBytes(8);
            reader.ReadInt32(); // Reserved
            reader.ReadInt32(); // Reserved
            UnkBytes3 = reader.ReadBytes(2);
            reader.ReadBytes(2); // Reserved
            reader.Read(UnusedMetadata, 0, UnusedMetadata.Length);
            TextureData = reader.ReadBytes(dataLen - 96 - UnusedMetadata.Length);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(GetLength() - 4);
            writer.Write(HeaderSignature);
            writer.Write(ImageWidthPower);
            writer.Write(ImageHeightPower);
            writer.Write(MipLevels);
            writer.Write((Byte)TextureFormat);
            writer.Write((Byte)DestinationTextureFormat);
            writer.Write((Byte)ColorComponent);
            writer.Write(UnkByte);
            writer.Write((Byte)TexFun);
            writer.Write(UnkBytes1);
            writer.Write(TextureBasePointer);
            for (var i = 0; i < 6; ++i)
            {
                writer.Write(MipLevelsTBP[i]);
            }
            writer.Write(TextureBufferWidth);
            for (var i = 0; i < 6; ++i)
            {
                writer.Write(MipLevelsTBW[i]);
            }
            writer.Write(ClutBufferBasePointer);
            writer.Write(UnkBytes2);
            writer.Write(0); // Reserved
            writer.Write(0); // Reserved
            writer.Write(UnkBytes3);
            writer.Write((Int16)0); // Reserved
            writer.Write(UnusedMetadata);
            writer.Write(TextureData);
        }

        public void CalculateData()
        {
            var interpreter = VIFInterpreter.InterpretCode(TextureData);
            var data = interpreter.GetGifMem();
            Colors.Clear();
            switch (TextureFormat)
            {
                case TexturePixelFormat.PSMCT32:
                    EzSwizzle.TagToColors(data[1], Colors);
                    break;
                case TexturePixelFormat.PSMT8:
                    byte[] gifData = EzSwizzle.TagToBytes(data[1]);
                    int RRW = (int)((data[0].Data[1].Output >> 0) & 0xFFFFFFFF);
                    int RRH = (int)((data[0].Data[1].Output >> 32) & 0xFFFFFFFF);
                    int Width = (int)(Math.Pow(2, ImageWidthPower));
                    int Height = (int)(Math.Pow(2, ImageHeightPower));
                    byte[] rawTextureData = EzSwizzle.writeTexPSMCT32(0, 1, 0, 0, RRW, RRH, gifData);
                    byte[] texData = EzSwizzle.readTexPSMT8(0, TextureBufferWidth, 0, 0, Width, Height, rawTextureData, false);
                    byte[] paletteData = EzSwizzle.readTexPSMCT32(ClutBufferBasePointer, 1, 0, 0, 16, 16, rawTextureData, false);
                    List<Color> palette = EzSwizzle.BytesToColors(paletteData);
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 8; j < 16; j++)
                        {
                            Color tmp = palette[j + i * 32];
                            palette[j + i * 32] = palette[j + i * 32 + 8];
                            palette[j + i * 32 + 8] = tmp;
                        }
                    }
                    int Pixels = Width * Height;
                    for (var i = 0; i < Pixels; ++i)
                    {
                        Colors.Add(palette[texData[i]]);
                    }
                    break;
            }
        }
        public List<Color> Colors { get; set; } = new List<Color>();
        public override String GetName()
        {
            return $"Texture {id:X}";
        }

        #region Enums
        public enum TexturePixelFormat
        {
            PSMCT32 = 0b000000,
            PSMCT24 = 0b000001,
            PSMCT16 = 0b000010,
            PSMCT16S = 0b001010,
            PSMT8 = 0b010011,
            PSMT4 = 0b010100,
            PSMT8H = 0b011011,
            PSMT4HL = 0b100100,
            PSMT4HH = 0b101100,
            PSMZ32 = 0b110000,
            PSMZ24 = 0b110001,
            PSMZ16 = 0b110010,
            PSMZ16S = 0b111010
        }
        public enum TextureColorComponent
        {
            RGB = 0,
            RGBA = 1
        }
        public enum TextureFunction
        {
            MODULATE = 0b00,
            DECAL = 0b01,
            HIGHLIGHT = 0b10,
            HIGHLIGHT2 = 0b11
        }
        #endregion
    }
}
