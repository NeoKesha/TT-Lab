using System;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.AssetData.Graphics.SubModels
{
    public class Vertex
    {
        public Vertex()
        {
            Position = new Vector4();
            Color = new Vector4();
            UV = new Vector4();
            _normal = new Vector4();
            _emitColor = new Vector4();
            JointInfo = new VertexJointInfo();
        }
        public Vertex(Vector4 pos) : this()
        {
            Position.X = pos.X;
            Position.Y = pos.Y;
            Position.Z = pos.Z;
            Position.W = pos.W;
        }
        public Vertex(Vector4 pos, Vector4 color) : this(pos)
        {
            Color.X = color.X;
            Color.Y = color.Y;
            Color.Z = color.Z;
            Color.W = color.W;
            AlphaBlendingBit = color.StoresColorWithAlphaBlend;
        }
        public Vertex(Vector4 pos, Vector4 color, Vector4 uv) : this(pos, color)
        {
            UV.X = uv.X;
            UV.Y = uv.Y;
            UV.Z = uv.Z;
            UV.W = uv.W;
        }
        public Vertex(Vector4 pos, Vector4 color, Vector4 uv, Vector4 emitColor) : this(pos, color, uv)
        {
            EmitColor.X = emitColor.X;
            EmitColor.Y = emitColor.Y;
            EmitColor.Z = emitColor.Z;
            EmitColor.W = emitColor.W;
        }
        public Vector4 Position { get; set; }
        public Vector4 Color { get; set; }
        public Vector4 Normal
        {
            get => _normal;
            set
            {
                _normal = value;
                HasNormals = true;
            }
        }
        public Vector4 EmitColor
        {
            get => _emitColor;
            set
            {
                _emitColor = value;
                HasEmitColor = true;
            }
        }
        public Vector4 UV { get; set; }
        public VertexJointInfo JointInfo { get; set; }

        public bool HasNormals { get; private set; }
        public bool HasEmitColor { get; private set; }
        public bool AlphaBlendingBit { get; set; }

        private Vector4 _normal;
        private Vector4 _emitColor;

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
