using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace Twinsanity.TwinsanityInterchange.Common.CameraSubtypes
{
    public class CameraSpline : CameraSubBase
    {
        public Single UnkFloat3 { get; set; }
        public List<Vector4> PathPoints { get; set; }
        public List<Vector4> InterpolationPoints { get; set; }
        public List<UInt64> UnkData { get; set; }
        public UInt16 UnkShort { get; set; }
        public CameraSpline()
        {
            PathPoints = new List<Vector4>();
            InterpolationPoints = new List<Vector4>();
            UnkData = new List<UInt64>();
        }
        public override int GetLength()
        {
            return base.GetLength() + 4 + 4 + PathPoints.Count * Constants.SIZE_VECTOR4 + InterpolationPoints.Count * Constants.SIZE_VECTOR4 + UnkData.Count * 8 + 2;
        }

        public override void Read(BinaryReader reader, int length)
        {
            base.Read(reader, base.GetLength());
            int cnt1 = reader.ReadInt32();
            UnkFloat3 = reader.ReadSingle();
            PathPoints.Clear();
            InterpolationPoints.Clear();
            for (int i = 0; i < cnt1; ++i)
            {
                Vector4 vec = new Vector4();
                vec.Read(reader, Constants.SIZE_VECTOR4);
                PathPoints.Add(vec);
                Vector4 vec2 = new Vector4();
                vec2.Read(reader, Constants.SIZE_VECTOR4);
                InterpolationPoints.Add(vec2);
            }
            UnkData.Clear();
            for (var i = 0; i < cnt1; ++i)
            {
                UnkData.Add(reader.ReadUInt64());
            }
            UnkShort = reader.ReadUInt16();
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(PathPoints.Count);
            writer.Write(UnkFloat3);
            for (var i = 0; i < PathPoints.Count; ++i)
            {
                PathPoints[i].Write(writer);
                InterpolationPoints[i].Write(writer);
            }
            foreach (var d in UnkData)
            {
                writer.Write(d);
            }
            writer.Write(UnkShort);
        }

        public override ITwinCamera.CameraType GetCameraType()
        {
            return ITwinCamera.CameraType.CameraSpline;
        }
    }
}
