using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class TwinShader : ITwinSerializeable
    {
        public UInt32 ShaderType { get; set; }
        public UInt32 IntParam { get; set; }
        public Single[] FloatParam { get; private set; }
        Byte[] bitfieldData;
        UInt64 bitfield;
        public UInt16 UnkShort1 { get; set; }
        public UInt16 UnkShort2 { get; set; }
        public Vector4 UnkVector1 { get; private set; }
        public Vector4 UnkVector2 { get; private set; }
        public Vector4 UnkVector3 { get; private set; }
        public UInt32 TextureId { get; set; }
        public UInt32 UnkInt2 { get; set; }
        public TwinBlob Blob { get; private set; }
        public TwinShader()
        {
            FloatParam = new float[4];
            bitfieldData = new byte[30];
            UnkVector1 = new Vector4();
            UnkVector2 = new Vector4();
            UnkVector3 = new Vector4();
            Blob = null;
        }
        public int GetLength()
        {
            int blobLen = (Blob != null) ? Blob.GetLength() : 0;
            int paramLen = (ShaderType == 23) ? 12 :
                            (ShaderType == 26) ? 20 :
                            (ShaderType == 16 || ShaderType == 17) ? 4 :
                            0;
            return 4 + paramLen + bitfieldData.Length + 4 + Constants.SIZE_VECTOR4 * 3 + 8 + blobLen;
        }

        public void Read(BinaryReader reader, int length)
        {
            ShaderType = reader.ReadUInt32();
            switch (ShaderType)
            {
                case 23:
                    IntParam = reader.ReadUInt32();
                    FloatParam[0] = reader.ReadSingle();
                    FloatParam[1] = reader.ReadSingle();
                    break;
                case 26:
                    IntParam = reader.ReadUInt32();
                    FloatParam[0] = reader.ReadSingle();
                    FloatParam[1] = reader.ReadSingle();
                    FloatParam[2] = reader.ReadSingle();
                    FloatParam[3] = reader.ReadSingle();
                    break;
                case 16:
                case 17:
                    FloatParam[0] = reader.ReadSingle();
                    break;
                default:
                    break;
            }
            reader.Read(bitfieldData, 0, bitfieldData.Length);
            //bitfield = reader.ReadUInt64();
            UnkShort1 = reader.ReadUInt16();
            UnkShort2 = reader.ReadUInt16();
            UnkVector1.Read(reader, Constants.SIZE_VECTOR4);
            UnkVector2.Read(reader, Constants.SIZE_VECTOR4);
            UnkVector3.Read(reader, Constants.SIZE_VECTOR4);
            TextureId = reader.ReadUInt32();
            UnkInt2 = reader.ReadUInt32();
            if (bitfieldData[29] != 0)
            {
                Blob = new TwinBlob();
                Blob.Read(reader, 0);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ShaderType);
            switch (ShaderType)
            {
                case 23:
                    writer.Write(IntParam);
                    writer.Write(FloatParam[0]);
                    writer.Write(FloatParam[1]);
                    break;
                case 26:
                    writer.Write(IntParam);
                    writer.Write(FloatParam[0]);
                    writer.Write(FloatParam[1]);
                    writer.Write(FloatParam[2]);
                    writer.Write(FloatParam[3]);
                    break;
                case 16:
                case 17:
                    writer.Write(FloatParam[0]);
                    break;
                default:
                    break;
            }
            if (Blob != null)
            {
                bitfieldData[29] = 1;
            } else
            {
                bitfieldData[29] = 0;
            }
            writer.Write(bitfieldData);
            writer.Write(UnkShort1);
            writer.Write(UnkShort2);
            UnkVector1.Write(writer);
            UnkVector2.Write(writer);
            UnkVector3.Write(writer);
            writer.Write(TextureId);
            writer.Write(UnkInt2);
            if (Blob != null)
            {
                Blob.Write(writer);
            }
        }
    }
}
