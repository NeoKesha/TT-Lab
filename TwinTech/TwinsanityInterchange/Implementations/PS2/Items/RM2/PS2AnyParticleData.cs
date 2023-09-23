using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2
{
    public class PS2AnyParticleData : BaseTwinItem, ITwinPatricle
    {
        public UInt32 Version;
        public List<ParticleType> ParticleTypes;
        public List<ParticleInstance> ParticleInstances;

        public PS2AnyParticleData()
        {
            Version = 0x1E;
            ParticleTypes = new List<ParticleType>();
            ParticleInstances = new List<ParticleInstance>();
        }

        public override Int32 GetLength()
        {
            return 12 + ParticleTypes.Sum(t => t.GetLength()) + ParticleInstances.Sum(i => i.GetLength());
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            Version = reader.ReadUInt32();
            if ((Version < 0x5 || Version > 0x1E) && Version != 0x20)
            {
                throw new Exception($"Invalid/Deprecated particle data section version: {Version}");
            }
            var typesAmt = reader.ReadInt32();
            for (var i = 0; i < typesAmt; ++i)
            {
                var type = new ParticleType(Version);
                type.Read(reader, length);
                ParticleTypes.Add(type);
            }
            var instAmt = reader.ReadInt32();
            for (var i = 0; i < instAmt; ++i)
            {
                var inst = new ParticleInstance(Version);
                inst.Read(reader, length);
                ParticleInstances.Add(inst);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Version);
            writer.Write(ParticleTypes.Count);
            foreach (var t in ParticleTypes)
            {
                t.Write(writer);
            }
            writer.Write(ParticleInstances.Count);
            foreach (var i in ParticleInstances)
            {
                i.Write(writer);
            }
        }

        public override String GetName()
        {
            return $"Particle data {id:X}";
        }
    }
}
