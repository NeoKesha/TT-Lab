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
    public class PS2AnyCollisionSurface : ITwinSurface
    {
        UInt32 id;
        public UInt32 Flags { get; set; }
        public UInt16 SurfaceId { get; set; }
        public UInt16[] StepSoundIds { get; private set; }
        public Single[] Parameters { get; private set; }
        public UInt16[] UnkShorts { get; private set; }
        
        public PS2AnyCollisionSurface()
        {
            StepSoundIds = new ushort[10];
            Parameters = new float[16];
            UnkShorts = new ushort[12];
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return 114;
        }

        public void Read(BinaryReader reader, int length)
        {
            Flags = reader.ReadUInt32();
            SurfaceId = reader.ReadUInt16();
            for (int i = 0; i < StepSoundIds.Length; ++i)
            {
                StepSoundIds[i] = reader.ReadUInt16();
            }
            for (int i = 0; i < Parameters.Length; ++i)
            {
                Parameters[i] = reader.ReadSingle();
            }
            for (int i = 0; i < UnkShorts.Length; ++i)
            {
                UnkShorts[i] = reader.ReadUInt16();
            }
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Flags);
            writer.Write(SurfaceId);
            for (int i = 0; i < StepSoundIds.Length; ++i)
            {
                writer.Write(StepSoundIds[i]);
            }
            for (int i = 0; i < Parameters.Length; ++i)
            {
                writer.Write(Parameters[i]);
            }
            for (int i = 0; i < UnkShorts.Length; ++i)
            {
                writer.Write(UnkShorts[i]);
            }
        }

        public String GetName()
        {
            return $"Collision surface {id:X}";
        }
    }
}
