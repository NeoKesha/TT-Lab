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
    public class CameraPath : CameraSubBase
    {
        public List<Vector4> UnkVectors { get; set; }
        public Byte[] UnkData { get; set; }
        public CameraPath()
        {
            UnkVectors = new List<Vector4>();
        }
        public override int GetLength()
        {
            return base.GetLength() + 4 + UnkVectors.Count * Constants.SIZE_VECTOR4 + 4 + UnkData.Length;
        }

        public override void Read(BinaryReader reader, int length)
        {
            base.Read(reader, base.GetLength());
            int cnt1 = reader.ReadInt32();
            UnkVectors.Clear();
            for (int i = 0; i < cnt1; ++i)
            {
                Vector4 vec = new Vector4();
                vec.Read(reader, Constants.SIZE_VECTOR4);
                UnkVectors.Add(vec);
            }
            int cnt2 = reader.ReadInt32();
            UnkData = reader.ReadBytes(cnt2 * 8);
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(UnkVectors.Count);
            foreach(ITwinSerializable e in UnkVectors) {
                e.Write(writer);
            }
            writer.Write(UnkData.Length / 8);
            writer.Write(UnkData);
        }
    }
}
