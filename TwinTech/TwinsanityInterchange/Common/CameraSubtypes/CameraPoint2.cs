using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace Twinsanity.TwinsanityInterchange.Common.CameraSubtypes
{
    public class CameraPoint2 : CameraSubBase
    {
        public Vector4 UnkVec { get; private set; }
        public Single UnkFloat3 { get; set; }
        public Byte UnkByte { get; set; }
        public CameraPoint2()
        {
            UnkVec = new Vector4();
        }
        public override int GetLength()
        {
            return base.GetLength() + 5 + Constants.SIZE_VECTOR4;
        }

        public override void Read(BinaryReader reader, int length)
        {
            base.Read(reader, base.GetLength());
            UnkVec.Read(reader, Constants.SIZE_VECTOR4);
            UnkFloat3 = reader.ReadSingle();
            UnkByte = reader.ReadByte();
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            UnkVec.Write(writer);
            writer.Write(UnkFloat3);
            writer.Write(UnkByte);
        }
    }
}
