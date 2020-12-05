using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives
{
    public class PS2MD : ITwinSerializable
    {
        public PS2MH Header;
        public List<MDRecord> Records;

        // A requirement to provide the header archive
        public PS2MD(PS2MH header)
        {
            Header = header;
        }

        public Int32 GetLength()
        {
            return Records.Sum(r => r.GetLength());
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            foreach (var record in Header.Records)
            {
                reader.BaseStream.Position = record.Offset;
                var r = new MDRecord(record);
                r.Read(reader, record.Size);
                Records.Add(r);
            }
        }

        public void Write(BinaryWriter writer)
        {
            foreach (var r in Records)
            {
                r.Write(writer);
            }
        }
    }
}
