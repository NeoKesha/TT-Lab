using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Archives
{
    public class PS2MB : ITwinSerializable
    {
        public PS2MH Header;
        public List<MBRecord> Records;

        // A requirement to provide the header archive
        public PS2MB(PS2MH header)
        {
            Header = header;
            Records = new List<MBRecord>();
        }

        public Int32 GetLength()
        {
            return Records.Sum(r => r.GetLength());
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            foreach (var record in Header.GetSortedRecords())
            {
                reader.BaseStream.Position = record.Offset;
                var hasRec = false;
                var r = new MBRecord(record);
                r.Read(reader, record.Size);
                // Apparently "undefined" track dupes are not allowed
                if (r.Name != null && r.Name.Replace("\0", "") == "undefined")
                {
                    var undef = Records.Find(rec => rec.Name != null && rec.Name.Replace("\0", "") == "undefined");
                    if (undef != null) hasRec = true;
                }
                if (!hasRec)
                {
                    Records.Add(r);
                }
            }
        }

        public void Write(BinaryWriter writer)
        {
            foreach (var r in Records)
            {
                r.Write(writer);
                // Padding
                while (writer.BaseStream.Position % 0x800 != 0)
                {
                    writer.Write((Byte)0);
                }
            }
        }
    }
}
