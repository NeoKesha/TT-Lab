using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class ParticleType : ITwinSerializable
    {
        public UInt32 Version;
        public char[] Name;
        public Byte UnkByte1;
        public UInt16 UnkUShort1;
        public UInt16 UnkUShort2;
        public UInt16 UnkUShort3;
        public UInt16 UnkUShort4;
        public UInt16 UnkUShort5;
        public UInt16 UnkUShort6;
        public UInt16 UnkUShort7;
        public Byte UnkByte2;
        public Byte UnkByte3;
        public Byte UnkByte4;
        public Byte UnkByte5;
        public Single UnkFloat1;
        public Single UnkFloat2;
        public Single UnkFloat3;
        public Single UnkFloat4;
        public Single UnkFloat5;
        public Single UnkFloat6;
        public Single UnkFloat7;
        public Vector3 UnkVec1;
        public Vector3 UnkVec2;
        public Single UnkFloat8;
        public Single UnkFloat9;
        public Single UnkFloat10;
        public Single UnkFloat11;
        public Single UnkFloat12;
        public Single UnkFloat13;
        public Single UnkFloat14;
        public Single UnkFloat15;
        public Single UnkFloat16;
        public Single UnkFloat17;
        public Single UnkFloat18;
        public Single UnkFloat19;
        public Single UnkFloat20;
        public Single UnkFloat21;
        public UInt16 UnkUShort8;
        public Byte UnkByte6;
        public Byte UnkByte7;
        public Single UnkFloat22;
        public Single UnkFloat23;
        public Single UnkFloat24;
        public Single UnkFloat25;
        public Single UnkFloat26;
        public Vector4[] UnkVecs;
        public Int64[] UnkLongs1;
        public Single UnkFloat27;
        public Single UnkFloat28;
        public Single UnkFloat29;
        public Single UnkFloat30;
        public Int64[] UnkLongs2;
        public Int64[] UnkLongs3;
        public Single UnkFloat31;
        public Single UnkFloat32;
        public Int64[] UnkLongs4;
        public Int64[] UnkLongs5;
        public Int64[] UnkLongs6;
        public Single UnkFloat33;
        public Single UnkFloat34;
        public Single UnkFloat35;
        public Single UnkFloat36;
        public Int64[] UnkLongs7;
        public Byte UnkByte8;
        public Byte UnkByte9;
        private Int32 padAmount;
        public Single UnkFloat37;
        public Int16[] UnkShorts; // Short index 1
        public Single UnkFloat38;
        // Short index 2
        public Single UnkFloat39;
        public Single UnkFloat40;
        public Int32 UnkInt;
        // UnkFloat37 again
        public Vector4 UnkVec3;

        public ParticleType(UInt32 ver)
        {
            Version = ver;
            Name = new char[16];
            UnkVec1 = new Vector3();
            UnkVec2 = new Vector3();
            UnkVecs = new Vector4[8];
            UnkLongs1 = new Int64[8];
            UnkLongs2 = new Int64[8];
            UnkLongs3 = new Int64[8];
            UnkLongs4 = new Int64[8];
            UnkLongs5 = new Int64[8];
            UnkLongs6 = new Int64[8];
            UnkLongs7 = new Int64[8];
            UnkShorts = new Int16[4];
            UnkVec3 = new Vector4();
        }

        public Int32 GetLength()
        {
            throw new NotImplementedException();
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            Name = reader.ReadChars(16);
            if (Version == 0x20)
            {
                reader.ReadByte();
                UnkByte1 = reader.ReadByte();
            }
            UnkUShort1 = reader.ReadUInt16();
            UnkUShort2 = reader.ReadUInt16();
            UnkUShort3 = reader.ReadUInt16();
            UnkUShort4 = reader.ReadUInt16();
            UnkUShort5 = reader.ReadUInt16();
            UnkUShort6 = reader.ReadUInt16();
            UnkUShort7 = reader.ReadUInt16();
            UnkByte2 = reader.ReadByte();
            UnkByte3 = reader.ReadByte();
            UnkByte4 = reader.ReadByte();
            UnkByte5 = reader.ReadByte();
            UnkFloat1 = reader.ReadSingle();
            if (Version == 0x20)
            {
                UnkFloat1 = 25;
            }
            UnkFloat2 = 0;
            UnkFloat3 = 25;
            if (Version >= 0x6)
            {
                UnkFloat2 = reader.ReadSingle();
                UnkFloat3 = reader.ReadSingle();
            }
            UnkFloat4 = 0;
            if (Version >= 0xA)
            {
                UnkFloat4 = reader.ReadSingle();
                if (UnkFloat4 <= 2)
                {
                    UnkFloat4 = 999999.875f;
                }
            }
            if (Version <= 0x16 || Version == 0x20)
            {
                UnkFloat5 = 0;
            } 
            else
            {
                UnkFloat5 = reader.ReadSingle();
            }
            if (Version < 0x18 || Version == 0x20)
            {
                UnkFloat6 = 0.5f;
            }
            else
            {
                UnkFloat6 = reader.ReadSingle();
            }
            if (Version < 0x7)
            {
                reader.ReadBytes(8);
            }
            UnkFloat7 = reader.ReadSingle();
            UnkVec1.Read(reader, Constants.SIZE_VECTOR3);
            if (Version < 0x12)
            {
                reader.ReadBytes(0xC);
            }
            UnkVec2.Read(reader, Constants.SIZE_VECTOR3);
            if (Version < 0x12)
            {
                reader.ReadBytes(0xC);
            }
            UnkFloat8 = reader.ReadSingle();
            UnkFloat9 = reader.ReadSingle();
            UnkFloat10 = reader.ReadSingle();
            UnkFloat11 = reader.ReadSingle();
            UnkFloat12 = reader.ReadSingle();
            UnkFloat13 = reader.ReadSingle();
            UnkFloat14 = reader.ReadSingle();
            UnkFloat15 = reader.ReadSingle();
            UnkFloat16 = reader.ReadSingle();
            UnkFloat17 = reader.ReadSingle();
            UnkFloat18 = reader.ReadSingle();
            UnkFloat19 = reader.ReadSingle();
            UnkFloat20 = reader.ReadSingle();
            UnkFloat21 = reader.ReadSingle();
            UnkUShort8 = reader.ReadUInt16();
            UnkByte6 = reader.ReadByte();
            UnkByte7 = reader.ReadByte();
            UnkFloat22 = reader.ReadSingle();
            UnkFloat23 = reader.ReadSingle();
            UnkFloat24 = reader.ReadSingle();
            UnkFloat25 = reader.ReadSingle();
            UnkFloat26 = reader.ReadSingle();
            for (var i = 0; i < 8; ++i)
            {
                UnkVecs[i].Read(reader, Constants.SIZE_VECTOR4);
            }
            for (var i = 0; i < 8; ++i)
            {
                UnkLongs1[i] = reader.ReadInt64();
            }
            UnkFloat27 = 0.125f;
            UnkFloat28 = 0.125f;
            if (Version > 0x15)
            {
                UnkFloat27 = reader.ReadSingle();
                UnkFloat28 = reader.ReadSingle();
            }
            UnkFloat29 = reader.ReadSingle();
            UnkFloat30 = reader.ReadSingle();
            for (var i = 0; i < 8; ++i)
            {
                UnkLongs2[i] = reader.ReadInt64();
            }
            for (var i = 0; i < 8; ++i)
            {
                UnkLongs3[i] = reader.ReadInt64();
            }
            UnkFloat31 = reader.ReadSingle();
            UnkFloat32 = reader.ReadSingle();
            for (var i = 0; i < 8; ++i)
            {
                UnkLongs4[i] = reader.ReadInt64();
            }
            for (var i = 0; i < 8; ++i)
            {
                UnkLongs5[i] = reader.ReadInt64();
            }
            for (var i = 0; i < 8; ++i)
            {
                UnkLongs6[i] = reader.ReadInt64();
            }
            UnkFloat33 = reader.ReadSingle();
            UnkFloat34 = reader.ReadSingle();
            UnkFloat35 = reader.ReadSingle();
            UnkFloat36 = reader.ReadSingle();
            if (Version == 0x20)
            {
                reader.ReadBytes(4);
            }
            if (Version >= 0x3)
            {
                for (var i = 0; i < 8; ++i)
                {
                    UnkLongs7[i] = reader.ReadInt64();
                }
                UnkByte8 = reader.ReadByte();
            }
            if (Version >= 0x11)
            {
                UnkByte9 = reader.ReadByte();
            }
            if (UnkByte4 == 0x7)
            {
                UnkByte9 = 2;
            }
            if (Version == 0x20)
            {
                reader.ReadBytes(6);
            }
            if (Version > 0x16 && Version < 0x1D && Version != 0x20)
            {
                padAmount = reader.ReadInt32();
                for (var i = 0; i < padAmount; ++i)
                {
                    reader.ReadBytes(24);
                }
            }
            else
            {
                if (Version == 0x20)
                {
                    UnkFloat37 = reader.ReadSingle();
                    reader.ReadBytes(56);
                }
            }
            UnkShorts[1] = 0;
            UnkFloat38 = 0;
            if (Version >= 0x10)
            {
                if (Version == 0x20)
                {
                    UnkShorts[1] = reader.ReadInt16();
                    reader.ReadBytes(2);
                }
                else
                {
                    UnkShorts[1] = (Int16)reader.ReadInt32();
                }
                UnkFloat38 = reader.ReadSingle();
            }
            UnkShorts[2] = 5;
            UnkFloat39 = 0.5f;
            if (Version >= 0x19 && Version != 0x20)
            {
                UnkShorts[2] = (Int16)reader.ReadInt32();
                UnkFloat39 = reader.ReadSingle();
            }
            UnkFloat40 = 0;
            if (Version >= 0x1A && Version != 0x20)
            {
                UnkFloat40 = reader.ReadSingle();
            }
            if (Version != 0x20)
            {
                if (Version > 0x1A)
                {
                    UnkInt = reader.ReadInt32();
                }
                if (Version > 0x1B)
                {
                    UnkFloat37 = reader.ReadSingle();
                }
            }
            if (Version >= 0x1E)
            {
                UnkVec3.Read(reader, Constants.SIZE_VECTOR4);
            }
            else
            {
                UnkVec3.X = 10;
                UnkVec3.Y = 10;
                UnkVec3.Z = 10;
                UnkVec3.W = 0;
                if (UnkByte2 == 0)
                {
                    var f1 = UnkFloat30 * 0.0001f;
                    UnkVec3.X = ((UnkFloat7 + UnkVec1.X) * UnkFloat21 + UnkVec2.X + f1) * 0.75f;
                    UnkVec3.Y = ((UnkFloat7 + UnkVec1.Y) * UnkFloat21 + UnkVec2.Y + f1) * 0.75f;
                    UnkVec3.Z = ((UnkFloat7 + UnkVec1.Z) * UnkFloat21 + UnkVec2.Z + f1) * 0.75f;
                }
            }
        }

        public void Write(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
