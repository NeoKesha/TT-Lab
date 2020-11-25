using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.CameraSubtypes
{
    public class CameraSpline : CameraSubBase
    {
        public Single UnkFloat3 { get; set; }
        public List<Vector4> UnkVectors { get; private set; }
        public Byte[] UnkData { get; private set; }
        public UInt16 UnkShort { get; set; }
        public CameraSpline()
        {
            UnkVectors = new List<Vector4>();
        }
        public override int GetLength()
        {
            return base.GetLength() + 4 + 4 + UnkVectors.Count * Constants.SIZE_VECTOR4 * 2 + UnkData.Length + 2;
        }

        public override void Read(BinaryReader reader, int length)
        {
            base.Read(reader, base.GetLength());
            int cnt1 = reader.ReadInt32();
            UnkFloat3 = reader.ReadSingle();
            UnkVectors.Clear();
            for (int i = 0; i < 2 * cnt1; ++i)
            {
                Vector4 vec = new Vector4();
                vec.Read(reader, Constants.SIZE_VECTOR4);
                UnkVectors.Add(vec);
            }
            UnkData = reader.ReadBytes(cnt1 * 8);
            UnkShort = reader.ReadUInt16();
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(UnkVectors.Count / 2);
            writer.Write(UnkFloat3);
            foreach (ITwinSerializable e in UnkVectors) {
                e.Write(writer);
            }
            writer.Write(UnkData);
            writer.Write(UnkShort);
        }
    }
}
