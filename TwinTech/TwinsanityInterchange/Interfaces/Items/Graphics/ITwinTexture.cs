using static Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics.PS2AnyTexture;
using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items
{
    public interface ITwinTexture : ITwinItem
    {
        List<Color> Colors { get; set; }
        UInt32 HeaderSignature { get; set; }
        UInt16 ImageWidthPower { get; set; }
        UInt16 ImageHeightPower { get; set; }
        Byte MipLevels { get; set; }
        TexturePixelFormat TextureFormat { get; set; }
        TexturePixelFormat DestinationTextureFormat { get; set; }
        TextureColorComponent ColorComponent { get; set; }
        Byte UnkByte  { get; set; }
        TextureFunction TexFun  { get; set; }
        Byte[] UnkBytes1  { get; set; }
        Int32 TextureBasePointer { get; set; }
        Int32[] MipLevelsTBP { get; set; }
        Int32 TextureBufferWidth { get; set; }
        Int32[] MipLevelsTBW { get; set; }
        Int32 ClutBufferBasePointer { get; set; }
        Byte[] UnkBytes2  { get; set; }
        Byte[] UnkBytes3  { get; set; }
        Byte[] UnusedMetadata { get; set; }
        Byte[] TextureData { get; set; }

        void CalculateData();
        void FromBitmap(List<Color> image, Int32 width, TextureFunction fun, TexturePixelFormat format);

        #region Enums
        enum TexturePixelFormat
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
        enum TextureColorComponent
        {
            RGB = 0,
            RGBA = 1
        }
        enum TextureFunction
        {
            MODULATE = 0b00,
            DECAL = 0b01,
            HIGHLIGHT = 0b10,
            HIGHLIGHT2 = 0b11
        }
        #endregion
    }
}
