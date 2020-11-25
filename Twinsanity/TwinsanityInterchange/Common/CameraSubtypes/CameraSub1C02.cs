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
    public class CameraSub1C02 : CameraSubBase
    {
        public Vector4 UnkVector { get; private set; }
        public CameraSub1C02()
        {
            UnkVector = new Vector4();
        }
        public override int GetLength()
        {
            return base.GetLength() + Constants.SIZE_VECTOR4;
        }

        public override void Read(BinaryReader reader, int length)
        {
            base.Read(reader, base.GetLength());
            UnkVector.Read(reader, Constants.SIZE_VECTOR4);
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            UnkVector.Write(writer);
        }
    }
}
