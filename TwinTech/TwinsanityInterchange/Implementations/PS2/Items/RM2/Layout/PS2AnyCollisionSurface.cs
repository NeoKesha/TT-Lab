using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout
{
    public class PS2AnyCollisionSurface : BaseTwinItem, ITwinSurface
    {
        public UInt32 Flags { get; set; }
        public UInt16 SurfaceId { get; set; }
        public UInt16[] StepSoundIds { get; set; }
        public Single[] UnkFloatParams { get; set; }
        public Vector4 UnkVec { get; set; }
        public Vector4[] UnkBoundingBox { get; set; }
        
        public PS2AnyCollisionSurface()
        {
            StepSoundIds = new ushort[10];
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
            SurfaceId = reader.ReadUInt16();
            for (int i = 0; i < StepSoundIds.Length; ++i)
            {
                StepSoundIds[i] = reader.ReadUInt16();
            }
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
            writer.Write(SurfaceId);
            for (int i = 0; i < StepSoundIds.Length; ++i)
            {
                writer.Write(StepSoundIds[i]);
            }
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
