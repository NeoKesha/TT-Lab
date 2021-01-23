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
    public class CameraPoint : CameraSubBase
    {
        public Vector4 Point { get; set; }
        public CameraPoint()
        {
            Point = new Vector4();
        }
        public override int GetLength()
        {
            return base.GetLength() + Constants.SIZE_VECTOR4;
        }

        public override void Read(BinaryReader reader, int length)
        {
            base.Read(reader, base.GetLength());
            Point.Read(reader, Constants.SIZE_VECTOR4);
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            Point.Write(writer);
        }
    }
}
