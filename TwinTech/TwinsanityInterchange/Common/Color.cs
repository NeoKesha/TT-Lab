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
    public class Color : ITwinSerializable
    {
        public Color()
        {
            A = 255;
            R = 255;
            G = 255;
            B = 255;
        }
        public Color(Byte R, Byte G, Byte B)
        {
            A = 255;
            this.R = R;
            this.G = G;
            this.B = B;
        }
        public Color(Byte R, Byte G, Byte B, Byte A)
        {
            this.A = A;
            this.R = R;
            this.G = G;
            this.B = B;
        }
        public Byte A { get; set; }
        public Byte R { get; set; }
        public Byte G { get; set; }
        public Byte B { get; set; }
        public int GetLength()
        {
            return 4;
        }

        public void Read(BinaryReader reader, int length)
        {
            R = reader.ReadByte();
            G = reader.ReadByte();
            B = reader.ReadByte();
            A = reader.ReadByte();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(R);
            writer.Write(G);
            writer.Write(B);
            writer.Write(A);
        }
        public Vector4 GetVector()
        {
            Vector4 vec = new Vector4();
            vec.X = R / 255.0f;
            vec.Y = G / 255.0f;
            vec.Z = B / 255.0f;
            vec.W = A / 255.0f;
            return vec;
        }

        public UInt32 ToARGB()
        {
            return (UInt32)((A << 24) | (R << 16) | (G << 8) | (B));
        }
        public void FromABGR(UInt32 val)
        {
            A = (Byte)(((val >> 24) & 0xFF) << 1);
            B = (Byte)(((val >> 16) & 0xFF) << 1);
            G = (Byte)(((val >> 8) & 0xFF) << 1);
            R = (Byte)(((val >> 0) & 0xFF) << 1);
        }
        public UInt32 ToABGR()
        {
            var a = A >> 1;
            var b = B >> 1;
            var g = G >> 1;
            var r = R >> 1;
            return (UInt32)((a << 24) | (b << 16) | (g << 8) | (r << 0));
        }

        public override String ToString()
        {
            return $"{A} {R} {G} {B}";
        }
    }
}
