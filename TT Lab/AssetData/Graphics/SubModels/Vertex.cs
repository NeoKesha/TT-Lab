using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.AssetData.Graphics.SubModels
{
    public class Vertex
    {
        public Vertex()
        {
            Position = new Vector3();
            Color = new Vector4();
            UV = new Vector3();
            Normal = new Vector4();
            EmitColor = new Vector4();
        }
        public Vertex(Vector4 pos) : this()
        {
            Position.X = pos.X;
            Position.Y = pos.Y;
            Position.Z = pos.Z;
        }
        public Vertex(Vector4 pos, Vector4 color) : this(pos)
        {
            Color.X = color.X;
            Color.Y = color.Y;
            Color.Z = color.Z;
            Color.W = color.W;
        }
        public Vertex(Vector4 pos,  Vector4 color, Vector4 uv) : this(pos,color)
        {
            UV.X = uv.X;
            UV.Y = uv.Y;
            UV.Z = uv.Z;
        }
        public Vertex(Vector4 pos, Vector4 color, Vector4 uv, Vector4 emitColor) : this(pos,color,uv)
        {
            EmitColor.X = emitColor.X;
            EmitColor.Y = emitColor.Y;
            EmitColor.Z = emitColor.Z;
            EmitColor.W = emitColor.W;
        }
        public Vector3 Position { get; set; }
        public Vector4 Color { get; set; }
        public Vector4 Normal { get; set; }
        public Vector4 EmitColor { get; set; }
        public Vector3 UV { get; set; }

        public override String ToString()
        {
            var r = (Byte)Math.Round(Color.X * 255.0f);
            var g = (Byte)Math.Round(Color.Y * 255.0f);
            var b = (Byte)Math.Round(Color.Z * 255.0f);
            var a = (Byte)Math.Round(Color.W * 255.0f);
            return $"{Position.X} {Position.Y} {Position.Z} {UV.X} {UV.Y} {UV.Z} {r} {g} {b} {a} {EmitColor.X} {EmitColor.Y} {EmitColor.Z} {EmitColor.W}";
        }
    }
}
