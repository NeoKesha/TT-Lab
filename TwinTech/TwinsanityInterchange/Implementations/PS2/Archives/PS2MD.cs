using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives
{
    public class PS2MD : ITwinSerializable
    {
        public PS2MH Header;


        // A requirement to provide the header archive
        public PS2MD(PS2MH header)
        {
            Header = header;
        }

        public Int32 GetLength()
        {
            throw new NotImplementedException();
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            throw new NotImplementedException();
        }

        public void Write(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
