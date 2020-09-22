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
    public class CameraSub1C06 : ITwinSerializeable
    {
        public UInt32 UnkInt { get; set; }
        public Single UnkFloat1 { get; set; }
        public Single UnkFloat2 { get; set; }
        public Single UnkFloat3 { get; set; }
        public List<Vector4> UnkVectors { get; private set; }
        public Byte[] UnkData { get; private set; }
        public UInt16 UnkShort { get; set; }
        public CameraSub1C06()
        {
            UnkVectors = new List<Vector4>();
        }
        public int GetLength()
        {
            return 12 + 4 + 4 + UnkVectors.Count * Constants.SIZE_VECTOR4 * 2 + UnkData.Length + 2;
        }

        public void Read(BinaryReader reader, int length)
        {
            UnkInt = reader.ReadUInt32();
            UnkFloat1 = reader.ReadSingle();
            UnkFloat2 = reader.ReadSingle();
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

        public void Write(BinaryWriter writer)
        {
            writer.Write(UnkInt);
            writer.Write(UnkFloat1);
            writer.Write(UnkFloat2);
            writer.Write(UnkVectors.Count / 2);
            writer.Write(UnkFloat3);
            foreach (ITwinSerializeable e in UnkVectors) {
                e.Write(writer);
            }
            writer.Write(UnkData);
            writer.Write(UnkShort);
        }
    }
}
