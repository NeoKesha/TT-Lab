using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2
{
    public class PS2AnyParticleData : ITwinPatricle
    {
        UInt32 id;
        public UInt32 Version;
        public Int32 ParticleTypesAmount;
        public List<ParticleType> ParticleTypes;

        public PS2AnyParticleData()
        {
            ParticleTypes = new List<ParticleType>();
        }

        public UInt32 GetID()
        {
            return id;
        }

        public Int32 GetLength()
        {
            throw new NotImplementedException();
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            throw new NotImplementedException();
        }

        public void SetID(UInt32 id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
