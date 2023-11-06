using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;
using static Twinsanity.TwinsanityInterchange.Enumerations.Enums;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout
{
    public class PS2AnyCollisionSurface : BaseTwinItem, ITwinSurface
    {
        public UInt32 Flags { get; set; }
        public SurfaceType SurfaceId { get; set; }
        public UInt16 StepSoundId1 { get; set; }
        public UInt16 StepSoundId2 { get; set; }
        public UInt16 UnkId1 { get; set; }
        public UInt16 UnkId2 { get; set; }
        public UInt16 LandSoundId1 { get; set; }
        public UInt16 UnkId3 { get; set; }
        public UInt16 UnkId4 { get; set; }
        public UInt16 LandSoundId2 { get; set; }
        public UInt16 UnkSoundId { get; set; }
        public UInt16 UnkId5 { get; set; }
        public Single[] UnkFloatParams { get; set; }
        public Vector4 UnkVec { get; set; }
        public Vector4[] UnkBoundingBox { get; set; }

        public PS2AnyCollisionSurface()
        {
            UnkFloatParams = new float[10];
            UnkVec = new Vector4();
            UnkBoundingBox = new Vector4[2];
        }

        public override int GetLength()
        {
            return 114;
        }

        public override void Read(BinaryReader reader, int length)
        {
            Flags = reader.ReadUInt32();
            SurfaceId = (SurfaceType)reader.ReadUInt16();
            StepSoundId1 = reader.ReadUInt16();
            StepSoundId2 = reader.ReadUInt16();
            UnkId1 = reader.ReadUInt16();
            UnkId2 = reader.ReadUInt16();
            LandSoundId1 = reader.ReadUInt16();
            UnkId3 = reader.ReadUInt16();
            UnkId4 = reader.ReadUInt16();
            LandSoundId2 = reader.ReadUInt16();
            UnkSoundId = reader.ReadUInt16();
            UnkId5 = reader.ReadUInt16();
            for (int i = 0; i < UnkFloatParams.Length; ++i)
            {
                UnkFloatParams[i] = reader.ReadSingle();
            }
            UnkVec.Read(reader, Constants.SIZE_VECTOR4);
            for (int i = 0; i < UnkBoundingBox.Length; ++i)
            {
                UnkBoundingBox[i] = new Vector4();
                UnkBoundingBox[i].Read(reader, Constants.SIZE_VECTOR4);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Flags);
            writer.Write((UInt16)SurfaceId);
            writer.Write(StepSoundId1);
            writer.Write(StepSoundId2);
            writer.Write(UnkId1);
            writer.Write(UnkId2);
            writer.Write(LandSoundId1);
            writer.Write(UnkId3);
            writer.Write(UnkId4);
            writer.Write(LandSoundId2);
            writer.Write(UnkSoundId);
            writer.Write(UnkId5);
            for (int i = 0; i < UnkFloatParams.Length; ++i)
            {
                writer.Write(UnkFloatParams[i]);
            }
            UnkVec.Write(writer);
            for (int i = 0; i < UnkBoundingBox.Length; ++i)
            {
                UnkBoundingBox[i].Write(writer);
            }
        }

        public override String GetName()
        {
            return $"Collision surface {id:X}";
        }
    }
}
