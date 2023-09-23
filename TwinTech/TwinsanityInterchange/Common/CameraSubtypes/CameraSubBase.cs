using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.CameraSubtypes
{
    public class CameraSubBase : ITwinSerializable
    {
        public UInt32 UnkInt { get; set; }
        public Single UnkFloat1 { get; set; }
        public Single UnkFloat2 { get; set; }
        public CameraSubBase()
        {
        }

        public virtual int GetLength()
        {
            return 12;
        }

        public virtual void Read(BinaryReader reader, int length)
        {
            UnkInt = reader.ReadUInt32();
            UnkFloat1 = reader.ReadSingle();
            UnkFloat2 = reader.ReadSingle();
        }

        public virtual void Write(BinaryWriter writer)
        {
            writer.Write(UnkInt);
            writer.Write(UnkFloat1);
            writer.Write(UnkFloat2);
        }
    }
}
