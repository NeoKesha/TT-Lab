using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics
{
    public class PS2AnyModel : ITwinModel
    {
        UInt32 id;

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
